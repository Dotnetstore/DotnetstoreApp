using DotnetstoreApp.SDK.Responses.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Responses.CV;

public sealed class CvResponseTests
{
    [Fact]
    public void CvResponse_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvResponse);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvResponse.Id) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvResponse.Name) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvResponse.Language) && p.PropertyType == typeof(string));
    }
}