namespace Atc.Microsoft.Graph.Client.Services.Calendar;

public sealed class CalendarGraphService : GraphServiceClientWrapper, ICalendarGraphService
{
    public CalendarGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<global::Microsoft.Graph.Models.Calendar> Data)> GetCalendarsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<global::Microsoft.Graph.Models.Calendar> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .Calendars
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForCalendars(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new CalendarCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<global::Microsoft.Graph.Models.Calendar, CalendarCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(global::Microsoft.Graph.Models.Calendar), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(global::Microsoft.Graph.Models.Calendar), count);

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

    public async Task<(HttpStatusCode StatusCode, IList<Event> Data)> GetEventsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<Event> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .Events
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForEvents(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new EventCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Event, EventCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Event), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(Event), count);

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

    public async Task<(HttpStatusCode StatusCode, IList<Event> Data)> GetCalendarViewByUserId(
        string userId,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<Event> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .CalendarView
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForCalendarView(
                        startDateTime,
                        endDateTime,
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new EventCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Event, EventCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Event), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(Event), count);

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