using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Requests.CV;

public sealed class CvUpdateRequestTests
{
    [Fact]
    public void CvUpdateRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvUpdateRequest);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvUpdateRequest.Name) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvUpdateRequest.Language) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvUpdateRequest.LastName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvUpdateRequest.FirstName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvUpdateRequest.DateOfBirth) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvUpdateRequest.Introduction) && p.PropertyType == typeof(string));
    }
}