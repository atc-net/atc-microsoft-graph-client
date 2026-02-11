namespace Atc.Microsoft.Graph.Client.Services.Subscriptions;

public sealed class SubscriptionsGraphService : GraphServiceClientWrapper, ISubscriptionsGraphService
{
    public SubscriptionsGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<Subscription> Data)> GetSubscriptions(
        CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await Client.Subscriptions.GetAsync(cancellationToken: cancellationToken);

            return response?.Value is not null
                ? (HttpStatusCode.OK, response.Value)
                : (HttpStatusCode.InternalServerError, (IList<Subscription>)[]);
        }
        catch (ODataError odataError)
        {
            LogSubscriptionListFailed(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, []);
        }
        catch (Exception ex)
        {
            LogSubscriptionListFailed(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, []);
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

            await ResiliencePipeline.ExecuteAsync(
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
