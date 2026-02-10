namespace Atc.Microsoft.Graph.Client.Tests.Services.Outlook;

public sealed class OutlookGraphServiceTests : IDisposable
{
    private readonly IRequestAdapter requestAdapter;
    private readonly GraphServiceClient graphServiceClient;
    private readonly NullLoggerFactory loggerFactory;
    private readonly OutlookGraphService sut;

    public OutlookGraphServiceTests()
    {
        requestAdapter = Substitute.For<IRequestAdapter>();
        requestAdapter.BaseUrl.Returns("https://graph.microsoft.com/v1.0");

        graphServiceClient = new GraphServiceClient(requestAdapter);
        loggerFactory = new NullLoggerFactory();
        sut = new OutlookGraphService(loggerFactory, graphServiceClient);
    }

    public void Dispose()
    {
        graphServiceClient.Dispose();
        loggerFactory.Dispose();
    }

    [Fact]
    public async Task GetRootMailFoldersByUserId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<MailFolderCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((MailFolderCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetRootMailFoldersByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetRootMailFoldersByUserId_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new MailFolderCollectionResponse { Value = [] };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<MailFolderCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetRootMailFoldersByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetRootMailFoldersByUserId_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<MailFolderCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetRootMailFoldersByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetMailFoldersByUserIdAndFolderId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<MailFolderCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((MailFolderCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetMailFoldersByUserIdAndFolderId(
            "user-id",
            "folder-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetMessagesByUserId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<MessageCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((MessageCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetMessagesByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetMessagesByUserId_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new MessageCollectionResponse { Value = [] };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<MessageCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetMessagesByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetFileAttachmentsByUserIdAndMessageId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<AttachmentCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((AttachmentCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetFileAttachmentsByUserIdAndMessageId(
            "user-id",
            "message-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetFileAttachmentsByUserIdAndMessageId_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new AttachmentCollectionResponse { Value = [] };
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<AttachmentCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetFileAttachmentsByUserIdAndMessageId(
            "user-id",
            "message-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }
}