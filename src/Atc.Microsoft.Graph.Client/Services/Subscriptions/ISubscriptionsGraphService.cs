namespace Atc.Microsoft.Graph.Client.Services.Subscriptions;

public interface ISubscriptionsGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Subscription> Data)> GetSubscriptions(
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
