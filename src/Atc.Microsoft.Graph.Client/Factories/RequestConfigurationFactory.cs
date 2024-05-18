namespace Atc.Microsoft.Graph.Client.Factories;

public static class RequestConfigurationFactory
{
    public static Action<RequestConfiguration<AttachmentsRequestBuilder.AttachmentsRequestBuilderGetQueryParameters>> CreateForAttachments(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<DrivesRequestBuilder.DrivesRequestBuilderGetQueryParameters>> CreateForDrives(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<ItemsRequestBuilder.ItemsRequestBuilderGetQueryParameters>> CreateForItems(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Drives.Item.Items.Item.DeltaWithToken.DeltaWithTokenRequestBuilder.DeltaWithTokenRequestBuilderGetQueryParameters>> CreateForItemsWithDelta(
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<ChildFoldersRequestBuilder.ChildFoldersRequestBuilderGetQueryParameters>> CreateForChildFolders(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<MailFoldersRequestBuilder.MailFoldersRequestBuilderGetQueryParameters>> CreateForMailFolders(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Users.Item.MailFolders.Item.Messages.MessagesRequestBuilder.MessagesRequestBuilderGetQueryParameters>> CreateForMessagesMailFolder(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Users.Item.Messages.MessagesRequestBuilder.MessagesRequestBuilderGetQueryParameters>> CreateForMessagesUserId(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Users.Item.MailFolders.Item.Messages.Delta.DeltaRequestBuilder.DeltaRequestBuilderGetQueryParameters>> CreateForMessagesDelta(
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<SitesRequestBuilder.SitesRequestBuilderGetQueryParameters>> CreateForSites(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<TeamsRequestBuilder.TeamsRequestBuilderGetQueryParameters>> CreateForTeams(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<UsersRequestBuilder.UsersRequestBuilderGetQueryParameters>> CreateForUsers(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Any())
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Any())
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };
}