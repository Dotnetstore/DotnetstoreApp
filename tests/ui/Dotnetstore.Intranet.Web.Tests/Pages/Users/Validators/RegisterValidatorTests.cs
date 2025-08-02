using Dotnetstore.Intranet.SDK.Services;
using Dotnetstore.Intranet.Web.Pages.Users.Models;
using Dotnetstore.Intranet.Web.Pages.Users.Validators;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Web.Tests.Pages.Users.Validators;

public class RegisterValidatorTests
{
    [Fact]
    public void RegisterValidator_CorrectValues_ShouldReturnIsValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void RegisterValidator_EmptyLastName_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(lastName: "", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }

    [Fact]
    public void RegisterValidator_TooLongLastName_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(lastName: new string('a', DataSchemeConstants.UserLastNameMaxLength + 1), dateOfBirth: DateTime.Now.AddYears(-20));

        // Act
        var result = validator.Validate(model);

        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_EmptyFirstName_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(firstName: "", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_TooLongFirstName_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(firstName: new string('a', DataSchemeConstants.UserFirstNameMaxLength + 1), dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_EmptyMiddleName_ShouldReturnIsValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(middleName: "", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void RegisterValidator_TooLongMiddleName_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(middleName: new string('a', DataSchemeConstants.UserMiddleNameMaxLength + 1), dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_EmptyDateOfBirth_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(dateOfBirth: null);
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_TooYoungDateOfBirth_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(dateOfBirth: DateTime.Now.AddYears(DataSchemeConstants.UserDateOfBirthMax + 1));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_TooOldDateOfBirth_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(dateOfBirth: DateTime.Now.AddYears(DataSchemeConstants.UserDateOfBirthMin - 1));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_ValidDateOfBirth_ShouldReturnIsValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void RegisterValidator_EmptySocialSecurityNumber_ShouldReturnIsValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(socialSecurityNumber: null, dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void RegisterValidator_TooLongSocialSecurityNumber_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(socialSecurityNumber: new string('1', DataSchemeConstants.UserSocialSecurityNumberMaxLength + 1), dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_InvalidSocialSecurityNumber_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(socialSecurityNumber: "123456789", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_ValidSocialSecurityNumber_ShouldReturnIsValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(socialSecurityNumber: "19851212-3459", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void RegisterValidator_EmptyEmailAddress_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(emailAddress: "", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_InvalidEmailAddress_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(emailAddress: "invalid-email", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_TooLongEmailAddress_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(emailAddress: new string('a', DataSchemeConstants.UserEmailMaxLength + 1) + "@test.com", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_ValidEmailAddress_ShouldReturnIsValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(emailAddress: "test@test.com", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
    
    [Fact]
    public void RegisterValidator_EmptyPassword_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(password: "", confirmPassword: "", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_TooLongPassword_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(password: new string('a', DataSchemeConstants.UserPasswordMaxLength + 1), confirmPassword: new string('a', DataSchemeConstants.UserPasswordMaxLength + 1), dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_PasswordsDoNotMatch_ShouldReturnIsNotValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(password: "Password123!", confirmPassword: "DifferentPassword123!", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeFalse();
    }
    
    [Fact]
    public void RegisterValidator_ValidPassword_ShouldReturnIsValid()
    {
        // Arrange
        var validator = new RegisterValidator();
        var model = GetModel(password: "Password123!", confirmPassword: "Password123!", dateOfBirth: DateTime.Now.AddYears(-20));
        
        // Act
        var result = validator.Validate(model);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }

    private static RegisterModel GetModel(
        string lastName = "Doe",
        string firstName = "John",
        string middleName = "A",
        DateTime? dateOfBirth = null,
        string? socialSecurityNumber = null,
        string emailAddress = "test@test.com",
        string password = "Password123!",
        string confirmPassword = "Password123!")
    {
        return new RegisterModel
        {
            LastName = lastName,
            FirstName = firstName,
            MiddleName = middleName,
            DateOfBirth = dateOfBirth,
            SocialSecurityNumber = socialSecurityNumber,
            EmailAddress = emailAddress,
            Password = password,
            ConfirmPassword = confirmPassword
        };
    }
}