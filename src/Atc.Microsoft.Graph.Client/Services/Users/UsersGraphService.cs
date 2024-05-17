namespace Atc.Microsoft.Graph.Client.Services.Users;

public sealed class UsersGraphService : GraphServiceClientWrapper, IUsersGraphService
{
    public UsersGraphService(
        ILoggerFactory loggerFactory,
        GraphServiceClient client)
        : base(loggerFactory, client)
    {
    }

    public async Task<(HttpStatusCode StatusCode, IList<User> Data)> GetUsers(
        List<string>? expandQueryParameters,
        string? filterQueryParameter,
        List<string>? selectQueryParameters,
        CancellationToken cancellationToken)
    {
        List<User> pagedItems = [];
        var count = 0;

        try
        {
            var requestInformation = Client
                .Users
                .ToGetRequestInformation(
                    RequestConfigurationFactory.CreateForUsers(
                        expandQueryParameters,
                        filterQueryParameter,
                        selectQueryParameters));

            var response = await Client.RequestAdapter.SendAsync(
                requestInformation,
                (_) => new UserCollectionResponse(),
                cancellationToken: cancellationToken);

            if (response is null)
            {
                return (HttpStatusCode.InternalServerError, pagedItems);
            }

            var pageIterator = PageIterator<User, UserCollectionResponse>.CreatePageIterator(
                Client,
                response,
                item =>
                {
                    pagedItems.Add(item);

                    count++;
                    if (count % 1000 == 0)
                    {
                        LogPageIteratorCount(nameof(User), count);
                    }

                    return true;
                });

            try
            {
                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.TooManyRequests)
            {
                await Task.Delay(MicrosoftGraphConstants.RetryWaitDelayInMs, cancellationToken);

                await pageIterator.IterateAsync(cancellationToken);
            }
            catch (ODataError odataError) when (odataError.ResponseStatusCode == (int)HttpStatusCode.Gone)
            {
                return (HttpStatusCode.Gone, pagedItems);
            }

            LogPageIteratorTotalCount(nameof(User), count);

            return (HttpStatusCode.OK, pagedItems);
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
}