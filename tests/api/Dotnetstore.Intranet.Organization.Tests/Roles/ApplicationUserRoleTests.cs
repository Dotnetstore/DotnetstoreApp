using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Roles;

public class ApplicationUserRoleTests
{
    [Fact]
    public void ApplicationUserRole_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUserRole);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.Id) && p.PropertyType == typeof(ApplicationUserRoleId));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.Name) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.Description) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.CreatedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.CreatedDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.LastUpdatedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.LastUpdatedDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.IsDeleted) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.DeletedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.DeletedDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.IsSystem) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.IsGdpr) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserRole.ApplicationUserInRoles) && p.PropertyType == typeof(ICollection<ApplicationUserInRole>));
        properties.Length.ShouldBe(13);
    }
    
    [Fact]
    public void Create_Should_Return_ApplicationUserRole_Instance()
    {
        // Arrange
        var id = ApplicationUserRoleId.Create(Guid.NewGuid());
        const string name = "Admin";
        const string description = "Administrator role";
        var createdDate = DateTime.UtcNow;

        // Act
        var role = ApplicationUserRole.Create(id, name, description, createdDate);

        // Assert
        role.ShouldNotBeNull();
        role.Id.ShouldBe(id);
        role.Name.ShouldBe(name);
        role.Description.ShouldBe(description);
        role.CreatedDate.ShouldBe(createdDate);
        role.IsDeleted.ShouldBeFalse();
        role.IsSystem.ShouldBeFalse();
        role.IsGdpr.ShouldBeFalse();
    }

    [Fact]
    public void Create_Should_Set_OptionalParameters_Correctly()
    {
        // Arrange
        var id = ApplicationUserRoleId.Create(Guid.NewGuid());
        const string name = "User";
        const string description = "Regular user role";
        var createdDate = DateTime.UtcNow;
        var createdBy = Guid.NewGuid();
        var lastUpdatedBy = Guid.NewGuid();
        var lastUpdatedDate = DateTime.UtcNow.AddMinutes(5);
        const bool isDeleted = true;
        var deletedBy = Guid.NewGuid();
        var deletedDate = DateTime.UtcNow.AddMinutes(10);
        const bool isSystem = true;
        const bool isGdpr = true;

        // Act
        var role = ApplicationUserRole.Create(
            id,
            name,
            description,
            createdDate,
            createdBy,
            lastUpdatedBy,
            lastUpdatedDate,
            isDeleted,
            deletedBy,
            deletedDate,
            isSystem,
            isGdpr);

        // Assert
        role.ShouldNotBeNull();
        role.Id.ShouldBe(id);
        role.Name.ShouldBe(name);
        role.Description.ShouldBe(description);
        role.CreatedDate.ShouldBe(createdDate);
        role.CreatedBy.ShouldBe(createdBy);
        role.LastUpdatedBy.ShouldBe(lastUpdatedBy);
        role.LastUpdatedDate.ShouldBe(lastUpdatedDate);
        role.IsDeleted.ShouldBe(isDeleted);
        role.DeletedBy.ShouldBe(deletedBy);
        role.DeletedDate.ShouldBe(deletedDate);
        role.IsSystem.ShouldBe(isSystem);
        role.IsGdpr.ShouldBe(isGdpr);
    }

    [Fact]
    public void Create_Should_Set_DefaultValues_When_OptionalParameters_Are_Not_Provided()
    {
        // Arrange
        var id = ApplicationUserRoleId.Create(Guid.NewGuid());
        const string name = "Guest";
        const string description = "Guest role";
        var createdDate = DateTime.UtcNow;

        // Act
        var role = ApplicationUserRole.Create(id, name, description, createdDate);

        // Assert
        role.ShouldNotBeNull();
        role.Id.ShouldBe(id);
        role.Name.ShouldBe(name);
        role.Description.ShouldBe(description);
        role.CreatedDate.ShouldBe(createdDate);
        role.CreatedBy.ShouldBeNull();
        role.LastUpdatedBy.ShouldBeNull();
        role.LastUpdatedDate.ShouldBeNull();
        role.IsDeleted.ShouldBeFalse();
        role.DeletedBy.ShouldBeNull();
        role.DeletedDate.ShouldBeNull();
        role.IsSystem.ShouldBeFalse();
        role.IsGdpr.ShouldBeFalse();
    }
}