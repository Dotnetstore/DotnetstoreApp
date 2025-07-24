using DotnetstoreApp.SDK.Enums;
using DotnetstoreApp.SDK.Responses.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Responses.CV;

public class CvInformationResponseTests
{
    [Fact]
    public void CvInformationResponse_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvInformationResponse);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(p => p.Name == "Id" && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == "CvId" && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == "Name" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "InformationType" && p.PropertyType == typeof(CvInformationType));
    }
}