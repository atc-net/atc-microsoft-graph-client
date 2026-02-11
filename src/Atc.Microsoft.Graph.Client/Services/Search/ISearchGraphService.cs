namespace Atc.Microsoft.Graph.Client.Services.Search;

public interface ISearchGraphService
{
    Task<(HttpStatusCode StatusCode, IList<SearchResponse> Data)> ExecuteQuery(
        QueryPostRequestBody queryPostRequestBody,
        CancellationToken cancellationToken = default);
}
