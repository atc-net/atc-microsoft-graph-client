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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
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
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<GroupsRequestBuilder.GroupsRequestBuilderGetQueryParameters>> CreateForGroups(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Groups.Item.GroupItemRequestBuilder.GroupItemRequestBuilderGetQueryParameters>> CreateForGroupById(
        List<string>? expandQueryParameters,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Groups.Item.Members.MembersRequestBuilder.MembersRequestBuilderGetQueryParameters>> CreateForGroupMembers(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Groups.Item.Owners.OwnersRequestBuilder.OwnersRequestBuilderGetQueryParameters>> CreateForGroupOwners(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<ChannelsRequestBuilder.ChannelsRequestBuilderGetQueryParameters>> CreateForChannels(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Teams.Item.Members.MembersRequestBuilder.MembersRequestBuilderGetQueryParameters>> CreateForTeamMembers(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Users.Item.UserItemRequestBuilder.UserItemRequestBuilderGetQueryParameters>> CreateForUserById(
        List<string>? expandQueryParameters,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<MemberOfRequestBuilder.MemberOfRequestBuilderGetQueryParameters>> CreateForUserMemberOf(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<CalendarsRequestBuilder.CalendarsRequestBuilderGetQueryParameters>> CreateForCalendars(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<EventsRequestBuilder.EventsRequestBuilderGetQueryParameters>> CreateForEvents(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<CalendarViewRequestBuilder.CalendarViewRequestBuilderGetQueryParameters>> CreateForCalendarView(
        DateTimeOffset startDateTime,
        DateTimeOffset endDateTime,
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            rc.QueryParameters.StartDateTime = startDateTime.UtcDateTime.ToString("o");
            rc.QueryParameters.EndDateTime = endDateTime.UtcDateTime.ToString("o");

            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<ListsRequestBuilder.ListsRequestBuilderGetQueryParameters>> CreateForLists(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<ContactsRequestBuilder.ContactsRequestBuilderGetQueryParameters>> CreateForContacts(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Users.Item.Contacts.Item.ContactItemRequestBuilder.ContactItemRequestBuilderGetQueryParameters>> CreateForContactById(
        List<string>? expandQueryParameters,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<OnlineMeetingsRequestBuilder.OnlineMeetingsRequestBuilderGetQueryParameters>> CreateForOnlineMeetings(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Users.Item.OnlineMeetings.Item.OnlineMeetingItemRequestBuilder.OnlineMeetingItemRequestBuilderGetQueryParameters>> CreateForOnlineMeetingById(
        List<string>? expandQueryParameters,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };

    public static Action<RequestConfiguration<global::Microsoft.Graph.Sites.Item.Lists.Item.Items.ItemsRequestBuilder.ItemsRequestBuilderGetQueryParameters>> CreateForListItems(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters) =>
        rc =>
        {
            if (expandQueryParameters is not null &&
                expandQueryParameters.Count != 0)
            {
                rc.QueryParameters.Expand = [.. expandQueryParameters];
            }

            if (!string.IsNullOrEmpty(filterQueryParameter))
            {
                rc.QueryParameters.Filter = filterQueryParameter;
            }

            if (selectQueryParameters is not null &&
                selectQueryParameters.Count != 0)
            {
                rc.QueryParameters.Select = [.. selectQueryParameters];
            }
        };
}