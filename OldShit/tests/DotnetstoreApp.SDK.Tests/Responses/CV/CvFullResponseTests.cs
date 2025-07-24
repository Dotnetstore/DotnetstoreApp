using DotnetstoreApp.SDK.Responses.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Responses.CV;

public sealed class CvFullResponseTests
{
    [Fact]
    public void CvFullResponse_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvFullResponse);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.Id) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.Name) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.Language) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.LastName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.FirstName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.DateOfBirth) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.Introduction) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.Experiences) && p.PropertyType == typeof(List<CvExperienceResponse>));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.Information) && p.PropertyType == typeof(List<CvInformationResponse>));
        properties.ShouldContain(p => p.Name == nameof(CvFullResponse.Educations) && p.PropertyType == typeof(List<CvEducationResponse>));
    }
}