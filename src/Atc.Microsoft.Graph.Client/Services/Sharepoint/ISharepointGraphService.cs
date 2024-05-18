namespace Atc.Microsoft.Graph.Client.Services.Sharepoint;

public interface ISharepointGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Site> Data)> GetSites(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, Guid? SubscriptionId)> SetupSubscription(
        Subscription subscription,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, bool Succeeded)> RenewSubscription(
        Guid subscriptionId,
        DateTimeOffset expirationDate,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, bool Succeeded)> DeleteSubscription(
        Guid subscriptionId,
        CancellationToken cancellationToken = default);
}