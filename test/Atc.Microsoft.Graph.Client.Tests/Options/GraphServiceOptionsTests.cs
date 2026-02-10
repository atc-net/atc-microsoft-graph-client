namespace Atc.Microsoft.Graph.Client.Tests.Options;

public sealed class GraphServiceOptionsTests
{
    [Fact]
    public void IsValid_AllPropertiesSet_ReturnsTrue()
    {
        // Arrange
        var sut = new GraphServiceOptions
        {
            TenantId = "tenant-id",
            ClientId = "client-id",
            ClientSecret = "client-secret",
        };

        // Act
        var result = sut.IsValid();

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("", "client-id", "client-secret")]
    [InlineData("tenant-id", "", "client-secret")]
    [InlineData("tenant-id", "client-id", "")]
    [InlineData("", "", "")]
    public void IsValid_MissingProperties_ReturnsFalse(
        string tenantId,
        string clientId,
        string clientSecret)
    {
        // Arrange
        var sut = new GraphServiceOptions
        {
            TenantId = tenantId,
            ClientId = clientId,
            ClientSecret = clientSecret,
        };

        // Act
        var result = sut.IsValid();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void ToString_ContainsTenantAndClientId()
    {
        // Arrange
        var sut = new GraphServiceOptions
        {
            TenantId = "my-tenant",
            ClientId = "my-client",
            ClientSecret = "super-secret-value",
        };

        // Act
        var result = sut.ToString();

        // Assert
        result.Should().Contain("TenantId: my-tenant");
        result.Should().Contain("ClientId: my-client");
        result.Should().NotContain("super-secret-value");
    }
}