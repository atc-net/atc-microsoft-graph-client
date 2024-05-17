namespace Atc.Microsoft.Graph.Client.Services.Sharepoint;

public interface ISharepointGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Site> Data)> GetSites(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken);
}