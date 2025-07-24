using Dotnetstore.Intranet.SharedKernel.Models;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SharedKernel.Tests.Models;

public class AggregateRootTests
{
    private class TestAggregateRoot(Guid id) : AggregateRoot<Guid>(id);
    
    [Fact]
    public void AggregateRoot_Should_Initialize_With_Id()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var aggregateRoot = new TestAggregateRoot(id);
        
        // Assert
        aggregateRoot.Id.ShouldBe(id);
    }
    
    [Fact]
    public void AggregateRoot_Should_Be_Of_Type_AggregateRoot()
    {
        // Arrange
        var id = Guid.NewGuid();
        
        // Act
        var aggregateRoot = new TestAggregateRoot(id);
        
        // Assert
        aggregateRoot.ShouldBeOfType<TestAggregateRoot>();
    }
}