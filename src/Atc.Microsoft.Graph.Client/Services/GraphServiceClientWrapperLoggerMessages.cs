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
        Message = "Failed to retrieve data: '{ErrorMessage}'.")]
    protected partial void LogGetFailure(
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SubscriptionSetupFailed,
        Level = LogLevel.Error,
        Message = "Failed to setup subscription for the resource '{Resource}': '{ErrorMessage}'.")]
    protected partial void LogSubscriptionSetupFailed(
        string? resource,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SubscriptionRenewalFailed,
        Level = LogLevel.Error,
        Message = "Failed to renew subscription with id '{SubscriptionId}' with expirationDate '{ExpirationDate}': '{ErrorMessage}'.")]
    protected partial void LogSubscriptionRenewalFailed(
        Guid subscriptionId,
        DateTimeOffset expirationDate,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SubscriptionDeletionFailed,
        Level = LogLevel.Error,
        Message = "Failed to delete subscription with id '{SubscriptionId}': '{ErrorMessage}'.")]
    protected partial void LogSubscriptionDeletionFailed(
        Guid subscriptionId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DownloadFileFailed,
        Level = LogLevel.Error,
        Message = "Failed to download file with id: '{FileId}': '{ErrorMessage}'.")]
    protected partial void LogDownloadFileFailed(
        string fileId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.Retrying,
        Level = LogLevel.Warning,
        Message = "Retry attempt {AttemptNumber} after {RetryDelay}: '{ErrorMessage}'.")]
    protected partial void LogRetrying(
        string? errorMessage,
        int attemptNumber,
        TimeSpan retryDelay);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DownloadFileEmpty,
        Level = LogLevel.Warning,
        Message = "File to download is empty - id: '{FileId}'.")]
    protected partial void LogDownloadFileEmpty(
        string fileId);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DeltaLinkNotFoundForDrive,
        Level = LogLevel.Warning,
        Message = "Could not find Delta Link for drive with id: '{DriveId}': '{ErrorMessage}'.")]
    protected partial void LogDeltaLinkNotFoundForDrive(
        string driveId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.DriveNotFoundForTeam,
        Level = LogLevel.Warning,
        Message = "Could not find drive for team with id: '{TeamId}': '{ErrorMessage}'.")]
    protected partial void LogDriveNotFoundForTeam(
        string teamId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.PageIteratorCount,
        Level = LogLevel.Debug,
        Message = "{Area} Iterator processed {Count} items.")]
    protected partial void LogPageIteratorCount(
        string area,
        int count);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.PageIteratorTotalCount,
        Level = LogLevel.Debug,
        Message = "{Area} Iterator processed a total of {Count} items.")]
    protected partial void LogPageIteratorTotalCount(
        string area,
        int count);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.GroupNotFoundById,
        Level = LogLevel.Warning,
        Message = "Could not find group with id: '{GroupId}': '{ErrorMessage}'.")]
    protected partial void LogGroupNotFoundById(
        string groupId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.UserNotFoundById,
        Level = LogLevel.Warning,
        Message = "Could not find user with id: '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogUserNotFoundById(
        string userId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.ManagerNotFoundForUser,
        Level = LogLevel.Warning,
        Message = "Could not find manager for user with id: '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogManagerNotFoundForUser(
        string userId,
        string? errorMessage);
}