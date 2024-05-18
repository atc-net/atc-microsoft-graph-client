namespace Atc.Microsoft.Graph.Client;

internal static class LoggingEventIdConstants
{
    internal static class GraphServiceClientWrapper
    {
        public const int GetFailure = 10_000;

        public const int SubscriptionSetupFailed = 10_100;
        public const int SubscriptionRenewalFailed = 10_101;
        public const int SubscriptionDeletionFailed = 10_102;

        public const int DownloadFileFailed = 10_200;
        public const int DownloadFileRetrying = 10_201;
        public const int DownloadFileEmpty = 10_202;

        public const int DeltaLinkNotFoundForDrive = 10_300;

        public const int DriveNotFoundForTeam = 10_400;

        public const int PageIteratorCount = 10_500;
        public const int PageIteratorTotalCount = 10_501;
    }
}