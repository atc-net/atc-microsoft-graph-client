namespace Atc.Microsoft.Graph.Client.Tests.Services.Subscriptions;

public sealed class SubscriptionsGraphServiceTests : IDisposable
{
    private readonly IRequestAdapter requestAdapter;
    private readonly GraphServiceClient graphServiceClient;
    private readonly NullLoggerFactory loggerFactory;
    private readonly SubscriptionsGraphService sut;

    public SubscriptionsGraphServiceTests()
    {
        requestAdapter = Substitute.For<IRequestAdapter>();
        requestAdapter.BaseUrl.Returns("https://graph.microsoft.com/v1.0");

        graphServiceClient = new GraphServiceClient(requestAdapter);
        loggerFactory = new NullLoggerFactory();
        sut = new SubscriptionsGraphService(loggerFactory, graphServiceClient);
    }

    public void Dispose()
    {
        graphServiceClient.Dispose();
        loggerFactory.Dispose();
    }

    [Fact]
    public async Task GetSubscriptions_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<SubscriptionCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, data) = await sut.GetSubscriptions(
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetSubscriptions_NullResponse_ReturnsInternalServerError()
    {
        // Arrange
        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<SubscriptionCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns((SubscriptionCollectionResponse)null!);

        // Act
        var (statusCode, data) = await sut.GetSubscriptions(
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        data.Should().BeEmpty();
    }

    [Fact]
    public async Task GetSubscriptions_Success_ReturnsOk()
    {
        // Arrange
        var subscriptions = new List<Subscription> { new() { Id = Guid.NewGuid().ToString() } };
        var response = new SubscriptionCollectionResponse { Value = subscriptions };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<SubscriptionCollectionResponse>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .Returns(response);

        // Act
        var (statusCode, data) = await sut.GetSubscriptions(
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        data.Should().HaveCount(1);
    }

    [Fact]
    public Task SetupSubscription_ThrowsOnNull()
    {
        // Act
        var act = () => sut.SetupSubscription(
            subscription: null!,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        return act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SetupSubscription_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var subscription = new Subscription { Resource = "sites/root" };
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Subscription>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, subscriptionId) = await sut.SetupSubscription(
            subscription,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        subscriptionId.Should().BeNull();
    }

    [Fact]
    public async Task SetupSubscription_TimedOutError_ReturnsRequestTimeout()
    {
        // Arrange
        var subscription = new Subscription { Resource = "sites/root" };
        var odataError = new ODataError { Error = new MainError { Message = "The request timed out" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Subscription>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, subscriptionId) = await sut.SetupSubscription(
            subscription,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.RequestTimeout);
        subscriptionId.Should().BeNull();
    }

    [Fact]
    public async Task RenewSubscription_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var expirationDate = DateTimeOffset.UtcNow.AddDays(1);
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .SendAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<ParsableFactory<Subscription>>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>())
            .ThrowsAsyncForAnyArgs(odataError);

        // Act
        var (statusCode, succeeded) = await sut.RenewSubscription(
            subscriptionId,
            expirationDate,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteSubscription_ODataError_ReturnsInternalServerError()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();
        var odataError = new ODataError { Error = new MainError { Message = "Error" } };

        requestAdapter
            .When(x => x.SendNoContentAsync(
                Arg.Any<RequestInformation>(),
                Arg.Any<Dictionary<string, ParsableFactory<IParsable>>>(),
                Arg.Any<CancellationToken>()))
            .Do(_ => throw odataError);

        // Act
        var (statusCode, succeeded) = await sut.DeleteSubscription(
            subscriptionId,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.InternalServerError);
        succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task DeleteSubscription_NotFoundError_ReturnsOk()
    {
        // Arrange
        var subscriptionId = Guid.NewGuid();

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
        var (statusCode, succeeded) = await sut.DeleteSubscription(
            subscriptionId,
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task DeleteSubscription_Success_ReturnsOk()
    {
        // Act
        var (statusCode, succeeded) = await sut.DeleteSubscription(
            Guid.NewGuid(),
            cancellationToken: TestContext.Current.CancellationToken);

        // Assert
        statusCode.Should().Be(HttpStatusCode.OK);
        succeeded.Should().BeTrue();
    }
}