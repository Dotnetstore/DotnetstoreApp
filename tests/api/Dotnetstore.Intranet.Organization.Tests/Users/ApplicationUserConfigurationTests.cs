using Dotnetstore.Intranet.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Users;

public class ApplicationUserConfigurationTests
{
    private readonly IMutableEntityType _entityType;
    
    public ApplicationUserConfigurationTests()
    {
        var builder = new ModelBuilder();
        var entityTypeBuilder = builder.Entity<ApplicationUser>();
        var configuration = new ApplicationUserConfiguration();
        
        configuration.Configure(entityTypeBuilder);
        _entityType = entityTypeBuilder.Metadata;
    }
    
    [Fact]
    public void ApplicationUserConfiguration_Should_Implement_IEntityTypeConfiguration()
    {
        // Arrange
        var configuration = new ApplicationUserConfiguration();

        // Act & Assert
        configuration.ShouldBeAssignableTo<IEntityTypeConfiguration<ApplicationUser>>();
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementIdCorrect()
    {
        // Assert
        _entityType.ShouldNotBeNull();
        var idProperty = _entityType.FindProperty(nameof(ApplicationUser.Id));
        idProperty.ShouldNotBeNull();
        idProperty.IsKey().ShouldBeTrue();
        idProperty.IsNullable.ShouldBeFalse();
        idProperty.GetMaxLength().ShouldBeNull(); // Guid does not have a max length
        idProperty.GetValueConverter().ShouldNotBeNull();
        idProperty.GetValueConverter()!.ConvertToProviderExpression.Body.ToString().ShouldContain("v.Value");
        idProperty.GetValueConverter()!.ConvertFromProviderExpression.Body.ToString().ShouldContain("Create(v)");
        idProperty.IsUniqueIndex().ShouldBeTrue();
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementEmailAddressCorrect()
    {
        // Assert
        var emailProperty = _entityType.FindProperty(nameof(ApplicationUser.EmailAddress));
        emailProperty.ShouldNotBeNull();
        emailProperty.IsNullable.ShouldBeFalse();
        emailProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserEmailMaxLength);
        emailProperty.IsUnicode()!.Value.ShouldBeFalse();
        emailProperty.IsUniqueIndex().ShouldBeTrue();
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementLastNameCorrect()
    {
        // Assert
        var lastNameProperty = _entityType.FindProperty(nameof(ApplicationUser.LastName));
        lastNameProperty.ShouldNotBeNull();
        lastNameProperty.IsNullable.ShouldBeFalse();
        lastNameProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserLastNameMaxLength);
        lastNameProperty.IsUnicode()!.Value.ShouldBeFalse();
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementFirstNameCorrect()
    {
        // Assert
        var firstNameProperty = _entityType.FindProperty(nameof(ApplicationUser.FirstName));
        firstNameProperty.ShouldNotBeNull();
        firstNameProperty.IsNullable.ShouldBeFalse();
        firstNameProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserFirstNameMaxLength);
        firstNameProperty.IsUnicode()!.Value.ShouldBeFalse();
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementMiddleNameCorrect()
    {
        // Assert
        var middleNameProperty = _entityType.FindProperty(nameof(ApplicationUser.MiddleName));
        middleNameProperty.ShouldNotBeNull();
        middleNameProperty.IsNullable.ShouldBeTrue();
        middleNameProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserMiddleNameMaxLength);
        middleNameProperty.IsUnicode()!.Value.ShouldBeFalse();
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementDateOfBirthCorrect()
    {
        // Assert
        var dateOfBirthProperty = _entityType.FindProperty(nameof(ApplicationUser.DateOfBirth));
        dateOfBirthProperty.ShouldNotBeNull();
        dateOfBirthProperty.IsNullable.ShouldBeFalse();
        dateOfBirthProperty.ClrType.ShouldBe(typeof(DateTime));
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementIsMaleCorrect()
    {
        // Assert
        var isMaleProperty = _entityType.FindProperty(nameof(ApplicationUser.IsMale));
        isMaleProperty.ShouldNotBeNull();
        isMaleProperty.IsNullable.ShouldBeFalse();
        isMaleProperty.ClrType.ShouldBe(typeof(bool));
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementSocialSecurityNumberCorrect()
    {
        // Assert
        var ssnProperty = _entityType.FindProperty(nameof(ApplicationUser.SocialSecurityNumber));
        ssnProperty.ShouldNotBeNull();
        ssnProperty.IsNullable.ShouldBeTrue();
        ssnProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserSocialSecurityNumberMaxLength);
        ssnProperty.IsUnicode()!.Value.ShouldBeFalse();
    }

    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementPasswordHashCorrect()
    {
        // Assert
        var passwordHashProperty = _entityType.FindProperty(nameof(ApplicationUser.PasswordHash));
        passwordHashProperty.ShouldNotBeNull();
        passwordHashProperty.IsNullable.ShouldBeFalse();
        passwordHashProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserPasswordMaxLength);
        passwordHashProperty.IsUnicode()!.Value.ShouldBeFalse();
    }
    
    [Fact]
    public void ApplicationUserConfiguration_Should_ImplementEmailAddressConfirmationCodeCorrect()
    {
        // Assert
        var confirmationCodeProperty = _entityType.FindProperty(nameof(ApplicationUser.EmailAddressConfirmationCode));
        confirmationCodeProperty.ShouldNotBeNull();
        confirmationCodeProperty.IsNullable.ShouldBeTrue();
        confirmationCodeProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserEmailConfirmationCodeMaxLength);
        confirmationCodeProperty.IsUnicode()!.Value.ShouldBeFalse();
    }
}