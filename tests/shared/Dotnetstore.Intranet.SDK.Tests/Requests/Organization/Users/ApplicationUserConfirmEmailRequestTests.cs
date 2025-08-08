using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SDK.Tests.Requests.Organization.Users;

public class ApplicationUserConfirmEmailRequestTests
{
    [Fact]
    public void ApplicationUserConfirmEmailRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUserConfirmEmailRequest);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserConfirmEmailRequest.Code) && p.PropertyType == typeof(string));
        properties.Length.ShouldBe(1);
    }
}