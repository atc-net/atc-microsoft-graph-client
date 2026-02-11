namespace Atc.Microsoft.Graph.Client.Services.OnlineMeetings;

public sealed class OnlineMeetingsGraphService : GraphServiceClientWrapper, IOnlineMeetingsGraphService
{
    public OnlineMeetingsGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<OnlineMeeting> Data)> GetOnlineMeetingsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<OnlineMeeting> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .OnlineMeetings
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForOnlineMeetings(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new OnlineMeetingCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<OnlineMeeting, OnlineMeetingCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(OnlineMeeting), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(OnlineMeeting), count);

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

    public async Task<(HttpStatusCode StatusCode, OnlineMeeting? Data)> GetOnlineMeetingByUserIdAndMeetingId(
        string userId,
        string meetingId,
        List<string>? expandQueryParameters = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var onlineMeeting = await Client
                .Users[userId]
                .OnlineMeetings[meetingId]
                .GetAsync(
                    RequestConfigurationFactory.CreateForOnlineMeetingById(
                        expandQueryParameters,
                        selectQueryParameters),
                    cancellationToken);

            return onlineMeeting is not null
                ? (HttpStatusCode.OK, onlineMeeting)
                : (HttpStatusCode.NotFound, null);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.NotFound)
        {
            LogOnlineMeetingNotFoundById(userId, meetingId, odataError.Error?.Message);
            return (HttpStatusCode.NotFound, null);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, OnlineMeeting? Data)> CreateOnlineMeeting(
        string userId,
        OnlineMeeting onlineMeeting,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(onlineMeeting);

        try
        {
            OnlineMeeting? result = null;

            await ResiliencePipeline.ExecuteAsync(
                async ct =>
                {
                    result = await Client
                        .Users[userId]
                        .OnlineMeetings
                        .PostAsync(onlineMeeting, cancellationToken: ct);
                    return result;
                },
                cancellationToken);

            return result is not null
                ? (HttpStatusCode.Created, result)
                : (HttpStatusCode.InternalServerError, null);
        }
        catch (ODataError odataError)
        {
            LogOnlineMeetingCreationFailed(userId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogOnlineMeetingCreationFailed(userId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, OnlineMeeting? Data)> UpdateOnlineMeeting(
        string userId,
        string meetingId,
        OnlineMeeting onlineMeeting,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(onlineMeeting);

        try
        {
            var result = await Client
                .Users[userId]
                .OnlineMeetings[meetingId]
                .PatchAsync(onlineMeeting, cancellationToken: cancellationToken);

            return result is not null
                ? (HttpStatusCode.OK, result)
                : (HttpStatusCode.InternalServerError, null);
        }
        catch (ODataError odataError)
        {
            LogOnlineMeetingUpdateFailed(userId, meetingId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogOnlineMeetingUpdateFailed(userId, meetingId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> DeleteOnlineMeeting(
        string userId,
        string meetingId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await Client
                .Users[userId]
                .OnlineMeetings[meetingId]
                .DeleteAsync(cancellationToken: cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.NotFound)
        {
            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogOnlineMeetingDeletionFailed(userId, meetingId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogOnlineMeetingDeletionFailed(userId, meetingId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }
}