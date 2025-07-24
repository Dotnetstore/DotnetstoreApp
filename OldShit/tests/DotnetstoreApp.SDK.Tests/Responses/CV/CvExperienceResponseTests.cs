using DotnetstoreApp.SDK.Responses.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Responses.CV;

public sealed class CvExperienceResponseTests
{
    [Fact]
    public void CvExperienceResponse_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvExperienceResponse);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.Id) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.CvId) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.StartDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.EndDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.Company) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.IsConsultant) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.Role) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.Extent) && p.PropertyType == typeof(int));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.Tools) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.CompanyNeeds) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceResponse.Mission) && p.PropertyType == typeof(string));
    }
}