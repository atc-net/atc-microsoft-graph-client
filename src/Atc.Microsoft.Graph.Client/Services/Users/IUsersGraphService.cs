namespace Atc.Microsoft.Graph.Client.Services.Users;

public interface IUsersGraphService
{
    Task<(HttpStatusCode StatusCode, IList<User> Data)> GetUsers(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}