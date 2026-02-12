namespace Atc.Microsoft.Graph.Client.Tests.Services.Search;

public sealed class SearchGraphServiceTests : IDisposable
{
    private readonly IRequestAdapter requestAdapter;
    private readonly GraphServiceClient graphServiceClient;
    private readonly NullLoggerFactory loggerFactory;
    private readonly SearchGraphService sut;

    public SearchGraphServiceTests()
    {
        requestAdapter = Substitute.For<IRequestAdapter>();
        requestAdapter.BaseUrl.Returns("https://graph.microsoft.com/v1.0");

        graphServiceClient = new GraphServiceClient(requestAdapter);
        loggerFactory = new NullLoggerFactory();
        sut = new SearchGraphService(loggerFactory, graphServiceClient);
    }

    public void Dispose()
    {
        graphServiceClient.Dispose();
        loggerFactory.Dispose();
    }

    [Fact]
    public Task ExecuteQuery_ThrowsOnNull()
    {
        // Act
        var act = () => sut.ExecuteQuery(
            queryPostRequestBody: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ExecuteQuery_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var requestBody = new global::Microsoft.Graph.Search.Query.QueryPostRequestBody();
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<global::Microsoft.Graph.Search.Query.QueryPostResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.ExecuteQuery(
            requestBody,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task ExecuteQuery_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        var requestBody = new global::Microsoft.Graph.Search.Query.QueryPostRequestBody();

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<global::Microsoft.Graph.Search.Query.QueryPostResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((global::Microsoft.Graph.Search.Query.QueryPostResponse)null!);

        // Act
        var (statusCode, data) = await sut.ExecuteQuery(
            requestBody,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task ExecuteQuery_Success_ReturnsOk()
    {
        // Arrange
        var requestBody = new global::Microsoft.Graph.Search.Query.QueryPostRequestBody();
        var searchResponse = new SearchResponse();

        var response = new global::Microsoft.Graph.Search.Query.QueryPostResponse
        {
            Value = [searchResponse],
        };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<global::Microsoft.Graph.Search.Query.QueryPostResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.ExecuteQuery(
            requestBody,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(1);
    }
}