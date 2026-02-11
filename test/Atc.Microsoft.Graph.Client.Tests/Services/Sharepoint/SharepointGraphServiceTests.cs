namespace Atc.Microsoft.Graph.Client.Tests.Services.Sharepoint;

public sealed class SharepointGraphServiceTests : IDisposable
{
    private readonly IRequestAdapter requestAdapter;
    private readonly GraphServiceClient graphServiceClient;
    private readonly NullLoggerFactory loggerFactory;
    private readonly SharepointGraphService sut;

    public SharepointGraphServiceTests()
    {
        requestAdapter = Substitute.For<IRequestAdapter>();
        requestAdapter.BaseUrl.Returns("https://graph.microsoft.com/v1.0");

        graphServiceClient = new GraphServiceClient(requestAdapter);
        loggerFactory = new NullLoggerFactory();
        sut = new SharepointGraphService(loggerFactory, graphServiceClient);
    }

    public void Dispose()
    {
        graphServiceClient.Dispose();
        loggerFactory.Dispose();
    }

    [Fact]
    public async Task GetSites_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<SiteCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((SiteCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetSites(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetSites_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new SiteCollectionResponse { Value = [] };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<SiteCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetSites(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetSites_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<SiteCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetSites(cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetListsBySiteId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((ListCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetListsBySiteId("site-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetListsBySiteId_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetListsBySiteId("site-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetListsBySiteId_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new ListCollectionResponse { Value = [] };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetListsBySiteId("site-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetListsBySiteId_WithLists_ReturnsOkWithData()
    {
        // Arrange
        var lists = new List<List>
        {
            new() { Id = "1", DisplayName = "List 1" },
        };

        var response = new ListCollectionResponse { Value = lists };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetListsBySiteId("site-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetListItemsByListIdAndSiteId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListItemCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((ListItemCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetListItemsByListIdAndSiteId("site-id", "list-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetListItemsByListIdAndSiteId_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListItemCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetListItemsByListIdAndSiteId("site-id", "list-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetListItemsByListIdAndSiteId_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new ListItemCollectionResponse { Value = [] };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListItemCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetListItemsByListIdAndSiteId("site-id", "list-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetListItemsByListIdAndSiteId_WithListItems_ReturnsOkWithData()
    {
        // Arrange
        var listItems = new List<ListItem>
        {
            new() { Id = "1" },
        };

        var response = new ListItemCollectionResponse { Value = listItems };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ListItemCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetListItemsByListIdAndSiteId("site-id", "list-id", cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(1);
    }
}