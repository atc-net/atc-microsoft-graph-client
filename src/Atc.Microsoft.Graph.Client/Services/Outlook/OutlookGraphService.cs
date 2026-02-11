namespace Atc.Microsoft.Graph.Client.Services.Outlook;

public sealed class OutlookGraphService : GraphServiceClientWrapper, IOutlookGraphService
{
    public OutlookGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<MailFolder> Data)> GetRootMailFoldersByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<MailFolder> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .MailFolders
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForMailFolders(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new MailFolderCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<MailFolder, MailFolderCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(MailFolder), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(MailFolder), count);

            return (HttpStatusCode.OK, pagedItems);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
        {
            return (HttpStatusCode.Gone, pagedItems);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
    }

    public async Task<(HttpStatusCode StatusCode, IList<MailFolder> Data)> GetMailFoldersByUserIdAndFolderId(
        string userId,
        string folderId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<MailFolder> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .MailFolders[folderId]
                .ChildFolders
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForChildFolders(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new MailFolderCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<MailFolder, MailFolderCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(MailFolder), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(MailFolder), count);

            return (HttpStatusCode.OK, pagedItems);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
        {
            return (HttpStatusCode.Gone, pagedItems);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
    }

    public async Task<(HttpStatusCode StatusCode, IList<Message> Data)> GetMessagesByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<Message> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .Messages
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForMessagesUserId(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new MessageCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Message, MessageCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Message), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(Message), count);

            return (HttpStatusCode.OK, pagedItems);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
        {
            return (HttpStatusCode.Gone, pagedItems);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
    }

    public async Task<(HttpStatusCode StatusCode, IList<Message> Data, string? DeltaToken)> GetMessagesByUserIdAndFolderId(
        string userId,
        string folderId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        string? deltaToken = null,
        CancellationToken cancellationToken = default)
    {
        List<Message> pagedItems = [];

        try
        {
            return string.IsNullOrEmpty(deltaToken)
                ? await GetMessagesByUserIdAndFolderIdWithoutDeltaToken(
                    userId,
                    folderId,
                    filterQueryParameter,
                    selectQueryParameters,
                    cancellationToken)
                : await GetMessagesByUserIdAndFolderIdWithDeltaToken(
                    userId,
                    folderId,
                    deltaToken,
                    expandQueryParameters,
                    filterQueryParameter,
                    selectQueryParameters,
                    cancellationToken);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, IList<FileAttachment> Data)> GetFileAttachmentsByUserIdAndMessageId(
        string userId,
        string messageId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<FileAttachment> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .Messages[messageId]
                .Attachments
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForAttachments(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new AttachmentCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Attachment, AttachmentCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    if (item is FileAttachment attachment)
                    {
                        pagedItems.Add(attachment);
                    }

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(FileAttachment), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(FileAttachment), count);

            return (HttpStatusCode.OK, pagedItems);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
        {
            return (HttpStatusCode.Gone, pagedItems);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> SendMail(
        string userId,
        Message message,
        bool saveToSentItems = true,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        try
        {
            await ResiliencePipeline.ExecuteAsync(
                async ct => await Client.Users[userId].SendMail.PostAsync(
                    new SendMailPostRequestBody
                    {
                        Message = message,
                        SaveToSentItems = saveToSentItems,
                    },
                    cancellationToken: ct),
                cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogMailSendFailed(userId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogMailSendFailed(userId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }

    public async Task<(HttpStatusCode StatusCode, Message? Data)> CreateDraftMessage(
        string userId,
        Message message,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(message);

        try
        {
            Message? result = null;

            await ResiliencePipeline.ExecuteAsync(
                async ct =>
                {
                    result = await Client
                        .Users[userId]
                        .Messages
                        .PostAsync(message, cancellationToken: ct);
                    return result;
                },
                cancellationToken);

            return result is not null
                ? (HttpStatusCode.Created, result)
                : (HttpStatusCode.InternalServerError, null);
        }
        catch (ODataError odataError)
        {
            LogMailDraftCreationFailed(userId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogMailDraftCreationFailed(userId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> SendDraftMessage(
        string userId,
        string messageId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ResiliencePipeline.ExecuteAsync(
                async ct => await Client.Users[userId].Messages[messageId].Send.PostAsync(cancellationToken: ct),
                cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogMailDraftSendFailed(userId, messageId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogMailDraftSendFailed(userId, messageId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> ReplyToMessage(
        string userId,
        string messageId,
        string comment,
        Message? responseMessage = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ResiliencePipeline.ExecuteAsync(
                async ct => await Client.Users[userId].Messages[messageId].Reply.PostAsync(
                    new ReplyPostRequestBody
                    {
                        Comment = comment,
                        Message = responseMessage,
                    },
                    cancellationToken: ct),
                cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogReplyToMessageFailed(userId, messageId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogReplyToMessageFailed(userId, messageId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> ReplyAllToMessage(
        string userId,
        string messageId,
        string comment,
        Message? responseMessage = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await ResiliencePipeline.ExecuteAsync(
                async ct => await Client.Users[userId].Messages[messageId].ReplyAll.PostAsync(
                    new ReplyAllPostRequestBody
                    {
                        Comment = comment,
                        Message = responseMessage,
                    },
                    cancellationToken: ct),
                cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogReplyToMessageFailed(userId, messageId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogReplyToMessageFailed(userId, messageId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> ForwardMessage(
        string userId,
        string messageId,
        string comment,
        List<Recipient> toRecipients,
        Message? forwardMessage = null,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(toRecipients);

        try
        {
            await ResiliencePipeline.ExecuteAsync(
                async ct => await Client.Users[userId].Messages[messageId].Forward.PostAsync(
                    new ForwardPostRequestBody
                    {
                        Comment = comment,
                        ToRecipients = toRecipients,
                        Message = forwardMessage,
                    },
                    cancellationToken: ct),
                cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogForwardMessageFailed(userId, messageId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogForwardMessageFailed(userId, messageId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }

    private async Task<(HttpStatusCode StatusCode, IList<Message> Data, string? DeltaToken)> GetMessagesByUserIdAndFolderIdWithoutDeltaToken(
        string userId,
        string folderId,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken = default)
    {
        List<Message> pagedItems = [];
        var count = 0;

        var requestInformation = Client
            .Users[userId]
            .MailFolders[folderId]
            .Messages
            .Delta // To get a delta Link included in the response
            .ToGetRequestInformation(
                RequestConfigurationFactory.CreateForMessagesDelta(
                    filterQueryParameter,
                    selectQueryParameters));

        var response = await Client.RequestAdapter.SendAsync(
            requestInformation,
            (_) => new MessageCollectionResponse(),
            cancellationToken: cancellationToken);

        if (response is null)
        {
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }

        var pageIterator = PageIterator<Message, MessageCollectionResponse>.CreatePageIterator(
            Client,
            response,
            item =>
            {
                pagedItems.Add(item);

                count++;
                if (count % 1000 == 0)
                {
                    LogPageIteratorCount(nameof(Message), count);
                }

                return true;
            });

        try
        {
            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
        {
            return (HttpStatusCode.Gone, pagedItems, null);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }

        LogPageIteratorTotalCount(nameof(Message), count);

        if (!response.AdditionalData.TryGetValue("@odata.deltaLink", out var deltaLink) ||
            deltaLink is null)
        {
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }

        var sa = deltaLink.ToString()!.Split("deltatoken=", StringSplitOptions.RemoveEmptyEntries);
        return sa.Length == 2
            ? (HttpStatusCode.OK, pagedItems, sa[1])
            : (HttpStatusCode.InternalServerError, pagedItems, null);
    }

    private async Task<(HttpStatusCode StatusCode, IList<Message> Data, string? DeltaToken)> GetMessagesByUserIdAndFolderIdWithDeltaToken(
        string userId,
        string folderId,
        string deltaToken,
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken = default)
    {
        List<Message> pagedItems = [];
        var count = 0;

        var url = Client
                .Users[userId]
                .MailFolders[folderId]
                .Messages
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForMessagesMailFolder(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters))
                .URI
                .ToString();

        var saUrl = url.Split('?', StringSplitOptions.RemoveEmptyEntries);
        url = saUrl.Length == 1
            ? $"{url}/delta?$skipToken={deltaToken}"
            : $"{saUrl[0]}/delta?$skipToken={deltaToken}&{saUrl[1]}";

        var deltaRequestBuilder = new global::Microsoft.Graph.Users.Item.MailFolders.Item.Messages.Delta.DeltaRequestBuilder(
            url,
            Client.RequestAdapter);

        var response = await deltaRequestBuilder.GetAsDeltaGetResponseAsync(cancellationToken: cancellationToken);
        if (response is null)
        {
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }

        var pageIterator = PageIterator<Message, global::Microsoft.Graph.Users.Item.MailFolders.Item.Messages.Delta.DeltaGetResponse>.CreatePageIterator(
            Client,
            response,
            item =>
            {
                pagedItems.Add(item);

                count++;
                if (count % 1000 == 0)
                {
                    LogPageIteratorCount(nameof(Message), count);
                }

                return true;
            });

        try
        {
            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
        {
            return (HttpStatusCode.Gone, pagedItems, null);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }

        LogPageIteratorTotalCount(nameof(Message), count);

        if (string.IsNullOrEmpty(response.OdataDeltaLink))
        {
            return (HttpStatusCode.InternalServerError, pagedItems, null);
        }

        var sa = response.OdataDeltaLink.Split("deltatoken=", StringSplitOptions.RemoveEmptyEntries);
        return sa.Length == 2
            ? (HttpStatusCode.OK, pagedItems, sa[1])
            : (HttpStatusCode.InternalServerError, pagedItems, null);
    }
}