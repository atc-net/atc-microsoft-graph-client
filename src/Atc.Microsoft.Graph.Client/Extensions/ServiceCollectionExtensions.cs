// ReSharper disable ConvertToLocalFunction
namespace Atc.Microsoft.Graph.Client.Extensions;

public static class ServiceCollectionExtensions
{
    private static readonly string[] DefaultScopes = ["https://graph.microsoft.com/.default"];

    /// <summary>
    /// Adds the <see cref="GraphServiceClient"/> to the service collection.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance to augment.</param>
    /// <param name="graphServiceClient"><see cref="GraphServiceClient"/> to use for the service. If null, one must be available in the service provider when this service is resolved.</param>
    /// <returns>The same instance as <paramref name="services"/>.</returns>
    public static IServiceCollection AddMicrosoftGraphServices(
        this IServiceCollection services,
        GraphServiceClient? graphServiceClient = null)
    {
        if (graphServiceClient is not null)
        {
            services.AddSingleton(graphServiceClient);
        }

        RegisterGraphServices(services);

        return services;
    }

    /// <summary>
    /// Adds the <see cref="GraphServiceClient"/> to the service collection using the provided <see cref="TokenCredential"/> and optional scopes.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance to augment.</param>
    /// <param name="tokenCredential">The <see cref="TokenCredential"/> to use for authentication.</param>
    /// <param name="scopes">Optional array of scopes for the <see cref="GraphServiceClient"/>.</param>
    /// <returns>The same instance as <paramref name="services"/>.</returns>
    public static IServiceCollection AddMicrosoftGraphServices(
        this IServiceCollection services,
        TokenCredential tokenCredential,
        string[]? scopes = null)
    {
        ArgumentNullException.ThrowIfNull(tokenCredential);

        services.AddSingleton(_ => new GraphServiceClient(tokenCredential, scopes ?? DefaultScopes));

        RegisterGraphServices(services);

        return services;
    }

    /// <summary>
    /// Adds the <see cref="GraphServiceClient"/> to the service collection using the provided <see cref="GraphServiceOptions"/> and optional scopes.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> instance to augment.</param>
    /// <param name="graphServiceOptions">The <see cref="GraphServiceOptions"/> containing configuration for the service.</param>
    /// <param name="scopes">Optional array of scopes for the <see cref="GraphServiceClient"/>.</param>
    /// <returns>The same instance as <paramref name="services"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown if the <paramref name="graphServiceOptions"/> are invalid.</exception>
    public static IServiceCollection AddMicrosoftGraphServices(
        this IServiceCollection services,
        GraphServiceOptions graphServiceOptions,
        string[]? scopes = null)
    {
        ArgumentNullException.ThrowIfNull(graphServiceOptions);

        if (!graphServiceOptions.IsValid())
        {
            throw new InvalidOperationException($"Required service '{nameof(GraphServiceOptions)}' is not registered");
        }

        services.AddSingleton(_ =>
        {
            var options = new TokenCredentialOptions
            {
                AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
            };

            var clientSecretCredential = new ClientSecretCredential(
                graphServiceOptions.TenantId,
                graphServiceOptions.ClientId,
                graphServiceOptions.ClientSecret,
                options);

            return new GraphServiceClient(clientSecretCredential, scopes ?? DefaultScopes);
        });

        RegisterGraphServices(services);

        return services;
    }

    private static void RegisterGraphServices(IServiceCollection services)
    {
        services.AddGraphService<ICalendarGraphService, CalendarGraphService>(
            (loggerFactory, graphServiceClient) => new CalendarGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<IContactsGraphService, ContactsGraphService>(
            (loggerFactory, graphServiceClient) => new ContactsGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<IGroupsGraphService, GroupsGraphService>(
            (loggerFactory, graphServiceClient) => new GroupsGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<IOneDriveGraphService, OneDriveGraphService>(
            (loggerFactory, graphServiceClient) => new OneDriveGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<IOnlineMeetingsGraphService, OnlineMeetingsGraphService>(
            (loggerFactory, graphServiceClient) => new OnlineMeetingsGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<IOutlookGraphService, OutlookGraphService>(
            (loggerFactory, graphServiceClient) => new OutlookGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<ISearchGraphService, SearchGraphService>(
            (loggerFactory, graphServiceClient) => new SearchGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<ISharepointGraphService, SharepointGraphService>(
            (loggerFactory, graphServiceClient) => new SharepointGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<ISubscriptionsGraphService, SubscriptionsGraphService>(
            (loggerFactory, graphServiceClient) => new SubscriptionsGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<ITeamsGraphService, TeamsGraphService>(
            (loggerFactory, graphServiceClient) => new TeamsGraphService(loggerFactory, graphServiceClient));
        services.AddGraphService<IUsersGraphService, UsersGraphService>(
            (loggerFactory, graphServiceClient) => new UsersGraphService(loggerFactory, graphServiceClient));
    }

    private static void AddGraphService<TService, TImplementation>(
        this IServiceCollection services,
        Func<ILoggerFactory, GraphServiceClient, TImplementation> factory)
        where TService : class
        where TImplementation : GraphServiceClientWrapper, TService
    {
        services.AddSingleton<TService>(s =>
        {
            var loggerFactory = s.GetService<ILoggerFactory>() ?? new NullLoggerFactory();
            var graphServiceClient = s.GetRequiredService<GraphServiceClient>();
            return factory(loggerFactory, graphServiceClient);
        });
    }
}