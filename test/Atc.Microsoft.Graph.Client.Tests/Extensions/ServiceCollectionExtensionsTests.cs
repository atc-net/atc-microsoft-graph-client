namespace Atc.Microsoft.Graph.Client.Tests.Extensions;

public sealed class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddMicrosoftGraphServices_WithGraphServiceClient_RegistersAllServices()
    {
        // Arrange
        var requestAdapter = Substitute.For<IRequestAdapter>();
        using var graphServiceClient = new GraphServiceClient(requestAdapter);
        var services = new ServiceCollection();

        // Act
        services.AddMicrosoftGraphServices(graphServiceClient);

        // Assert
        services.Should().Contain(sd => sd.ServiceType == typeof(ICalendarGraphService));
        services.Should().Contain(sd => sd.ServiceType == typeof(IGroupsGraphService));
        services.Should().Contain(sd => sd.ServiceType == typeof(IOneDriveGraphService));
        services.Should().Contain(sd => sd.ServiceType == typeof(IOutlookGraphService));
        services.Should().Contain(sd => sd.ServiceType == typeof(ISharepointGraphService));
        services.Should().Contain(sd => sd.ServiceType == typeof(ITeamsGraphService));
        services.Should().Contain(sd => sd.ServiceType == typeof(IUsersGraphService));
    }

    [Fact]
    public void AddMicrosoftGraphServices_WithGraphServiceClient_RegistersAsSingletons()
    {
        // Arrange
        var requestAdapter = Substitute.For<IRequestAdapter>();
        using var graphServiceClient = new GraphServiceClient(requestAdapter);
        var services = new ServiceCollection();

        // Act
        services.AddMicrosoftGraphServices(graphServiceClient);

        // Assert
        Type[] expectedSingletons =
        [
            typeof(GraphServiceClient),
            typeof(ICalendarGraphService),
            typeof(IGroupsGraphService),
            typeof(IOneDriveGraphService),
            typeof(IOutlookGraphService),
            typeof(ISharepointGraphService),
            typeof(ITeamsGraphService),
            typeof(IUsersGraphService),
        ];

        foreach (var type in expectedSingletons)
        {
            services
                .Where(sd => sd.ServiceType == type)
                .Should().AllSatisfy(sd => sd.Lifetime.Should().Be(ServiceLifetime.Singleton));
        }
    }

    [Fact]
    public void AddMicrosoftGraphServices_WithGraphServiceClient_CanResolveServices()
    {
        // Arrange
        var requestAdapter = Substitute.For<IRequestAdapter>();
        using var graphServiceClient = new GraphServiceClient(requestAdapter);
        var services = new ServiceCollection();
        services.AddLogging();

        // Act
        services.AddMicrosoftGraphServices(graphServiceClient);

        // Assert
        using var provider = services.BuildServiceProvider();
        provider.GetRequiredService<ICalendarGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IGroupsGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IOneDriveGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IOutlookGraphService>().Should().NotBeNull();
        provider.GetRequiredService<ISharepointGraphService>().Should().NotBeNull();
        provider.GetRequiredService<ITeamsGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IUsersGraphService>().Should().NotBeNull();
    }

    [Fact]
    public void AddMicrosoftGraphServices_WithPreRegisteredClient_CanResolveServices()
    {
        // Arrange
        var requestAdapter = Substitute.For<IRequestAdapter>();
        using var graphServiceClient = new GraphServiceClient(requestAdapter);
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddSingleton(graphServiceClient);

        // Act
        services.AddMicrosoftGraphServices();

        // Assert
        using var provider = services.BuildServiceProvider();
        provider.GetRequiredService<ICalendarGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IGroupsGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IOneDriveGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IOutlookGraphService>().Should().NotBeNull();
        provider.GetRequiredService<ISharepointGraphService>().Should().NotBeNull();
        provider.GetRequiredService<ITeamsGraphService>().Should().NotBeNull();
        provider.GetRequiredService<IUsersGraphService>().Should().NotBeNull();
    }

    [Fact]
    public void AddMicrosoftGraphServices_WithoutGraphServiceClient_ThrowsOnResolve()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddLogging();
        services.AddMicrosoftGraphServices();

        // Act & Assert
        using var provider = services.BuildServiceProvider();

        provider
            .Invoking(static p => p.GetRequiredService<IUsersGraphService>())
            .Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void AddMicrosoftGraphServices_WithTokenCredential_ThrowsOnNull()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var act = () => services.AddMicrosoftGraphServices(tokenCredential: null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("tokenCredential");
    }

    [Fact]
    public void AddMicrosoftGraphServices_WithGraphServiceOptions_ThrowsOnNull()
    {
        // Arrange
        var services = new ServiceCollection();

        // Act
        var act = () => services.AddMicrosoftGraphServices(graphServiceOptions: null!);

        // Assert
        act.Should().Throw<ArgumentNullException>().WithParameterName("graphServiceOptions");
    }

    [Fact]
    public void AddMicrosoftGraphServices_WithInvalidGraphServiceOptions_ThrowsInvalidOperation()
    {
        // Arrange
        var services = new ServiceCollection();
        var options = new GraphServiceOptions();

        // Act
        var act = () => services.AddMicrosoftGraphServices(options);

        // Assert
        act.Should().Throw<InvalidOperationException>();
    }
}