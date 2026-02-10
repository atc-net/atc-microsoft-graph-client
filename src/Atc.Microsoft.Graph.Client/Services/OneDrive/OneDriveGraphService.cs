namespace Atc.Microsoft.Graph.Client.Services.OneDrive;

public sealed class OneDriveGraphService : GraphServiceClientWrapper, IOneDriveGraphService
{
    public OneDriveGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<Drive> Data)> GetDrivesBySiteId(
        Guid siteId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<Drive> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Sites[siteId.ToString()]
                .Drives
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForDrives(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new DriveCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Drive, DriveCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Drive), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(Drive), count);

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

    public async Task<Drive?> GetDriveByTeamId(
        string teamId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var drive = await Client
                .Groups[teamId]
                .Drive
                .GetAsync(cancellationToken: cancellationToken);

            if (drive is not null)
            {
                return drive;
            }

            LogDriveNotFoundForTeam(teamId, errorMessage: null);
            return null;
        }
        catch (ODataError odataError)
        {
            LogDriveNotFoundForTeam(teamId, odataError.Error?.Message);
            return null;
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return null;
        }
    }

    public async Task<string?> GetDeltaTokenForDriveItemsByDriveId(
        string driveId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var deltaWithTokenResponse = await Client
                .Drives[driveId]
                .Items["root"]
                .DeltaWithToken("latest")
                .GetAsDeltaWithTokenGetResponseAsync(cancellationToken: cancellationToken);

            if (deltaWithTokenResponse?.OdataDeltaLink is null)
            {
                LogDeltaLinkNotFoundForDrive(driveId, errorMessage: null);
                return null;
            }

            var sa = deltaWithTokenResponse.OdataDeltaLink.Split("token='", StringSplitOptions.RemoveEmptyEntries);
            if (sa.Length == 2)
            {
                return sa[1].Replace("')", string.Empty, StringComparison.OrdinalIgnoreCase);
            }

            LogDeltaLinkNotFoundForDrive(driveId, errorMessage: null);
            return null;
        }
        catch (ODataError odataError)
        {
            LogDeltaLinkNotFoundForDrive(driveId, odataError.Error?.Message);
            return null;
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return null;
        }
    }

    public async Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveId(
        string driveId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        var requestInformation = Client
            .Drives[driveId]
            .List
            .Items
            .ToGetRequestInformation(
                RequestConfigurationFactory.CreateForItems(
                    expandQueryParameters,
                    filterQueryParameter,
                    selectQueryParameters));

        var (httpStatusCode, data) = await GetAllListItemsByDriveId(requestInformation, cancellationToken);

        var driveItems = await data
            .Where(x => x.DriveItem is not null)
            .Select(x => x.DriveItem!)
            .ToListAsync(cancellationToken);

        return (httpStatusCode, driveItems);
    }

    public async Task<(HttpStatusCode StatusCode, IList<DriveItem> Data)> GetDriveItemsByDriveIdAndDeltaToken(
        string driveId,
        string deltaToken,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<DriveItem> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Drives[driveId]
                .Items["root"]
                .DeltaWithToken(deltaToken)
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForItemsWithDelta(
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new DriveItemCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<DriveItem, DriveItemCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(DriveItem), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(DriveItem), count);

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

    public async Task<Stream?> DownloadFile(
        string driveId,
        string fileId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await ResiliencePipeline.ExecuteAsync(
                async context =>
                {
                    var stream = await Client
                        .Drives[driveId]
                        .Items[fileId]
                        .Content
                        .GetAsync(cancellationToken: context);

                    if (stream is null)
                    {
                        LogDownloadFileEmpty(fileId);
                    }

                    return stream;
                },
                cancellationToken);
        }
        catch (Exception ex)
        {
            LogDownloadFileFailed(fileId, ex.GetLastInnerMessage());
            return null;
        }
    }

    private async Task<(HttpStatusCode StatusCode, IList<ListItem> Data)> GetAllListItemsByDriveId(
        RequestInformation requestInformation,
        CancellationToken cancellationToken)
    {
        List<ListItem> pagedItems = [];
        var count = 0;

        try
        {
            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new ListItemCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<ListItem, ListItemCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(ListItem), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(ListItem), count);

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
            var errorMessage = ex.GetLastInnerMessage();
            LogGetFailure(errorMessage);
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
    }
}