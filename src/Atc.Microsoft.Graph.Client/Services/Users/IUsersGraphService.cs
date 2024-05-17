namespace Atc.Microsoft.Graph.Client.Services.Users;

public interface IUsersGraphService
{
    Task<(HttpStatusCode StatusCode, IList<User> Data)> GetUsers(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken);
}