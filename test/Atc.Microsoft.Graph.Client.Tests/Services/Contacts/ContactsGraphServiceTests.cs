namespace Atc.Microsoft.Graph.Client.Tests.Services.Contacts;

public sealed class ContactsGraphServiceTests : IDisposable
{
    private readonly IRequestAdapter requestAdapter;
    private readonly GraphServiceClient graphServiceClient;
    private readonly NullLoggerFactory loggerFactory;
    private readonly ContactsGraphService sut;

    public ContactsGraphServiceTests()
    {
        requestAdapter = Substitute.For<IRequestAdapter>();
        requestAdapter.BaseUrl.Returns("https://graph.microsoft.com/v1.0");

        graphServiceClient = new GraphServiceClient(requestAdapter);
        loggerFactory = new NullLoggerFactory();
        sut = new ContactsGraphService(loggerFactory, graphServiceClient);
    }

    public void Dispose()
    {
        graphServiceClient.Dispose();
        loggerFactory.Dispose();
    }

    [Fact]
    public async Task GetContactsByUserId_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ContactCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((ContactCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetContactsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetContactsByUserId_EmptyResponse_ReturnsOk()
    {
        // Arrange
        var response = new ContactCollectionResponse { Value = [] };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ContactCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetContactsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetContactsByUserId_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ContactCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetContactsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetContactsByUserId_WithContacts_ReturnsOkWithData()
    {
        // Arrange
        var contacts = new List<Contact> { new() { Id = "1", DisplayName = "Test Contact" } };
        var response = new ContactCollectionResponse { Value = contacts };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<ContactCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetContactsByUserId(
            "user-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetContactByUserIdAndContactId_ODataErrorNotFound_ReturnsNotFound()
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
                Arg.Any<ParsableFactory<Contact>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetContactByUserIdAndContactId(
            "user-id",
            "contact-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.NotFound);
        data.Should().BeNull();
    }

    [Fact]
    public async Task GetContactByUserIdAndContactId_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Contact>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetContactByUserIdAndContactId(
            "user-id",
            "contact-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeNull();
    }

    [Fact]
    public async Task GetContactByUserIdAndContactId_Success_ReturnsOk()
    {
        // Arrange
        var contact = new Contact { Id = "contact-id", DisplayName = "Test" };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Contact>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(contact);

        // Act
        var (statusCode, data) = await sut.GetContactByUserIdAndContactId(
            "user-id",
            "contact-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
    }

    [Fact]
    public Task CreateContact_ThrowsOnNull()
    {
        // Act
        var act = () => sut.CreateContact(
            "user-id",
            contact: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task CreateContact_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var contact = new Contact { DisplayName = "Test" };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Contact>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.CreateContact(
            "user-id",
            contact,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeNull();
    }

    [Fact]
    public async Task CreateContact_Success_ReturnsCreated()
    {
        // Arrange
        var contact = new Contact { DisplayName = "Test" };
        var createdContact = new Contact { Id = "new-id", DisplayName = "Test" };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Contact>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(createdContact);

        // Act
        var (statusCode, data) = await sut.CreateContact(
            "user-id",
            contact,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.Created);
        data.Should().NotBeNull();
    }

    [Fact]
    public Task UpdateContact_ThrowsOnNull()
    {
        // Act
        var act = () => sut.UpdateContact(
            "user-id",
            "contact-id",
            contact: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task UpdateContact_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var contact = new Contact { DisplayName = "Updated" };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Contact>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.UpdateContact(
            "user-id",
            "contact-id",
            contact,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeNull();
    }

    [Fact]
    public async Task UpdateContact_Success_ReturnsOk()
    {
        // Arrange
        var contact = new Contact { DisplayName = "Updated" };
        var updatedContact = new Contact { Id = "contact-id", DisplayName = "Updated" };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Contact>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(updatedContact);

        // Act
        var (statusCode, data) = await sut.UpdateContact(
            "user-id",
            "contact-id",
            contact,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().NotBeNull();
    }

    [Fact]
    public async Task DeleteContact_ODataError_ReturnsInternalServerError()
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
        var (statusCode, succeeded) = await sut.DeleteContact(
            "user-id",
            "contact-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteContact_NotFoundError_ReturnsOk()
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
        var (statusCode, succeeded) = await sut.DeleteContact(
            "user-id",
            "contact-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteContact_Success_ReturnsOk()
    {
        // Act
        var (statusCode, succeeded) = await sut.DeleteContact(
            "user-id",
            "contact-id",
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }
}