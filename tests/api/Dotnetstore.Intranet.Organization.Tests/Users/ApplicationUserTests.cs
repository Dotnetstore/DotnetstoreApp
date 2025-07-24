using Dotnetstore.Intranet.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Users;

public class ApplicationUserTests
{
    [Fact]
    public void ApplicationUser_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUser);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.Id) && p.PropertyType == typeof(ApplicationUserId));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.LastName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.FirstName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.MiddleName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.DateOfBirth) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.IsMale) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.SocialSecurityNumber) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.EmailAddress) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.PasswordHash) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.CreatedDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.CreatedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.LastUpdatedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.LastUpdatedDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.IsDeleted) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.DeletedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.DeletedDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.IsSystem) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.IsGdpr) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.RefreshToken) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.RefreshTokenExpiryTime) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUser.ConcurrencyToken) && p.PropertyType == typeof(byte[]));
        properties.Length.ShouldBe(21);
    }

    [Fact]
    public void Create_WithId_Should_ReturnValidApplicationUser()
    {
        // Arrange
        var id = ApplicationUserId.Create(Guid.NewGuid());
        const string lastName = "Doe";
        const string firstName = "John";
        const string middleName = "A";
        var dateOfBirth = new DateTime(1990, 1, 1);
        const bool isMale = true;
        const string socialSecurityNumber = "123-45-6789";
        const string emailAddress = "test@test.com";
        const string passwordHash = "hashedpassword";
        var createdDate = DateTime.UtcNow;
        var createdBy = Guid.NewGuid();
        var lastUpdatedBy = Guid.NewGuid();
        var lastUpdatedDate = DateTime.UtcNow.AddDays(-1);
        const bool isDeleted = false;
        var deletedBy = (Guid?)null;
        var deletedDate = (DateTime?)null;

        // Act
        var user = ApplicationUser.Create(
            id,
            lastName,
            firstName,
            middleName,
            dateOfBirth,
            isMale,
            socialSecurityNumber,
            emailAddress,
            passwordHash,
            createdDate,
            createdBy,
            lastUpdatedBy,
            lastUpdatedDate,
            isDeleted,
            deletedBy,
            deletedDate);

        // Assert
        user.ShouldNotBeNull();
    }

    [Fact]
    public void Create_WithoutId_Should_ReturnValidApplicationUser()
    {
        // Arrange
        const string lastName = "Doe";
        const string firstName = "John";
        const string middleName = "A";
        var dateOfBirth = new DateTime(1990, 1, 1);
        const bool isMale = true;
        const string socialSecurityNumber = "123-45-6789";
        const string emailAddress = "test@test.com";
        const string passwordHash = "hashedpassword";
        var createdDate = DateTime.UtcNow;
        var createdBy = Guid.NewGuid();
        var lastUpdatedBy = Guid.NewGuid();
        var lastUpdatedDate = DateTime.UtcNow.AddDays(-1);
        const bool isDeleted = false;
        var deletedBy = (Guid?)null;
        var deletedDate = (DateTime?)null;

        // Act
        var user = ApplicationUser.Create(
            lastName,
            firstName,
            middleName,
            dateOfBirth,
            isMale,
            socialSecurityNumber,
            emailAddress,
            passwordHash,
            createdDate,
            createdBy,
            lastUpdatedBy,
            lastUpdatedDate,
            isDeleted,
            deletedBy,
            deletedDate);

        // Assert
        user.ShouldNotBeNull();
    }
}