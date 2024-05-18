namespace Atc.Microsoft.Graph.Client.Services.Teams;

public interface ITeamsGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Team> Data)> GetTeams(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken = default);
}