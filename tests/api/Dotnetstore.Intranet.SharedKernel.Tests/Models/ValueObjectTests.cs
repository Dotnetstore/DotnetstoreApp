using Dotnetstore.Intranet.SharedKernel.Models;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SharedKernel.Tests.Models;

public class ValueObjectTests
{
    private class TestValueObject(string value) : ValueObject
    {
        private string Value { get; } = value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
    
    [Fact]
    public void ValueObject_Equals_SameValues_ReturnsTrue()
    {
        // Arrange & Act
        var obj1 = new TestValueObject("test");
        var obj2 = new TestValueObject("test");

        // Assert
        obj1.Equals(obj2).ShouldBeTrue();
        (obj1 == obj2).ShouldBeTrue();
    }
    
    [Fact]
    public void ValueObject_Equals_DifferentValues_ReturnsFalse()
    {
        // Arrange & Act
        var obj1 = new TestValueObject("test1");
        var obj2 = new TestValueObject("test2");

        // Assert
        obj1.Equals(obj2).ShouldBeFalse();
        (obj1 == obj2).ShouldBeFalse();
    }
    
    [Fact]
    public void ValueObject_Equals_Null_ReturnsFalse()
    {
        // Arrange
        var obj1 = new TestValueObject("test");

        // Act & Assert
        obj1.Equals(null).ShouldBeFalse();
    }
    
    [Fact]
    public void ValueObject_Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        var obj1 = new TestValueObject("test");
        var obj2 = new object();

        // Act & Assert
        obj1.Equals(obj2).ShouldBeFalse();
    }
    
    [Fact]
    public void ValueObject_GetHashCode_SameValues_ReturnsSameHashCode()
    {
        // Arrange
        var obj1 = new TestValueObject("test");
        var obj2 = new TestValueObject("test");

        // Act & Assert
        obj1.GetHashCode().ShouldBe(obj2.GetHashCode());
    }
    
    [Fact]
    public void ValueObject_GetHashCode_DifferentValues_ReturnsDifferentHashCode()
    {
        // Arrange
        var obj1 = new TestValueObject("test1");
        var obj2 = new TestValueObject("test2");

        // Act & Assert
        obj1.GetHashCode().ShouldNotBe(obj2.GetHashCode());
    }
}