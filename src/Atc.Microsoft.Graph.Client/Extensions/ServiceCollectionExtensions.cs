namespace Atc.Microsoft.Graph.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMicrosoftGraphServiceClient(
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

        services.AddSingleton<IGraphServiceClientWrapper>(s => new GraphServiceClientWrapper(
            s.GetRequiredService<ILoggerFactory>(),
            s.GetRequiredService<GraphServiceClient>()));

        return services;
    }
}
