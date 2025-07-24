using DotnetstoreApp.CV.Cvs;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Cvs;

public sealed class CvIdTests
{
    [Fact]
    public void Create_ShouldReturnNewId()
    {
        // Act
        var id = CvId.Create();

        // Assert
        id.ShouldNotBeNull();
        id.Value.ShouldNotBe(Guid.Empty);
    }
    
    [Fact]
    public void Create_WithGuid_ShouldReturnIdWithGivenValue()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var id = CvId.Create(guid);

        // Assert
        id.ShouldNotBeNull();
        id.Value.ShouldBe(guid);
    }
    
    [Fact]
    public void ToString_ShouldReturnStringRepresentationOfValue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id = CvId.Create(guid);

        // Act
        var result = id.ToString();

        // Assert
        result.ShouldBe(guid.ToString());
    }
    
    [Fact]
    public void Equality_ShouldBeBasedOnValue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id1 = CvId.Create(guid);
        var id2 = CvId.Create(guid);
        var id3 = CvId.Create(Guid.NewGuid());

        // Act & Assert
        id1.ShouldBe(id2);
        id1.ShouldNotBe(id3);
        id2.ShouldNotBe(id3);
    }
    
    [Fact]
    public void GetHashCode_ShouldReturnHashBasedOnValue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id = CvId.Create(guid);

        // Act
        var hashCode = id.GetHashCode();

        // Assert
        hashCode.ShouldBe(id.GetHashCode());
    }
}