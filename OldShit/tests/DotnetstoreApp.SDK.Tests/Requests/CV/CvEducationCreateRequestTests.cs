using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Requests.CV;

public sealed class CvEducationCreateRequestTests
{
    [Fact]
    public void CvEducationCreateRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvEducationCreateRequest);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvEducationCreateRequest.CvId) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvEducationCreateRequest.School) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvEducationCreateRequest.Level) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvEducationCreateRequest.StartDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvEducationCreateRequest.EndDate) && p.PropertyType == typeof(DateTime?));
    }
}