using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SDK.Tests.Requests.Organization.Users;

public class ApplicationUserLoginRequestTests
{
    [Fact]
    public void UserLoginRequest_Should_ShouldContainCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUserLoginRequest);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserLoginRequest.Username) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserLoginRequest.Password) && p.PropertyType == typeof(string));
        properties.Length.ShouldBe(2);
    }
}