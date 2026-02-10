namespace Atc.Microsoft.Graph.Client.Tests.Services.Teams;

public sealed class TeamsGraphServiceTests : IDisposable
{
    private readonly IRequestAdapter requestAdapter;
    private readonly GraphServiceClient graphServiceClient;
    private readonly NullLoggerFactory loggerFactory;
    private readonly TeamsGraphService sut;

    public TeamsGraphServiceTests()
    {
        requestAdapter = Substitute.For<IRequestAdapter>();
        requestAdapter.BaseUrl.Returns("https://graph.microsoft.com/v1.0");

        graphServiceClient = new GraphServiceClient(requestAdapter);
        loggerFactory = new NullLoggerFactory();
        sut = new TeamsGraphService(loggerFactory, graphServiceClient);
    }

    public void Dispose()
    {
        graphServiceClient.Dispose();
        loggerFactory.Dispose();
    }

    [Fact]
    public async Task GetTeams_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<TeamCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((TeamCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetTeams(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetTeams_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Test error" } };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<TeamCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetTeams(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetTeams_EmptyResponse_ReturnsOkWithEmptyList()
    {
        // Arrange
        var response = new TeamCollectionResponse { Value = [] };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<TeamCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetTeams(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetTeams_WithTeams_ReturnsOkWithData()
    {
        // Arrange
        var teams = new List<Team>
        {
            new() { Id = "1", DisplayName = "Team 1" },
            new() { Id = "2", DisplayName = "Team 2" },
        };
        var response = new TeamCollectionResponse { Value = teams };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<TeamCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetTeams(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(2);
    }
}