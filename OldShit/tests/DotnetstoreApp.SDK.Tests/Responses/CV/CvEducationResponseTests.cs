using DotnetstoreApp.SDK.Responses.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Responses.CV;

public sealed class CvEducationResponseTests
{
    [Fact]
    public void CvEducationResponse_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvEducationResponse);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == "Id" && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == "CvId" && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == "School" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "Level" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "StartDate" && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == "EndDate" && p.PropertyType == typeof(DateTime?));
    }
}