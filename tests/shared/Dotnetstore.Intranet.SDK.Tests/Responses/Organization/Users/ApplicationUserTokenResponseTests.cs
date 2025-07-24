using Dotnetstore.Intranet.SDK.Responses.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SDK.Tests.Responses.Organization.Users;

public class ApplicationUserTokenResponseTests
{
    [Fact]
    public void UserTokenResponse_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUserTokenResponse);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserTokenResponse.Token) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserTokenResponse.RefreshToken) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserTokenResponse.UserId) && p.PropertyType == typeof(Guid));
        properties.Length.ShouldBe(3);
    }
}