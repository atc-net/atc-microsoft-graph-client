# CLAUDE.md - Project Guidelines for atc-microsoft-graph-client

## Project Overview

A .NET library that wraps the Microsoft Graph SDK, providing a simplified and consistent interface for interacting with Microsoft Graph APIs. Handles paging, retry logic (Polly), and error handling.

## Build & Test

```bash
dotnet build Atc.Microsoft.Graph.Client.slnx          # Build all projects
dotnet build Atc.Microsoft.Graph.Client.slnx -c Release  # Release build (warnings as errors)
dotnet test --solution Atc.Microsoft.Graph.Client.slnx    # Run all tests
```

## Project Structure

- `src/Atc.Microsoft.Graph.Client/` - Main library
  - `Services/` - Service implementations organized by domain (Calendar, Contacts, Groups, OneDrive, OnlineMeetings, Outlook, Search, Sharepoint, Subscriptions, Teams, Users)
  - `Factories/` - `RequestConfigurationFactory` for OData query parameter configuration
  - `Extensions/` - `ServiceCollectionExtensions` for DI registration
  - `Options/` - `GraphServiceOptions` for client configuration
- `test/Atc.Microsoft.Graph.Client.Tests/` - Unit tests (xUnit v3, NSubstitute, FluentAssertions)
- `sample/Atc.Microsoft.Graph.Client.Sample/` - Sample console app

## Architecture Patterns

### Service Pattern
Each Graph API area follows: `IXxxGraphService` (interface) -> `XxxGraphService` (sealed class) -> `GraphServiceClientWrapper` (abstract base).

### Return Convention
- Read operations: `Task<(HttpStatusCode StatusCode, IList<T> Data)>` or `Task<(HttpStatusCode StatusCode, T? Data)>`
- Write operations: `Task<(HttpStatusCode StatusCode, bool Succeeded)>` or `Task<(HttpStatusCode StatusCode, T? Data)>`
- Create operations return `HttpStatusCode.Created` on success

### Paginated GET Pattern
Uses `ToGetRequestInformation` + `RequestAdapter.SendAsync` + `PageIterator` with `ResiliencePipeline`.

### Write Operation Patterns
- **Void POST** (send/reply/forward/delete): Wraps in `ResiliencePipeline.ExecuteAsync`, returns `(OK, true)` or `(InternalServerError, false)`
- **POST creating entity**: Wraps in `ResiliencePipeline.ExecuteAsync`, returns `(Created, entity)` or `(InternalServerError, null)`
- **PATCH update**: Direct call (no ResiliencePipeline), returns `(OK, entity)` or `(InternalServerError, null)`
- **DELETE**: Direct call, 404 treated as success, returns `(OK, true)` or `(InternalServerError, false)`

### Error Handling
- `ODataError` with specific status codes (404, 410) handled separately
- Generic `ODataError` and `Exception` caught and logged
- Source-generated logging via `[LoggerMessage]` attributes

### Resilience
- Polly retry pipeline in base class: 3 retries, exponential backoff with jitter
- Respects `Retry-After` headers from Graph API

## Code Style

- .NET 10.0, C# 14, nullable enabled, implicit usings
- StyleCop analyzers enforced (SA1518: files must end with newline)
- Warnings treated as errors in Release builds
- `[SuppressMessage]` for intentional deviations
- PascalCase for `[LoggerMessage]` placeholders
- All services registered as Singletons

## Testing Patterns

- Mock `IRequestAdapter` via NSubstitute (not `GraphServiceClient` directly)
- Standard test fixture: `IDisposable` with `requestAdapter`, `graphServiceClient`, `loggerFactory`, `sut`
- Test categories: null response, empty response, OData error, success, null guard (where applicable)
- Use `TestContext.Current.CancellationToken` for async tests
- Use `global::` prefix for `Microsoft.Graph.Search.Query` types in test project (namespace ambiguity with `Atc.Microsoft.Graph`)

## DI Registration

New services must be added to:
1. `ServiceCollectionExtensions.RegisterGraphServices()` - alphabetical order
2. `ServiceCollectionExtensionsTests` - all 4 registration/resolution test methods

## Adding a New Service

1. Add logging event IDs to `LoggingEventIdConstants.cs`
2. Add `[LoggerMessage]` methods to `GraphServiceClientWrapperLoggerMessages.cs`
3. Add global usings to `GlobalUsings.cs` (both src and test)
4. Add factory methods to `RequestConfigurationFactory.cs` (for GET endpoints only)
5. Create `IXxxGraphService.cs` and `XxxGraphService.cs` in `Services/Xxx/`
6. Register in `ServiceCollectionExtensions.cs`
7. Add tests following existing patterns
8. Update `ServiceCollectionExtensionsTests` and `RequestConfigurationFactoryTests`
