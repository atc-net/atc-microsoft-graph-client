# 📊 Introduction

The library provides a convenient abstraction layer over the Microsoft Graph SDK, simplifying interactions with Microsoft Graph APIs. By wrapping the Microsoft Graph SDK, the library offers a consistent and simplified interface, handling complexities like paging and error handling for you.

# Table of Contents

- [Introduction](#introduction)
- [Table of Contents](#table-of-contents)
- [Atc.Microsoft.Graph.Client](#atcmicrosoftgraphclient)
  - [Services](#services)
    - [OneDriveGraphService](#onedrivegraphservice)
    - [OutlookGraphService](#outlookgraphservice)
    - [SharepointGraphService](#sharepointgraphservice)
    - [TeamsGraphService](#teamsgraphservice)
    - [UsersGraphService](#usersgraphservice)
  - [Wire-Up Using ServiceCollection Extensions](#wire-up-using-servicecollection-extensions)
    - [Options Available in the Extensions Class](#options-available-in-the-extensions-class)
    - [Setup with ServiceCollection](#setup-with-servicecollection)
- [Sample Project](#sample-project)
- [Requirements](#requirements)
- [How to Contribute](#how-to-contribute)

# Atc.Microsoft.Graph.Client

[![NuGet Version](https://img.shields.io/nuget/v/atc.microsoft.graph.client.svg?logo=nuget&style=for-the-badge)](https://www.nuget.org/packages/atc.microsoft.graph.client)

## Services

The services provided in the `Atc.Microsoft.Graph.Client` package are designed to facilitate seamless interaction with various Microsoft services through the Graph API. These services are essential for applications that need to manage and retrieve data efficiently from OneDrive, Outlook, SharePoint, Teams, User Management etc. By leveraging these services, applications can ensure robust and secure handling of data, integrating comprehensive functionalities directly into the application's workflow. Each service supports efficient querying of data, supporting expand, filter, and select query parameters to tailor the data retrieval process.

### OneDriveGraphService

The `IOneDriveGraphService` is essential for applications that require efficient management of OneDrive resources, including retrieving and managing drives and drive items, tracking changes with delta tokens, and downloading files. This service ensures robust and secure handling of OneDrive data, integrating OneDrive capabilities directly into the application's workflow.

### OutlookGraphService

The `IOutlookGraphService` is essential for applications that need to manage and retrieve Outlook mail data, such as mail folders, messages, and file attachments. It enables efficient querying of mail folders and messages, supports the use of delta tokens for tracking changes, and ensures secure handling of email data, enhancing email management within the application's ecosystem.

### SharepointGraphService

The `ISharepointGraphService` is essential for applications that need to manage SharePoint sites and subscriptions effectively. It provides capabilities for retrieving site information, setting up and managing subscriptions, and handling subscription renewals and deletions, ensuring robust and efficient integration of SharePoint functionalities within the application's environment.

### TeamsGraphService

The `ITeamsGraphService` is essential for applications that need to retrieve and manage information about Teams. It allows for efficient querying of Teams data, enhancing collaboration and communication capabilities within the application's ecosystem.

### UsersGraphService

The `IUsersGraphService` is essential for applications that need to retrieve and manage information about users. It allows for efficient querying of user data, ensuring robust and efficient integration of user management functionalities within the application's environment.

## Wire-Up Using ServiceCollection Extensions

To seamlessly integrate the Graph services into your application, you can utilize the provided [`ServiceCollection`](src/Atc.Microsoft.Graph.Client/Extensions/ServiceCollectionExtensions.cs) extension methods. These methods simplify the setup process and ensure that the Graph services are correctly configured and ready to use within your application's service architecture.

The methods ensure that the Graph services are added to the application's service collection and configured according to the specified parameters, making them available throughout your application via dependency injection.

The configuration example below utilize the application's settings (typically defined in appsettings.json) to configure the Graph Services by calling the overload that accepts `GraphServiceOptions` and implicitly configures the GraphServiceClient utilizing a ClientSecretCredential.

### Options Available in the Extensions Class
The `ServiceCollectionExtensions` class provides several methods to add and configure the Graph services in your application:

- AddMicrosoftGraphServices(GraphServiceClient? graphServiceClient = null)
  > Adds the GraphServiceClient to the service collection, optionally using a provided GraphServiceClient instance. If no instance is provided, one must be available in the service provider when this service is resolved.

- AddMicrosoftGraphServices(TokenCredential tokenCredential, string[]? scopes = null)
  > Adds the GraphServiceClient to the service collection using the provided TokenCredential for authentication. Optional scopes can also be specified.

- AddMicrosoftGraphServices(GraphServiceOptions graphServiceOptions, string[]? scopes = null)
  > Adds the GraphServiceClient to the service collection using the provided GraphServiceOptions. This method ensures the GraphServiceClient is configured with a ClientSecretCredential based on the specified options.

### Setup with ServiceCollection

```csharp
public void ConfigureServices(IServiceCollection services)
{
    var graphServiceOptions = new GraphServiceOptions
    {
        TenantId = "your_tenant_id",
        ClientId = "your_client_id",
        ClientSecret = "your_client_secret",
    };

    services.AddMicrosoftGraphServices(graphServiceOptions);
}
```

# 🚀 Sample Project

A [sample](sample/Atc.Microsoft.Graph.Client.Sample/Program.cs) is included, demonstrating how to configure and use the Microsoft Graph services.

# ⚙️ Requirements

* [.NET 10 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)

# 🤝 How to contribute

[Contribution Guidelines](https://atc-net.github.io/introduction/about-atc#how-to-contribute)

[Coding Guidelines](https://atc-net.github.io/introduction/about-atc#coding-guidelines)
