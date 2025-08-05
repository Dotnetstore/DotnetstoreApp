using Dotnetstore.Intranet.Organization.UserInRoles;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.UserInRoles;

public class ApplicationUserInRoleBuilderTests
{
    [Fact]
    public void Builder_CanCreateUserInRoleWithAllProperties()
    {
        // Arrange & Act
        var userInRole = ApplicationUserInRoleBuilder.Create()
            .WithId()
            .WithUserId(Guid.NewGuid())
            .WithRoleId(Guid.NewGuid())
            .WithCreatedDate(DateTime.UtcNow)
            .WithCreatedBy()
            .Build();

        // Assert
        userInRole.ShouldNotBeNull();
        userInRole.Id.Value.ShouldNotBe(Guid.Empty);
        userInRole.ApplicationUserId.Value.ShouldNotBe(Guid.Empty);
        userInRole.ApplicationUserRoleId.Value.ShouldNotBe(Guid.Empty);
        userInRole.CreatedDate.ShouldNotBe(DateTime.MinValue);
        userInRole.CreatedBy.ShouldBeNull();
        userInRole.LastUpdatedBy.ShouldBeNull();
        userInRole.LastUpdatedDate.ShouldBeNull();
        userInRole.IsDeleted.ShouldBeFalse();
        userInRole.DeletedBy.ShouldBeNull();
        userInRole.DeletedDate.ShouldBeNull();
    }
}