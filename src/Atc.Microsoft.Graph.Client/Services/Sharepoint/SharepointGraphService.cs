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
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
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
}