namespace Atc.Microsoft.Graph.Client.Services;

public abstract partial class GraphServiceClientWrapper
{
    protected GraphServiceClient Client { get; }

    protected ResiliencePipeline DownloadResiliencePipeline { get; }

    protected GraphServiceClientWrapper(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
    {
        this.logger = loggerFactory.CreateLogger<GraphServiceClientWrapper>();
        this.Client = client;

        var retryStrategyOptions = new RetryStrategyOptions
        {
            ShouldHandle = new PredicateBuilder()
                .Handle<ODataError>()
                .Handle<Exception>(),
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true,
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(3),
            OnRetry = args =>
            {
                var errorMessage = args.Outcome.Result switch
                {
                    ODataError oDataError => oDataError.Error?.Message,
                    Exception ex => ex.GetLastInnerMessage(),
                    _ => null,
                };

                errorMessage ??= args.Outcome.Exception?.GetLastInnerMessage() ?? "Unknown Exception";

                LogDownloadFileRetrying(errorMessage);
                return ValueTask.CompletedTask;
            },
        };

        DownloadResiliencePipeline = new ResiliencePipelineBuilder()
            .AddRetry(retryStrategyOptions)
            .Build();
    }
}