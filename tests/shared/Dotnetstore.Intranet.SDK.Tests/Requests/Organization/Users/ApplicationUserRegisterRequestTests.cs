using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SDK.Tests.Requests.Organization.Users;

public class ApplicationUserRegisterRequestTests
{
    [Fact]
    public void UserRegisterRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUserRegisterRequest);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.LastName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.FirstName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.MiddleName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.DateOfBirth) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.IsMale) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.SocialSecurityNumber) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.EmailAddress) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.Password) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRegisterRequest.ConfirmPassword) && p.PropertyType == typeof(string));
        properties.Length.ShouldBe(9);
    }
}