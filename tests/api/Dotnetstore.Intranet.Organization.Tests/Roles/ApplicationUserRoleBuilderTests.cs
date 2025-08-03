using Dotnetstore.Intranet.Organization.Roles;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Roles;

public class ApplicationUserRoleBuilderTests
{
    [Fact]
    public void ApplicationUserRoleBuilder_ShouldCreateValidRole()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Admin";
        var description = "Administrator role";
        
        // Act
        var role = ApplicationUserRoleBuilder.Create()
            .WithId(id)
            .WithName(name)
            .WithDescription(description)
            .WithCreatedDate(DateTime.UtcNow)
            .WithCreatedBy(Guid.NewGuid())
            .Build();

        // Assert
        role.Id.ShouldNotBeNull();
        role.Name.ShouldBe(name);
        role.Description.ShouldBe(description);
        role.CreatedDate.ShouldBeGreaterThan(DateTime.MinValue);
        role.CreatedBy.ShouldNotBeNull();
        role.IsDeleted.ShouldBeFalse();
        role.IsSystem.ShouldBeFalse();
        role.IsGdpr.ShouldBeFalse();
        role.LastUpdatedBy.ShouldBeNull();
        role.LastUpdatedDate.ShouldBeNull();
        role.DeletedBy.ShouldBeNull();
        role.DeletedDate.ShouldBeNull();
    }
}