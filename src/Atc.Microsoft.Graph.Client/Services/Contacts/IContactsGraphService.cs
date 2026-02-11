namespace Atc.Microsoft.Graph.Client.Services.Contacts;

public interface IContactsGraphService
{
    Task<(HttpStatusCode StatusCode, IList<Contact> Data)> GetContactsByUserId(
        string userId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, Contact? Data)> GetContactByUserIdAndContactId(
        string userId,
        string contactId,
        List<string>? expandQueryParameters = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, Contact? Data)> CreateContact(
        string userId,
        Contact contact,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, Contact? Data)> UpdateContact(
        string userId,
        string contactId,
        Contact contact,
        CancellationToken cancellationToken = default);

    Task<(HttpStatusCode StatusCode, bool Succeeded)> DeleteContact(
        string userId,
        string contactId,
        CancellationToken cancellationToken = default);
}