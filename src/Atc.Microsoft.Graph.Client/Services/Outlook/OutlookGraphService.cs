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

            try
            {
                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
            {
                return (HttpStatusCode.Gone, pagedItems);
            }

            LogPageIteratorTotalCount(nameof(MailFolder), count);

            return (HttpStatusCode.OK, pagedItems);
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

            try
            {
                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
            {
                return (HttpStatusCode.Gone, pagedItems);
            }

            LogPageIteratorTotalCount(nameof(MailFolder), count);

            return (HttpStatusCode.OK, pagedItems);
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

            try
            {
                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
            {
                return (HttpStatusCode.Gone, pagedItems);
            }

            LogPageIteratorTotalCount(nameof(Message), count);

            return (HttpStatusCode.OK, pagedItems);
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

            try
            {
                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
            {
                return (HttpStatusCode.Gone, pagedItems);
            }

            LogPageIteratorTotalCount(nameof(FileAttachment), count);

            return (HttpStatusCode.OK, pagedItems);
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
            await pageIterator.IterateAsync(cancellationToken);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

            await pageIterator.IterateAsync(cancellationToken);
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
            await pageIterator.IterateAsync(cancellationToken);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
        {
            await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

            await pageIterator.IterateAsync(cancellationToken);
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