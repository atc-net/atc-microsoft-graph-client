namespace Atc.Microsoft.Graph.Client.Services.Calendar;

/// <summary>
/// Provides operations for managing Microsoft Graph calendars and events.
/// </summary>
public interface ICalendarGraphService
{
    /// <summary>
    /// Retrieves all calendars for a user.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of calendars.</returns>
    Task<(HttpStatusCode StatusCode, IList<global::Microsoft.Graph.Models.Calendar> Data)> GetCalendarsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all events for a user.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of events.</returns>
    Task<(HttpStatusCode StatusCode, IList<Event> Data)> GetEventsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves the calendar view (events within a date range) for a user.
    /// </summary>
    /// <param name="userId">The user identifier or user principal name.</param>
    /// <param name="startDateTime">The start date and time of the calendar view range.</param>
    /// <param name="endDateTime">The end date and time of the calendar view range.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of events.</returns>
    Task<(HttpStatusCode StatusCode, IList<Event> Data)> GetCalendarViewByUserId(
        string userId,
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}