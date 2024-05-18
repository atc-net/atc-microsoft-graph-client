namespace Atc.Microsoft.Graph.Client.Options;

public sealed class GraphServiceOptions
{
    public string TenantId { get; set; } = string.Empty;

    public string? ClientId { get; set; }

    public string? ClientSecret { get; set; }
}