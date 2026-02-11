namespace Atc.Microsoft.Graph.Client.Tests.Services.OnlineMeetings;

public sealed class OnlineMeetingsGraphServiceTests : IDisposable
{
    private readonly IRequestAdapter requestAdapter;
    private readonly GraphServiceClient graphServiceClient;
    private readonly NullLoggerFactory loggerFactory;
    private readonly OnlineMeetingsGraphService sut;

    public OnlineMeetingsGraphServiceTests()
    {
        requestAdapter = Substitute.For<IRequestAdapter>();
        requestAdapter.BaseUrl.Returns("https://graph.microsoft.com/v1.0");

        graphServiceClient = new GraphServiceClient(requestAdapter);
        loggerFactory = new NullLoggerFactory();
        sut = new OnlineMeetingsGraphService(loggerFactory, graphServiceClient);
    }

    public void Dispose()
    {
        graphServiceClient.Dispose();
        loggerFactory.Dispose();
    }

    [Fact]
    public async Task GetOnlineMeetingsByUserId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeetingCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((OnlineMeetingCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetOnlineMeetingsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetOnlineMeetingsByUserId_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new OnlineMeetingCollectionResponse { Value = [] };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeetingCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetOnlineMeetingsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetOnlineMeetingsByUserId_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeetingCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetOnlineMeetingsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetOnlineMeetingsByUserId_WithMeetings_ReturnsOkWithData()
    {
        // Arrange
        var meetings = new List<OnlineMeeting> { new() { Id = "1", Subject = "Test Meeting" } };
        var response = new OnlineMeetingCollectionResponse { Value = meetings };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeetingCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetOnlineMeetingsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetOnlineMeetingByUserIdAndMeetingId_ODataErrorNotFound_ReturnsNotFound()
    {
        // Arrange
        var odataError = new ODataError
        {
            ResponseStatusCode = (int)HttpStatusCode.NotFound,
            Error = new MainError { Message = "Not found" },
        };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeeting>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetOnlineMeetingByUserIdAndMeetingId(
            "user-id",
            "meeting-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.NotFound);
        data.Should().BeNull();
    }

    [Fact]
    public async Task GetOnlineMeetingByUserIdAndMeetingId_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeeting>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetOnlineMeetingByUserIdAndMeetingId(
            "user-id",
            "meeting-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeNull();
    }

    [Fact]
    public async Task GetOnlineMeetingByUserIdAndMeetingId_Success_ReturnsOk()
    {
        // Arrange
        var meeting = new OnlineMeeting { Id = "meeting-id", Subject = "Test" };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeeting>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(meeting);

        // Act
        var (statusCode, data) = await sut.GetOnlineMeetingByUserIdAndMeetingId(
            "user-id",
            "meeting-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
    }

    [Fact]
    public Task CreateOnlineMeeting_ThrowsOnNull()
    {
        // Act
        var act = () => sut.CreateOnlineMeeting(
            "user-id",
            onlineMeeting: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task CreateOnlineMeeting_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var meeting = new OnlineMeeting { Subject = "Test" };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeeting>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.CreateOnlineMeeting(
            "user-id",
            meeting,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeNull();
    }

    [Fact]
    public async Task CreateOnlineMeeting_Success_ReturnsCreated()
    {
        // Arrange
        var meeting = new OnlineMeeting { Subject = "Test" };
        var createdMeeting = new OnlineMeeting { Id = "new-id", Subject = "Test" };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeeting>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(createdMeeting);

        // Act
        var (statusCode, data) = await sut.CreateOnlineMeeting(
            "user-id",
            meeting,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.Created);
        data.Should().NotBeNull();
    }

    [Fact]
    public Task UpdateOnlineMeeting_ThrowsOnNull()
    {
        // Act
        var act = () => sut.UpdateOnlineMeeting(
            "user-id",
            "meeting-id",
            onlineMeeting: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateOnlineMeeting_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var meeting = new OnlineMeeting { Subject = "Updated" };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeeting>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.UpdateOnlineMeeting(
            "user-id",
            "meeting-id",
            meeting,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeNull();
    }

    [Fact]
    public async Task UpdateOnlineMeeting_Success_ReturnsOk()
    {
        // Arrange
        var meeting = new OnlineMeeting { Subject = "Updated" };
        var updatedMeeting = new OnlineMeeting { Id = "meeting-id", Subject = "Updated" };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<OnlineMeeting>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(updatedMeeting);

        // Act
        var (statusCode, data) = await sut.UpdateOnlineMeeting(
            "user-id",
            "meeting-id",
            meeting,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteOnlineMeeting_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };
        requestAdapter
            .When(x => x.SendNoContentAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>()))
            .Do(_ => throw odataError);

        // Act
        var (statusCode, succeeded) = await sut.DeleteOnlineMeeting(
            "user-id",
            "meeting-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteOnlineMeeting_NotFoundError_ReturnsOk()
    {
        // Arrange
        var odataError = new ODataError
        {
            ResponseStatusCode = (int)HttpStatusCode.NotFound,
            Error = new MainError { Message = "Not found" },
        };
        requestAdapter
            .When(x => x.SendNoContentAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>()))
            .Do(_ => throw odataError);

        // Act
        var (statusCode, succeeded) = await sut.DeleteOnlineMeeting(
            "user-id",
            "meeting-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteOnlineMeeting_Success_ReturnsOk()
    {
        // Act
        var (statusCode, succeeded) = await sut.DeleteOnlineMeeting(
            "user-id",
            "meeting-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }
}