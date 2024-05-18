namespace Atc.Microsoft.Graph.Client.Options;

public sealed class GraphServiceOptions
{
    public string TenantId { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public string ClientSecret { get; set; } = string.Empty;

    public bool IsValid() => !string.IsNullOrEmpty(TenantId) &&
                             !string.IsNullOrEmpty(ClientId) &&
                             !string.IsNullOrEmpty(ClientSecret);

    public override string ToString()
        => $"{nameof(TenantId)}: {TenantId}, {nameof(ClientId)}: {ClientId}, {nameof(ClientSecret)}: {ClientSecret}";
}