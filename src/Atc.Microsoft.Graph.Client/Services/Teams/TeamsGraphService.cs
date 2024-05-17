namespace Atc.Microsoft.Graph.Client.Services.Teams;

public sealed class TeamsGraphService : GraphServiceClientWrapper, ITeamsGraphService
{
    public TeamsGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<Team> Data)> GetTeams(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken)
    {
        List<Team> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Teams
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForTeams(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new TeamCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Team, TeamCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Team), count);
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

            LogPageIteratorTotalCount(nameof(Team), count);

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