namespace Atc.Microsoft.Graph.Client.Services.OneDrive;

/// <summary>
/// Provides operations for interacting with OneDrive via the Microsoft Graph API.
/// </summary>
public interface IOneDriveGraphService
{
    /// <summary>
    /// Retrieves all drives associated with a SharePoint site.
    /// </summary>
    /// <param name="siteId">The SharePoint site identifier.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of drives.</returns>
    Task<(HttpStatusCode StatusCode, IList<Drive> Data)> GetDrivesBySiteId(
        Guid siteId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the default drive for a team (via its group identifier).
    /// </summary>
    /// <param name="teamId">The team (group) identifier.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and the drive, or null if not found.</returns>
    Task<(HttpStatusCode StatusCode, Drive? Data)> GetDriveByTeamId(
        string teamId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the latest delta token for tracking changes in a drive's items.
    /// </summary>
    /// <param name="driveId">The drive identifier.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and the delta token string, or null on failure.</returns>
    Task<(HttpStatusCode StatusCode, string? Data)> GetDeltaTokenForDriveItemsByDriveId(
        string driveId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all drive items (files and folders) from a drive's list items.
    /// </summary>
    /// <param name="driveId">The drive identifier.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of drive items.</returns>
    Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveId(
        string driveId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves drive items that have changed since a given delta token.
    /// </summary>
    /// <param name="driveId">The drive identifier.</param>
    /// <param name="deltaToken">The delta token from a previous sync.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of changed drive items.</returns>
    Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveIdAndDeltaToken(
        string driveId,
        string deltaToken,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Downloads a file's content stream from a drive.
    /// </summary>
    /// <param name="driveId">The drive identifier.</param>
    /// <param name="fileId">The file (drive item) identifier.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>The file content as a stream, or null on failure.</returns>
    Task<Stream?> DownloadFile(
        string driveId,
        string fileId,
        CancellationToken cancellationToken = default);
}