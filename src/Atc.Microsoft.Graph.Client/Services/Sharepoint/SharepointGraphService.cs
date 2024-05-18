namespace Atc.Microsoft.Graph.Client.Services.Sharepoint;

public sealed class SharepointGraphService : GraphServiceClientWrapper, ISharepointGraphService
{
    public SharepointGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<Site> Data)> GetSites(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<Site> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Sites
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForSites(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new SiteCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Site, SiteCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Site), count);
                    }

                    return true;
                });

            try
            {
                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
            {
                return (HttpStatusCode.Gone, pagedItems);
            }

            LogPageIteratorTotalCount(nameof(Site), count);

            return (HttpStatusCode.OK, pagedItems);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
    }

    public async Task<(HttpStatusCode StatusCode, Guid? SubscriptionId)> SetupSubscription(
        Subscription subscription,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(subscription);

        try
        {
            Guid? subscriptionId = null;

            await DownloadResiliencePipeline.ExecuteAsync(
                async context =>
                {
                    var graphSubscription = await Client.Subscriptions
                        .PostAsync(subscription, cancellationToken: context);

                    subscriptionId = graphSubscription?.Id is not null
                        ? Guid.Parse(graphSubscription.Id)
                        : null;

                    if (subscriptionId is null)
                    {
                        LogSubscriptionSetupFailed(subscription.Resource, "Subscription ID is null");
                    }

                    return subscriptionId;
                },
                cancellationToken);

            return (HttpStatusCode.OK, subscriptionId);
        }
        catch (ODataError odataError)
        {
            if (odataError.Error?.Message?.Contains("timed out", StringComparison.OrdinalIgnoreCase) == true)
            {
                return (HttpStatusCode.RequestTimeout, null);
            }

            LogSubscriptionSetupFailed(subscription.Resource, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogSubscriptionSetupFailed(subscription.Resource, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> RenewSubscription(
        Guid subscriptionId,
        DateTimeOffset expirationDate,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var newSubscription = new Subscription
            {
                ExpirationDateTime = expirationDate,
            };

            await Client
                .Subscriptions[subscriptionId.ToString()]
                .PatchAsync(newSubscription, cancellationToken: cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogSubscriptionRenewalFailed(subscriptionId, expirationDate, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogSubscriptionRenewalFailed(subscriptionId, expirationDate, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> DeleteSubscription(
        Guid subscriptionId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await Client.Subscriptions[subscriptionId.ToString()]
                .DeleteAsync(cancellationToken: cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.NotFound)
        {
            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogSubscriptionDeletionFailed(subscriptionId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogSubscriptionDeletionFailed(subscriptionId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }
}