using Dotnetstore.Intranet.Organization.Roles;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Roles;

public class ApplicationUserRoleIdTests
{
    [Fact]
    public void Create_ShouldReturnNewUserId()
    {
        // Arrange & Act
        var userId = ApplicationUserRoleId.Create();

        // Assert
        userId.Value.ShouldNotBe(Guid.Empty);
        userId.Value.Version.ShouldBe(7);
        userId.ShouldBeOfType<ApplicationUserRoleId>();
    }
    
    [Fact]
    public void Create_WithGuid_ShouldReturnUserRoleIdWithSpecifiedValue()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var userId = ApplicationUserRoleId.Create(guid);

        // Assert
        userId.Value.ShouldBe(guid);
        userId.ShouldBeOfType<ApplicationUserRoleId>();
    }
    
    [Fact]
    public void ToString_ShouldReturnStringRepresentationOfValue()
    {
        // Arrange
        var userId = ApplicationUserRoleId.Create();

        // Act
        var result = userId.ToString();

        // Assert
        result.ShouldBe(userId.Value.ToString());
    }
    
    [Fact]
    public void Equality_ShouldBeBasedOnValue()
    {
        // Arrange
        var userId1 = ApplicationUserRoleId.Create();
        var userId2 = ApplicationUserRoleId.Create(userId1.Value);

        // Act & Assert
        userId1.ShouldBe(userId2);
        userId1.Equals(userId2).ShouldBeTrue();
        (userId1 == userId2).ShouldBeTrue();
        (userId1 != userId2).ShouldBeFalse();
    }
    
    [Fact]
    public void GetHashCode_ShouldReturnHashBasedOnValue()
    {
        // Arrange
        var userId1 = ApplicationUserRoleId.Create();
        var userId2 = ApplicationUserRoleId.Create(userId1.Value);

        // Act
        var hash1 = userId1.GetHashCode();
        var hash2 = userId2.GetHashCode();

        // Assert
        hash1.ShouldBe(hash2);
    }
}