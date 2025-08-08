using Dotnetstore.Intranet.Web.Pages.Users.Models;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Web.Tests.Pages.Users.Models;

public class LoginModelTests
{
    [Fact]
    public void LoginModel_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(LoginModel);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(q => q.Name == nameof(LoginModel.Username) && q.PropertyType == typeof(string));
        properties.ShouldContain(q => q.Name == nameof(LoginModel.Password) && q.PropertyType == typeof(string));
        properties.Length.ShouldBe(2);
    }
}