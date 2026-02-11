namespace Atc.Microsoft.Graph.Client.Services.Contacts;

public sealed class ContactsGraphService : GraphServiceClientWrapper, IContactsGraphService
{
    public ContactsGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<Contact> Data)> GetContactsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        List<Contact> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users[userId]
                .Contacts
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForContacts(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new ContactCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<Contact, ContactCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(Contact), count);
                    }

                    return true;
                });

            await ResiliencePipeline.ExecuteAsync(
                async ct => await pageIterator.IterateAsync(ct),
                cancellationToken);

            LogPageIteratorTotalCount(nameof(Contact), count);

            return (HttpStatusCode.OK, pagedItems);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
        {
            return (HttpStatusCode.Gone, pagedItems);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, pagedItems);
        }
    }

    public async Task<(HttpStatusCode StatusCode, Contact? Data)> GetContactByUserIdAndContactId(
        string userId,
        string contactId,
        List<string>? expandQueryParameters = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var contact = await Client
                .Users[userId]
                .Contacts[contactId]
                .GetAsync(
                    RequestConfigurationFactory.CreateForContactById(
                        expandQueryParameters,
                        selectQueryParameters),
                    cancellationToken);

            return contact is not null
                ? (HttpStatusCode.OK, contact)
                : (HttpStatusCode.NotFound, null);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.NotFound)
        {
            LogContactNotFoundById(userId, contactId, odataError.Error?.Message);
            return (HttpStatusCode.NotFound, null);
        }
        catch (ODataError odataError)
        {
            LogGetFailure(odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogGetFailure(ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, Contact? Data)> CreateContact(
        string userId,
        Contact contact,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(contact);

        try
        {
            Contact? result = null;

            await ResiliencePipeline.ExecuteAsync(
                async ct =>
                {
                    result = await Client
                        .Users[userId]
                        .Contacts
                        .PostAsync(contact, cancellationToken: ct);
                    return result;
                },
                cancellationToken);

            return result is not null
                ? (HttpStatusCode.Created, result)
                : (HttpStatusCode.InternalServerError, null);
        }
        catch (ODataError odataError)
        {
            LogContactCreationFailed(userId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogContactCreationFailed(userId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, Contact? Data)> UpdateContact(
        string userId,
        string contactId,
        Contact contact,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(contact);

        try
        {
            var result = await Client
                .Users[userId]
                .Contacts[contactId]
                .PatchAsync(contact, cancellationToken: cancellationToken);

            return result is not null
                ? (HttpStatusCode.OK, result)
                : (HttpStatusCode.InternalServerError, null);
        }
        catch (ODataError odataError)
        {
            LogContactUpdateFailed(userId, contactId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, null);
        }
        catch (Exception ex)
        {
            LogContactUpdateFailed(userId, contactId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, null);
        }
    }

    public async Task<(HttpStatusCode StatusCode, bool Succeeded)> DeleteContact(
        string userId,
        string contactId,
        CancellationToken cancellationToken = default)
    {
        try
        {
            await Client
                .Users[userId]
                .Contacts[contactId]
                .DeleteAsync(cancellationToken: cancellationToken);

            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.NotFound)
        {
            return (HttpStatusCode.OK, true);
        }
        catch (ODataError odataError)
        {
            LogContactDeletionFailed(userId, contactId, odataError.Error?.Message);
            return (HttpStatusCode.InternalServerError, false);
        }
        catch (Exception ex)
        {
            LogContactDeletionFailed(userId, contactId, ex.GetLastInnerMessage());
            return (HttpStatusCode.InternalServerError, false);
        }
    }
}