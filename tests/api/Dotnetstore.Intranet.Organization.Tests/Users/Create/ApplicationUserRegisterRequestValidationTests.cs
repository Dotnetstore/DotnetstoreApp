using Dotnetstore.Intranet.Organization.Users.Create;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Users.Create;

public class ApplicationUserRegisterRequestValidationTests
{
    [Theory]
    [InlineData("Smith", "John", null, "1980-01-01", null, "test@test.com", "Password123", "Password123")]
    public void UserRegisterRequestValidation_Should_PassValidationWhenValidDataProvided(
        string lastName,
        string firstName,
        string? middleName,
        string birthDate,
        string? socialSecurityNumber,
        string email,
        string password,
        string confirmPassword)
    {
        // Arrange
        var dateOfBirth = DateTime.Parse(birthDate);
        var request = new ApplicationUserRegisterRequest(
            lastName,
            firstName,
            middleName,
            dateOfBirth,
            true,
            socialSecurityNumber,
            email,
            password,
            confirmPassword);
        var validator = new ApplicationUserRegisterRequestValidation();
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Theory]
    [InlineData("", "John", null, "1980-01-01", null, "test@test.com", "Password123", "Password123")]
    [InlineData("Smith", "", null, "1980-01-01", null, "test@test.com", "Password123", "Password123")]
    [InlineData("Smith", "John", null, "1930-01-01", null, "test@test.com", "Password123", "Password123")]
    [InlineData("Smith", "John", null, "1980-01-01", null, "testtest.com", "Password123", "Password123")]
    [InlineData("Smith", "John", null, "1980-01-01", null, "test@test.com", "", "Password123")]
    [InlineData("Smith", "John", null, "1980-01-01", null, "test@test.com", "Password123", "")]
    public void UserRegisterRequestValidation_Should_FailValidationWhenInvalidDataProvided(
        string lastName,
        string firstName,
        string? middleName,
        string birthDate,
        string? socialSecurityNumber,
        string email,
        string password,
        string confirmPassword)
    {
        // Arrange
        var dateOfBirth = DateTime.Parse(birthDate);
        var request = new ApplicationUserRegisterRequest(
            lastName,
            firstName,
            middleName,
            dateOfBirth,
            true,
            socialSecurityNumber,
            email,
            password,
            confirmPassword);
        var validator = new ApplicationUserRegisterRequestValidation();
        
        // Act
        var result = validator.Validate(request);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
}