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

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.MailSendFailed,
        Level = LogLevel.Error,
        Message = "Failed to send mail for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogMailSendFailed(
        string userId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.MailDraftCreationFailed,
        Level = LogLevel.Error,
        Message = "Failed to create draft message for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogMailDraftCreationFailed(
        string userId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.MailDraftSendFailed,
        Level = LogLevel.Error,
        Message = "Failed to send draft message '{MessageId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogMailDraftSendFailed(
        string userId,
        string messageId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.ReplyToMessageFailed,
        Level = LogLevel.Error,
        Message = "Failed to reply to message '{MessageId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogReplyToMessageFailed(
        string userId,
        string messageId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.ForwardMessageFailed,
        Level = LogLevel.Error,
        Message = "Failed to forward message '{MessageId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogForwardMessageFailed(
        string userId,
        string messageId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.OnlineMeetingNotFoundById,
        Level = LogLevel.Warning,
        Message = "Could not find online meeting with id '{MeetingId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogOnlineMeetingNotFoundById(
        string userId,
        string meetingId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.OnlineMeetingCreationFailed,
        Level = LogLevel.Error,
        Message = "Failed to create online meeting for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogOnlineMeetingCreationFailed(
        string userId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.OnlineMeetingUpdateFailed,
        Level = LogLevel.Error,
        Message = "Failed to update online meeting '{MeetingId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogOnlineMeetingUpdateFailed(
        string userId,
        string meetingId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.OnlineMeetingDeletionFailed,
        Level = LogLevel.Error,
        Message = "Failed to delete online meeting '{MeetingId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogOnlineMeetingDeletionFailed(
        string userId,
        string meetingId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.ContactNotFoundById,
        Level = LogLevel.Warning,
        Message = "Could not find contact with id '{ContactId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogContactNotFoundById(
        string userId,
        string contactId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.ContactCreationFailed,
        Level = LogLevel.Error,
        Message = "Failed to create contact for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogContactCreationFailed(
        string userId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.ContactUpdateFailed,
        Level = LogLevel.Error,
        Message = "Failed to update contact '{ContactId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogContactUpdateFailed(
        string userId,
        string contactId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.ContactDeletionFailed,
        Level = LogLevel.Error,
        Message = "Failed to delete contact '{ContactId}' for user '{UserId}': '{ErrorMessage}'.")]
    protected partial void LogContactDeletionFailed(
        string userId,
        string contactId,
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SubscriptionListFailed,
        Level = LogLevel.Error,
        Message = "Failed to retrieve subscriptions: '{ErrorMessage}'.")]
    protected partial void LogSubscriptionListFailed(
        string? errorMessage);

    [LoggerMessage(
        EventId = LoggingEventIdConstants.GraphServiceClientWrapper.SearchQueryFailed,
        Level = LogLevel.Error,
        Message = "Failed to execute search query: '{ErrorMessage}'.")]
    protected partial void LogSearchQueryFailed(
        string? errorMessage);
}