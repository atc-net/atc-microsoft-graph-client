// ReSharper disable StringLiteralTypo
var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();

services.AddLogging(configure => configure.AddConsole());

var graphServiceOptions = new GraphServiceOptions();
configuration.GetRequiredSection("GraphServiceOptions").Bind(graphServiceOptions);

services.AddMicrosoftGraphServices(graphServiceOptions);

var serviceProvider = services.BuildServiceProvider();

var sharepointService = serviceProvider.GetRequiredService<ISharepointGraphService>();

using var cts = new CancellationTokenSource();

var (statusCode, sites) = await sharepointService.GetSites(
    selectQueryParameters: ["id", "webUrl", "isPersonalSite"],
    cancellationToken: cts.Token);

if (statusCode != HttpStatusCode.OK)
{
    Console.WriteLine("Failed to retrieve sites.");
    return;
}

foreach (var site in sites)
{
    Console.WriteLine($"SiteId: {site.Id}");
    Console.WriteLine($"WebUrl: {site.WebUrl}");
    Console.WriteLine($"IsPersonalSite: {site.IsPersonalSite}");
    Console.WriteLine("--------------------------------------");
}