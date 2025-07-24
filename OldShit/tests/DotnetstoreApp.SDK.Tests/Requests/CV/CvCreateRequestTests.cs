using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Requests.CV;

public sealed class CvCreateRequestTests
{
    [Fact]
    public void CvCreateRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvCreateRequest);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(p => p.Name == "Name" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "Language" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "LastName" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "FirstName" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "DateOfBirth" && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == "Introduction" && p.PropertyType == typeof(string));
    }
}