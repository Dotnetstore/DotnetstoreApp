using Dotnetstore.Intranet.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Users;

public class ApplicationUserIdTests
{
    [Fact]
    public void Create_ShouldReturnNewUserId()
    {
        // Arrange & Act
        var userId = ApplicationUserId.Create();

        // Assert
        userId.Value.ShouldNotBe(Guid.Empty);
        userId.Value.Version.ShouldBe(7);
        userId.ShouldBeOfType<ApplicationUserId>();
    }
    
    [Fact]
    public void Create_WithGuid_ShouldReturnUserIdWithSpecifiedValue()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var userId = ApplicationUserId.Create(guid);

        // Assert
        userId.Value.ShouldBe(guid);
        userId.ShouldBeOfType<ApplicationUserId>();
    }
    
    [Fact]
    public void ToString_ShouldReturnStringRepresentationOfValue()
    {
        // Arrange
        var userId = ApplicationUserId.Create();

        // Act
        var result = userId.ToString();

        // Assert
        result.ShouldBe(userId.Value.ToString());
    }
    
    [Fact]
    public void Equality_ShouldBeBasedOnValue()
    {
        // Arrange
        var userId1 = ApplicationUserId.Create();
        var userId2 = ApplicationUserId.Create(userId1.Value);

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
        var userId1 = ApplicationUserId.Create();
        var userId2 = ApplicationUserId.Create(userId1.Value);

        // Act
        var hash1 = userId1.GetHashCode();
        var hash2 = userId2.GetHashCode();

        // Assert
        hash1.ShouldBe(hash2);
    }
}