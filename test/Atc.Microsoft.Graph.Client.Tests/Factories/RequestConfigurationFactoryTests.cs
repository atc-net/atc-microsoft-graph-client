namespace Atc.Microsoft.Graph.Client.Tests.Factories;

public sealed class RequestConfigurationFactoryTests
{
    [Fact]
    public void CreateForUsers_WithAllParameters_ReturnsAction()
    {
        // Arrange
        var expand = new List<string> { "manager" };
        const string filter = "startsWith(displayName, 'A')";
        var select = new List<string> { "id", "displayName" };

        // Act
        var action = RequestConfigurationFactory.CreateForUsers(expand, filter, select);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForUsers_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForUsers(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForTeams_WithAllParameters_ReturnsAction()
    {
        // Arrange
        var expand = new List<string> { "members" };
        const string filter = "displayName eq 'Team1'";
        var select = new List<string> { "id" };

        // Act
        var action = RequestConfigurationFactory.CreateForTeams(expand, filter, select);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForSites_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForSites(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForDrives_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForDrives(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForItems_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForItems(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForItemsWithDelta_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForItemsWithDelta(
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForChildFolders_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForChildFolders(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForMailFolders_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForMailFolders(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForMessagesMailFolder_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForMessagesMailFolder(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForMessagesUserId_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForMessagesUserId(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForMessagesDelta_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForMessagesDelta(
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForAttachments_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForAttachments(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForContacts_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForContacts(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForContactById_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForContactById(
            expandQueryParameters: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForOnlineMeetings_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForOnlineMeetings(
            expandQueryParameters: null,
            filterQueryParameter: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }

    [Fact]
    public void CreateForOnlineMeetingById_WithNullParameters_ReturnsAction()
    {
        // Act
        var action = RequestConfigurationFactory.CreateForOnlineMeetingById(
            expandQueryParameters: null,
            selectQueryParameters: null);

        // Assert
        action.Should().NotBeNull();
    }
}