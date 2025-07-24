using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Requests.CV;

public sealed class CvExperienceCreateRequestTests
{
    [Fact]
    public void CvExperienceCreateRequest_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvExperienceCreateRequest);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.CvId) && p.PropertyType == typeof(Guid));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.StartDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.EndDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.Company) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.IsConsultant) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.Role) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.Extent) && p.PropertyType == typeof(int));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.Tools) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.CompanyNeeds) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperienceCreateRequest.Mission) && p.PropertyType == typeof(string));
    }
}