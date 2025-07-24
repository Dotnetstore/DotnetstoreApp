using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Requests.CV;

public sealed class CvExperienceUpdateRequestTests
{
    [Fact]
    public void CvExperienceCreateRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvExperienceUpdateRequest);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.CvId) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.StartDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.EndDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.Company) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.IsConsultant) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.Role) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.Extent) && p.PropertyType == typeof(int));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.Tools) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.CompanyNeeds) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceUpdateRequest.Mission) && p.PropertyType == typeof(string));
    }
}