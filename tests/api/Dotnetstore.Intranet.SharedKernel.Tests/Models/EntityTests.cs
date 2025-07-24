using Dotnetstore.Intranet.SharedKernel.Models;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SharedKernel.Tests.Models;

public class EntityTests
{
    private class TestUserForEntity : Entity<Guid>
    {
        public TestUserForEntity(Guid id) : base(id)
        {
        }
    }
    
    [Fact]
    public void Constructor_SetsId()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var entity = new TestUserForEntity(id);

        // Assert
        entity.Id.ShouldBe(id);
    }

    [Fact]
    public void Entities_WithSameId_AreEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var user1 = new TestUserForEntity(id);
        var user2 = new TestUserForEntity(id);

        // Assert
        user1.ShouldBe(user2);
    }
    
    [Fact]
    public void Entities_WithDifferentIds_AreNotEqual()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        
        // Act
        var user1 = new TestUserForEntity(id1);
        var user2 = new TestUserForEntity(id2);

        // Assert
        user1.ShouldNotBe(user2);
    }
    
    [Fact]
    public void Entities_WithSameId_HaveSameHashCode()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var user1 = new TestUserForEntity(id);
        var user2 = new TestUserForEntity(id);

        // Assert
        user1.GetHashCode().ShouldBe(user2.GetHashCode());
    }
    
    [Fact]
    public void Entities_WithDifferentIds_HaveDifferentHashCodes()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        
        // Act
        var user1 = new TestUserForEntity(id1);
        var user2 = new TestUserForEntity(id2);

        // Assert
        user1.GetHashCode().ShouldNotBe(user2.GetHashCode());
    }
    
    [Fact]
    public void Entity_Equals_Null_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new TestUserForEntity(id);

        // Act
        var result = entity.Equals(null);

        // Assert
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void Entity_Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new TestUserForEntity(id);
        var differentType = new object();

        // Act
        var result = entity.Equals(differentType);

        // Assert
        result.ShouldBeFalse();
    }
    
    [Fact]
    public void Entity_Equals_SameInstance_ReturnsTrue()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new TestUserForEntity(id);

        // Act
        var result = entity.Equals(entity);

        // Assert
        result.ShouldBeTrue();
    }
}