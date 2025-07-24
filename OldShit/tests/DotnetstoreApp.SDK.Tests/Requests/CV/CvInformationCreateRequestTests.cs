using DotnetstoreApp.SDK.Enums;
using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Requests.CV;

public sealed class CvInformationCreateRequestTests
{
    [Fact]
    public void CvInformationCreateRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvInformationCreateRequest);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == "CvId" && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == "Name" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "CvInformationType" && p.PropertyType == typeof(CvInformationType));
        properties.Length.ShouldBe(3);
    }
}