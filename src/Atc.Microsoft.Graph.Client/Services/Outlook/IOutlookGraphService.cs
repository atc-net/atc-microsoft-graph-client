namespace Atc.Microsoft.Graph.Client.Services.Outlook;

public interface IOutlookGraphService
{
    Task<(HttpStatusCode StatusCode, IList<MailFolder> Data)> GetRootMailFoldersByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, IList<MailFolder> Data)> GetMailFoldersByUserIdAndFolderId(
        string userId,
        string folderId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, IList<Message> Data)> GetMessagesByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, IList<Message> Data, string? DeltaToken)> GetMessagesByUserIdAndFolderId(
        string userId,
        string folderId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        string? deltaToken = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, IList<FileAttachment> Data)> GetFileAttachmentsByUserIdAndMessageId(
        string userId,
        string messageId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}