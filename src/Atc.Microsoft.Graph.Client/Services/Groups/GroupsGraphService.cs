namespace Atc.Microsoft.Graph.Client.Services.Groups;

public sealed class GroupsGraphService : GraphServiceClientWrapper, IGroupsGraphService
{
    public GroupsGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<Group> Data)> GetGroups(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<Group> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Groups
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForGroups(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new GroupCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Group, GroupCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Group), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(Group), count);

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

    public async Task<(HttpStatusCode StatusCode, Group? Data)> GetGroupById(
        string groupId,
        List<string>? expandQueryParameters = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var group = await Client
                .Groups[groupId]
                .GetAsync(
                    RequestConfigurationFactory.CreateForGroupById(
                        expandQueryParameters,
                        selectQueryParameters),
                    cancellationToken: cancellationToken);

            if (group is not null)
            {
                return (HttpStatusCode.OK, group);
            }

            LogGroupNotFoundById(groupId, errorMessage: null);
            return (HttpStatusCode.NotFound, null);
        }
        catch (ODataError odataError)
        {
            LogGroupNotFoundById(groupId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, IList<DirectoryObject> Data)> GetGroupMembersByGroupId(
        string groupId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<DirectoryObject> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Groups[groupId]
                .Members
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForGroupMembers(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new DirectoryObjectCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<DirectoryObject, DirectoryObjectCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(DirectoryObject), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(DirectoryObject), count);

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

    public async Task<(HttpStatusCode StatusCode, IList<DirectoryObject> Data)> GetGroupOwnersByGroupId(
        string groupId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<DirectoryObject> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Groups[groupId]
                .Owners
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForGroupOwners(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new DirectoryObjectCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<DirectoryObject, DirectoryObjectCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(DirectoryObject), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(DirectoryObject), count);

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
}