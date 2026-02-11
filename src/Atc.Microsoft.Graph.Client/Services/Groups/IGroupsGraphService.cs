namespace Atc.Microsoft.Graph.Client.Services.Groups;

/// <summary>
/// Provides operations for managing Microsoft Graph groups.
/// </summary>
public interface IGroupsGraphService
{
    /// <summary>
    /// Retrieves all groups from Microsoft Graph.
    /// </summary>
    /// <param name="expandQueryParameters">Optional expand query parameters.</param>
    /// <param name="filterQueryParameter">Optional filter query parameter.</param>
    /// <param name="selectQueryParameters">Optional select query parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A tuple containing the HTTP status code and the list of groups.</returns>
    Task<(HttpStatusCode StatusCode, IList<Group> Data)> GetGroups(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a group by its identifier.
    /// </summary>
    /// <param name="groupId">The group identifier.</param>
    /// <param name="expandQueryParameters">Optional expand query parameters.</param>
    /// <param name="selectQueryParameters">Optional select query parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A tuple containing the HTTP status code and the group, if found.</returns>
    Task<(HttpStatusCode StatusCode, Group? Data)> GetGroupById(
        string groupId,
        List<string>? expandQueryParameters = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the members of a group by the group identifier.
    /// </summary>
    /// <param name="groupId">The group identifier.</param>
    /// <param name="expandQueryParameters">Optional expand query parameters.</param>
    /// <param name="filterQueryParameter">Optional filter query parameter.</param>
    /// <param name="selectQueryParameters">Optional select query parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A tuple containing the HTTP status code and the list of directory objects representing group members.</returns>
    Task<(HttpStatusCode StatusCode, IList<DirectoryObject> Data)> GetGroupMembersByGroupId(
        string groupId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the owners of a group by the group identifier.
    /// </summary>
    /// <param name="groupId">The group identifier.</param>
    /// <param name="expandQueryParameters">Optional expand query parameters.</param>
    /// <param name="filterQueryParameter">Optional filter query parameter.</param>
    /// <param name="selectQueryParameters">Optional select query parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A tuple containing the HTTP status code and the list of directory objects representing group owners.</returns>
    Task<(HttpStatusCode StatusCode, IList<DirectoryObject> Data)> GetGroupOwnersByGroupId(
        string groupId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}