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
        public const int Retrying = 10_201;
        public const int DownloadFileEmpty = 10_202;

        public const int DeltaLinkNotFoundForDrive = 10_300;

        public const int DriveNotFoundForTeam = 10_400;

        public const int PageIteratorCount = 10_500;
        public const int PageIteratorTotalCount = 10_501;

        public const int GroupNotFoundById = 10_600;

        public const int UserNotFoundById = 10_700;
        public const int ManagerNotFoundForUser = 10_701;

        public const int MailSendFailed = 10_800;
        public const int MailDraftCreationFailed = 10_801;
        public const int MailDraftSendFailed = 10_802;
        public const int ReplyToMessageFailed = 10_803;
        public const int ForwardMessageFailed = 10_804;

        public const int OnlineMeetingNotFoundById = 10_900;
        public const int OnlineMeetingCreationFailed = 10_901;
        public const int OnlineMeetingUpdateFailed = 10_902;
        public const int OnlineMeetingDeletionFailed = 10_903;

        public const int ContactNotFoundById = 11_000;
        public const int ContactCreationFailed = 11_001;
        public const int ContactUpdateFailed = 11_002;
        public const int ContactDeletionFailed = 11_003;

        public const int SubscriptionListFailed = 10_103;

        public const int SearchQueryFailed = 11_100;
    }
}