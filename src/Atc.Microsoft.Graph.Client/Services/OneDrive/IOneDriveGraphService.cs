namespace Atc.Microsoft.Graph.Client.Services.OneDrive;

public interface IOneDriveGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Drive> Data)> GetDrivesBySiteId(
        Guid siteId,
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken);

    Task<Drive?> GetDriveByTeamId(
        string teamId,
        CancellationToken cancellationToken);

    Task<string?> GetDeltaTokenForDriveItemsByDriveId(
        string driveId,
        CancellationToken cancellationToken);

    Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveId(
        string driveId,
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken);

    Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveIdAndDeltaToken(
        string driveId,
        string deltaToken,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken);

    Task<Stream?> DownloadFile(
        string driveId,
        string fileId,
        CancellationToken cancellationToken);
}