using Dotnetstore.Intranet.Web.Pages.Users.Models;
using Dotnetstore.Intranet.Web.Pages.Users.Validators;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Web.Tests.Pages.Users.Validators;

public class LoginValidatorTests
{
    [Theory]
    [InlineData("test@test.com", "Password123!")]
    public void LoginValidator_CorrectValues_ShouldReturnIsValid(string username, string password)
    {
        // Arrange
        var validator = new LoginValidator();
        var model = new LoginModel
        {
            Username = username,
            Password = password
        };
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [InlineData("", "Password123!")]
    [InlineData("test@test.com", "")]
    [InlineData("", "")]
    [InlineData("test", "Password123!")]
    public void LoginValidator_InCorrectValues_ShouldReturnInvalid(string username, string password)
    {
        // Arrange
        var validator = new LoginValidator();
        var model = new LoginModel
        {
            Username = username,
            Password = password
        };
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
}