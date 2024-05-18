namespace Atc.Microsoft.Graph.Client.Services;

/// <summary>
/// GraphServiceClientWrapper LoggerMessages.
/// </summary>
[SuppressMessage("Design", "MA0048:File name must match type name", Justification = "OK - By Design")]
public abstract partial class GraphServiceClientWrapper
{
    private readonly ILogger<GraphServiceClientWrapper> logger;

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.GetFailure,
        Level = LogLevel.Error,
        Message = "{callerMethodName}({callerLineNumber}) - Failed to retrieve data: '{errorMessage}'.")]
    protected partial void LogGetFailure(
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SubscriptionSetupFailed,
        Level = LogLevel.Error,
        Message = "{callerMethodName}({callerLineNumber}) - Failed to setup subscription for the resource '{resource}': '{errorMessage}'.")]
    protected partial void LogSubscriptionSetupFailed(
        string? resource,
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SubscriptionRenewalFailed,
        Level = LogLevel.Error,
        Message = "{callerMethodName}({callerLineNumber}) - Failed to renew subscription with id '{subscriptionId}' with expirationDate '{expirationDate}': '{errorMessage}'.")]
    protected partial void LogSubscriptionRenewalFailed(
        Guid subscriptionId,
        DateTimeOffset expirationDate,
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SubscriptionDeletionFailed,
        Level = LogLevel.Error,
        Message = "{callerMethodName}({callerLineNumber}) - Failed to delete subscription with id '{subscriptionId}': '{errorMessage}'.")]
    protected partial void LogSubscriptionDeletionFailed(
        Guid subscriptionId,
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DownloadFileFailed,
        Level = LogLevel.Error,
        Message = "{callerMethodName}({callerLineNumber}) - Failed to download file with id: '{fileId}': '{errorMessage}'.")]
    protected partial void LogDownloadFileFailed(
        string fileId,
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DownloadFileRetrying,
        Level = LogLevel.Warning,
        Message = "{callerMethodName}({callerLineNumber}) - Retrying download of file: '{errorMessage}'.")]
    protected partial void LogDownloadFileRetrying(
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DownloadFileEmpty,
        Level = LogLevel.Warning,
        Message = "{callerMethodName}({callerLineNumber}) - File to download is empty - id: '{fileId}'.")]
    protected partial void LogDownloadFileEmpty(
        string fileId,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DeltaLinkNotFoundForDrive,
        Level = LogLevel.Warning,
        Message = "{callerMethodName}({callerLineNumber}) - Could not find Delta Link for drive with id: '{driveId}': '{errorMessage}'.")]
    protected partial void LogDeltaLinkNotFoundForDrive(
        string driveId,
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DriveNotFoundForTeam,
        Level = LogLevel.Warning,
        Message = "{callerMethodName}({callerLineNumber}) - Could not find drive for team with id: '{teamId}': '{errorMessage}'.")]
    protected partial void LogDriveNotFoundForTeam(
        string teamId,
        string? errorMessage,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.PageIteratorCount,
        Level = LogLevel.Debug,
        Message = "{callerMethodName}({callerLineNumber}) - {area} Iterator processed {count} items.")]
    protected partial void LogPageIteratorCount(
        string area,
        int count,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.PageIteratorTotalCount,
        Level = LogLevel.Debug,
        Message = "{callerMethodName}({callerLineNumber}) - {area} Iterator processed a total of {count} items.")]
    protected partial void LogPageIteratorTotalCount(
        string area,
        int count,
        [CallerMemberName] string callerMethodName = "",
        [CallerLineNumber] int callerLineNumber = 0);
}