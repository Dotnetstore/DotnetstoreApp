using Dotnetstore.Intranet.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Users;

public class ApplicationUserBuilderTests
{
    [Fact]
    public void Builder_CanCreateUserWithAllProperties()
    {
        // Arrange & Act
        var user = ApplicationUserBuilder.Create()
            .WithId()
            .WithLastName("Doe")
            .WithFirstName("John")
            .WithMiddleName("A")
            .WithDateOfBirth(new DateTime(1990, 1, 1))
            .WithIsMale(true)
            .WithSocialSecurityNumber("123-45-6789")
            .WithEmailAddress("test@test.com")
            .WithPasswordHash("hashedpassword")
            .WithCreatedDate(DateTime.UtcNow)
            .WithCreatedBy()
            .Build();

        // Assert
        user.ShouldNotBeNull();
        user.Id.Value.ShouldNotBe(Guid.Empty);
        user.LastName.ShouldBe("Doe");
        user.FirstName.ShouldBe("John");
        user.MiddleName.ShouldBe("A");
        user.DateOfBirth.ShouldBe(new DateTime(1990, 1, 1));
        user.IsMale.ShouldBeTrue();
        user.SocialSecurityNumber.ShouldBe("123-45-6789");
        user.EmailAddress.ShouldBe("test@test.com");
        user.PasswordHash.ShouldBe("hashedpassword");
        user.CreatedDate.ShouldNotBe(DateTime.MinValue);
        user.CreatedBy.ShouldBeNull();
        user.LastUpdatedBy.ShouldBeNull();
        user.LastUpdatedDate.ShouldBeNull();
        user.IsDeleted.ShouldBeFalse();
        user.DeletedBy.ShouldBeNull();
        user.DeletedDate.ShouldBeNull();
        user.IsSystem.ShouldBeFalse();
        user.IsGdpr.ShouldBeFalse();
        user.AccountIsApproved.ShouldBeFalse();
        user.EmailAddressConfirmationCode.ShouldBeNull();
        user.EmailAddressIsConfirmed.ShouldBeFalse();
        user.RefreshToken.ShouldBeNull();
        user.RefreshTokenExpiryTime.ShouldBeNull();
        user.ConcurrencyToken.ShouldBeNull();
    }
}