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
        Func<IServiceProvider, GraphServiceClient> factory = (serviceProvider)
            => graphServiceClient ?? serviceProvider.GetRequiredService<GraphServiceClient>();

        services.AddSingleton(factory);

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
        services.AddGraphService<IOneDriveGraphService, OneDriveGraphService>();
        services.AddGraphService<IOutlookGraphService, OutlookGraphService>();
        services.AddGraphService<ISharepointGraphService, SharepointGraphService>();
        services.AddGraphService<ITeamsGraphService, TeamsGraphService>();
        services.AddGraphService<IUsersGraphService, UsersGraphService>();
    }

    private static void AddGraphService<TService, TImplementation>(
        this IServiceCollection services)
        where TService : class
        where TImplementation : GraphServiceClientWrapper, TService
    {
        services.AddSingleton<TService, TImplementation>(s =>
        {
            var loggerFactory = s.GetService<ILoggerFactory>() ?? new NullLoggerFactory();
            var graphServiceClient = s.GetRequiredService<GraphServiceClient>();

            return (TImplementation)Activator.CreateInstance(
                typeof(TImplementation),
                loggerFactory,
                graphServiceClient)!;
        });
    }
}