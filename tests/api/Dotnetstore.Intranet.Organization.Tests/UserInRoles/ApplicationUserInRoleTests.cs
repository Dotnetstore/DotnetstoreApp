using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.UserInRoles;

public class ApplicationUserInRoleTests
{
    [Fact]
    public void ApplicationUserInRole_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUserInRole);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.Id) && p.PropertyType == typeof(ApplicationUserInRoleId));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.ApplicationUserRoleId) && p.PropertyType == typeof(ApplicationUserRoleId));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.ApplicationUserId) && p.PropertyType == typeof(ApplicationUserId));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.Role) && p.PropertyType == typeof(ApplicationUserRole));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.User) && p.PropertyType == typeof(ApplicationUser));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.CreatedDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.CreatedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.LastUpdatedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.LastUpdatedDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.IsDeleted) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.DeletedBy) && p.PropertyType == typeof(Guid?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.DeletedDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.IsSystem) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserInRole.IsGdpr) && p.PropertyType == typeof(bool));
        properties.Length.ShouldBe(14);
    }

    [Fact]
    public void ApplicationUserInRole_Should_Be_Created_With_Valid_Parameters()
    {
        // Arrange
        var id = ApplicationUserInRoleId.Create(Guid.NewGuid());
        var roleId = ApplicationUserRoleId.Create(Guid.NewGuid());
        var userId = ApplicationUserId.Create(Guid.NewGuid());
        var createdDate = DateTime.UtcNow;

        // Act
        var userInRole = ApplicationUserInRole.Create(
            id,
            roleId,
            userId,
            createdDate,
            createdBy: null,
            lastUpdatedBy: null,
            lastUpdatedDate: null,
            isDeleted: false,
            deletedBy: null,
            deletedDate: null,
            isSystem: false,
            isGdpr: false);

        // Assert
        userInRole.ShouldNotBeNull();
        userInRole.Id.ShouldBe(id);
        userInRole.ApplicationUserRoleId.ShouldBe(roleId);
        userInRole.ApplicationUserId.ShouldBe(userId);
        userInRole.CreatedDate.ShouldBe(createdDate);
        userInRole.CreatedBy.ShouldBeNull();
        userInRole.LastUpdatedBy.ShouldBeNull();
        userInRole.LastUpdatedDate.ShouldBeNull();
        userInRole.IsDeleted.ShouldBeFalse();
        userInRole.DeletedBy.ShouldBeNull();
        userInRole.DeletedDate.ShouldBeNull();
        userInRole.IsSystem.ShouldBeFalse();
        userInRole.IsGdpr.ShouldBeFalse();
        userInRole.Role.ShouldBeNull();
        userInRole.User.ShouldBeNull();
    }
}