namespace Atc.Microsoft.Graph.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMicrosoftGraphServices(
        this IServiceCollection services,
        GraphServiceOptions graphServiceOptions)
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };

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

            return new GraphServiceClient(clientSecretCredential, scopes);
        });

        services.AddGraphService<IOneDriveGraphService, OneDriveGraphService>();
        services.AddGraphService<IOutlookGraphService, OutlookGraphService>();
        services.AddGraphService<ISharepointGraphService, SharepointGraphService>();
        services.AddGraphService<ITeamsGraphService, TeamsGraphService>();
        services.AddGraphService<IUsersGraphService, UsersGraphService>();

        return services;
    }

    private static IServiceCollection AddGraphService<TService, TImplementation>(
        this IServiceCollection services)
        where TService : class
        where TImplementation : GraphServiceClientWrapper, TService
    {
        services.AddSingleton<TService, TImplementation>(s => (TImplementation)Activator.CreateInstance(
            typeof(TImplementation),
            s.GetRequiredService<ILoggerFactory>(),
            s.GetRequiredService<GraphServiceClient>())!);

        return services;
    }
}