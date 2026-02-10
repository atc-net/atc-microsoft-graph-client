namespace Atc.Microsoft.Graph.Client.Services.Outlook;

/// <summary>
/// Provides operations for interacting with Outlook mail via the Microsoft Graph API.
/// </summary>
public interface IOutlookGraphService
{
    /// <summary>
    /// Retrieves all root-level mail folders for a user.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of mail folders.</returns>
    Task<(HttpStatusCode StatusCode, IList<MailFolder> Data)> GetRootMailFoldersByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves child mail folders within a specific parent folder for a user.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="folderId">The parent mail folder identifier.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of child mail folders.</returns>
    Task<(HttpStatusCode StatusCode, IList<MailFolder> Data)> GetMailFoldersByUserIdAndFolderId(
        string userId,
        string folderId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all messages for a user.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of messages.</returns>
    Task<(HttpStatusCode StatusCode, IList<Message> Data)> GetMessagesByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves messages from a specific mail folder for a user, with optional delta tracking.
    /// When a delta token is provided, only changes since that token are returned.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="folderId">The mail folder identifier.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="deltaToken">Optional delta token from a previous sync. When null, a full sync is performed.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code, a list of messages, and a new delta token for subsequent calls.</returns>
    Task<(HttpStatusCode StatusCode, IList<Message> Data, string? DeltaToken)> GetMessagesByUserIdAndFolderId(
        string userId,
        string folderId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        string? deltaToken = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves file attachments for a specific message.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="messageId">The message identifier.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of file attachments.</returns>
    Task<(HttpStatusCode StatusCode, IList<FileAttachment> Data)> GetFileAttachmentsByUserIdAndMessageId(
        string userId,
        string messageId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}