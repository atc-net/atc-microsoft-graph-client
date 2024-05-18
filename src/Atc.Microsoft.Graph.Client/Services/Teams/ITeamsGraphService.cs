namespace Atc.Microsoft.Graph.Client.Services.Teams;

public interface ITeamsGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Team> Data)> GetTeams(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}