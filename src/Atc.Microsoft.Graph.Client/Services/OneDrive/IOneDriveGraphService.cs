namespace Atc.Microsoft.Graph.Client.Services.OneDrive;

public interface IOneDriveGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Drive> Data)> GetDrivesBySiteId(
        Guid siteId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<Drive?> GetDriveByTeamId(
        string teamId,
        CancellationToken cancellationToken = default);

    Task<string?> GetDeltaTokenForDriveItemsByDriveId(
        string driveId,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveId(
        string driveId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveIdAndDeltaToken(
        string driveId,
        string deltaToken,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<Stream?> DownloadFile(
        string driveId,
        string fileId,
        CancellationToken cancellationToken = default);
}