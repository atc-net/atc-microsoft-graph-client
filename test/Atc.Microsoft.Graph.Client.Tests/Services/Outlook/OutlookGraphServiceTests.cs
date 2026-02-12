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

    [Fact]
    public Task SendMail_ThrowsOnNullMessage()
    {
        // Act
        var act = () => sut.SendMail(
            "user-id",
            message: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SendMail_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var message = new Message { Subject = "Test" };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .When(x => x.SendNoContentAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>()))
            .Do(_ => throw odataError);

        // Act
        var (statusCode, succeeded) = await sut.SendMail(
            "user-id",
            message,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task SendMail_Success_ReturnsOk()
    {
        // Arrange
        var message = new Message { Subject = "Test" };

        // Act
        var (statusCode, succeeded) = await sut.SendMail(
            "user-id",
            message,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }

    [Fact]
    public Task CreateDraftMessage_ThrowsOnNull()
    {
        // Act
        var act = () => sut.CreateDraftMessage(
            "user-id",
            message: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task CreateDraftMessage_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var message = new Message { Subject = "Draft" };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Message>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.CreateDraftMessage(
            "user-id",
            message,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeNull();
    }

    [Fact]
    public async Task CreateDraftMessage_Success_ReturnsCreated()
    {
        // Arrange
        var message = new Message { Subject = "Draft" };
        var createdMessage = new Message { Id = "new-id", Subject = "Draft" };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Message>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(createdMessage);

        // Act
        var (statusCode, data) = await sut.CreateDraftMessage(
            "user-id",
            message,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.Created);
        data.Should().NotBeNull();
    }

    [Fact]
    public async Task SendDraftMessage_ODataError_ReturnsInternalServerError()
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
        var (statusCode, succeeded) = await sut.SendDraftMessage(
            "user-id",
            "message-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task SendDraftMessage_Success_ReturnsOk()
    {
        // Act
        var (statusCode, succeeded) = await sut.SendDraftMessage(
            "user-id",
            "message-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task ReplyToMessage_ODataError_ReturnsInternalServerError()
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
        var (statusCode, succeeded) = await sut.ReplyToMessage(
            "user-id",
            "message-id",
            "Thanks!",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task ReplyToMessage_Success_ReturnsOk()
    {
        // Act
        var (statusCode, succeeded) = await sut.ReplyToMessage(
            "user-id",
            "message-id",
            "Thanks!",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task ReplyAllToMessage_ODataError_ReturnsInternalServerError()
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
        var (statusCode, succeeded) = await sut.ReplyAllToMessage(
            "user-id",
            "message-id",
            "Thanks all!",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task ReplyAllToMessage_Success_ReturnsOk()
    {
        // Act
        var (statusCode, succeeded) = await sut.ReplyAllToMessage(
            "user-id",
            "message-id",
            "Thanks all!",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }

    [Fact]
    public Task ForwardMessage_ThrowsOnNullRecipients()
    {
        // Act
        var act = () => sut.ForwardMessage(
            "user-id",
            "message-id",
            "FYI",
            toRecipients: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ForwardMessage_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var recipients = new List<Recipient> { new() { EmailAddress = new EmailAddress { Address = "test@test.com" } } };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .When(x => x.SendNoContentAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>()))
            .Do(_ => throw odataError);

        // Act
        var (statusCode, succeeded) = await sut.ForwardMessage(
            "user-id",
            "message-id",
            "FYI",
            recipients,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task ForwardMessage_Success_ReturnsOk()
    {
        // Arrange
        var recipients = new List<Recipient> { new() { EmailAddress = new EmailAddress { Address = "test@test.com" } } };

        // Act
        var (statusCode, succeeded) = await sut.ForwardMessage(
            "user-id",
            "message-id",
            "FYI",
            recipients,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }
}