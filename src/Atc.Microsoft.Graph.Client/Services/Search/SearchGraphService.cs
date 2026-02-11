namespace Atc.Microsoft.Graph.Client.Services.Search;

public sealed class SearchGraphService : GraphServiceClientWrapper, ISearchGraphService
{
    public SearchGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<SearchResponse> Data)> ExecuteQuery(
        QueryPostRequestBody queryPostRequestBody,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryPostRequestBody);

        try
        {
            QueryPostResponse? response = null;

            await ResiliencePipeline.ExecuteAsync(
                async ct =>
                {
                    response = await Client
                        .Search
                        .Query
                        .PostAsQueryPostResponseAsync(queryPostRequestBody, cancellationToken: ct);
                    return response;
                },
                cancellationToken);

            return response?.Value is not null
                ? (HttpStatusCode.OK, response.Value)
                : (HttpStatusCode.InternalServerError, (IList<SearchResponse>)[]);
        }
        catch (ODataError odataError)
        {
            LogSearchQueryFailed(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, []);
        }
        catch (Exception ex)
        {
            LogSearchQueryFailed(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, []);
        }
    }
}