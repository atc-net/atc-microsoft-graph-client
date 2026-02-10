namespace Atc.Microsoft.Graph.Client.Services;

public abstract partial class GraphServiceClientWrapper
{
    protected GraphServiceClient Client { get; }

    protected ResiliencePipeline ResiliencePipeline { get; }

    protected GraphServiceClientWrapper(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
    {
        logger = loggerFactory.CreateLogger<GraphServiceClientWrapper>();
        Client = client;

        var retryStrategyOptions = new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder()
                .Handle<ODataError>()
                .Handle<Exception>(),
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(3),
            DelayGenerator = static args =>
            {
                if (args.Outcome.Exception is not ApiException { ResponseHeaders: { } headers } ||
                    !headers.TryGetValue("Retry-After", out var values))
                {
                    return new ValueTask<TimeSpan?>((TimeSpan?)null);
                }

                var retryAfter = values.FirstOrDefault();

                return int.TryParse(retryAfter, out var seconds)
                    ? new ValueTask<TimeSpan?>(TimeSpan.FromSeconds(seconds))
                    : new ValueTask<TimeSpan?>((TimeSpan?)null);
            },
            OnRetry = args =>
            {
                var errorMessage = args.Outcome.Exception?.GetLastInnerMessage() ?? "Unknown error";
                LogRetrying(errorMessage, args.AttemptNumber, args.RetryDelay);
                return ValueTask.CompletedTask;
            },
        };

        ResiliencePipeline = new ResiliencePipelineBuilder()
            .AddRetry(retryStrategyOptions)
            .Build();
    }
}