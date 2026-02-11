namespace Atc.Microsoft.Graph.Client.Services.Sharepoint;

/// <summary>
/// Provides operations for interacting with SharePoint via the Microsoft Graph API.
/// </summary>
public interface ISharepointGraphService
{
    /// <summary>
    /// Retrieves all SharePoint sites, with optional OData query parameters.
    /// </summary>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of sites.</returns>
    Task<(HttpStatusCode StatusCode, IList<Site> Data)> GetSites(
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all SharePoint lists for a given site, with optional OData query parameters.
    /// </summary>
    /// <param name="siteId">The site identifier.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of lists.</returns>
    Task<(HttpStatusCode StatusCode, IList<List> Data)> GetListsBySiteId(
        string siteId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves all list items for a given list and site, with optional OData query parameters.
    /// </summary>
    /// <param name="siteId">The site identifier.</param>
    /// <param name="listId">The list identifier.</param>
    /// <param name="expandQueryParameters">Optional OData $expand parameters.</param>
    /// <param name="filterQueryParameter">Optional OData $filter parameter.</param>
    /// <param name="selectQueryParameters">Optional OData $select parameters.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A tuple containing the HTTP status code and a list of list items.</returns>
    Task<(HttpStatusCode StatusCode, IList<ListItem> Data)> GetListItemsByListIdAndSiteId(
        string siteId,
        string listId,
        List<string>? expandQueryParameters = null,
        string? filterQueryParameter = null,
        List<string>? selectQueryParameters = null,
        CancellationToken cancellationToken = default);
}