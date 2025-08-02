using Dotnetstore.Intranet.Web.Pages.Users.Models;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Web.Tests.Pages.Users.Models;

public class RegisterModelTests
{
    [Fact]
    public void RegisterModel_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(RegisterModel);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.LastName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.FirstName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.MiddleName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.DateOfBirth) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.IsMale) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.SocialSecurityNumber) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.EmailAddress) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.Password) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(RegisterModel.ConfirmPassword) && p.PropertyType == typeof(string));
        properties.Length.ShouldBe(9, "RegisterModel should have exactly 9 properties.");
    }
}