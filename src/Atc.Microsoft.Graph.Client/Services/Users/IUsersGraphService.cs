namespace Atc.Microsoft.Graph.Client.Services.Users;

/// <summary>
/// Provides operations for managing Microsoft Graph users.
/// </summary>
public interface IUsersGraphService
{
    /// <summary>
    /// Retrieves all users from the Microsoft Graph directory, with optional OData query parameters.
    /// </summary>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of users.</returns>
    Task<(HttpStatusCode StatusCode, IList<User> Data)> GetUsers(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}