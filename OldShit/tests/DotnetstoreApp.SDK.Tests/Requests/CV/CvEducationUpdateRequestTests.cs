using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Requests.CV;

public sealed class CvEducationUpdateRequestTests
{
    [Fact]
    public void CvEducationUpdateRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvEducationUpdateRequest);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvEducationUpdateRequest.CvId) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvEducationUpdateRequest.School) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvEducationUpdateRequest.Level) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvEducationUpdateRequest.StartDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvEducationUpdateRequest.EndDate) && p.PropertyType == typeof(DateTime?));
    }
}