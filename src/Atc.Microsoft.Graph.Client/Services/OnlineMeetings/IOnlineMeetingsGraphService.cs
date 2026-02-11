namespace Atc.Microsoft.Graph.Client.Services.OnlineMeetings;

public interface IOnlineMeetingsGraphService
{
    Task<(HttpStatusCode StatusCode, IList<OnlineMeeting> Data)> GetOnlineMeetingsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, OnlineMeeting? Data)> GetOnlineMeetingByUserIdAndMeetingId(
        string userId,
        string meetingId,
        List<string>? expandQueryParameters = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, OnlineMeeting? Data)> CreateOnlineMeeting(
        string userId,
        OnlineMeeting onlineMeeting,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, OnlineMeeting? Data)> UpdateOnlineMeeting(
        string userId,
        string meetingId,
        OnlineMeeting onlineMeeting,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, bool Succeeded)> DeleteOnlineMeeting(
        string userId,
        string meetingId,
        CancellationToken cancellationToken = default);
}
