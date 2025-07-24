using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Educations;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Educations;

public sealed class CvEducationTests
{
    [Fact]
    public void CvEducation_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvEducation);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvEducation.Id) && p.PropertyType == typeof(CvEducationId));
        properties.ShouldContain(p => p.Name == nameof(CvEducation.CvId) && p.PropertyType == typeof(CvId));
        properties.ShouldContain(p => p.Name == nameof(CvEducation.School) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvEducation.Level) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvEducation.StartDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvEducation.EndDate) && p.PropertyType == typeof(DateTime?));
    }
    
    [Fact]
    public void CvEducation_WithoutId_Should_CreateWithCorrectValues()
    {
        // Arrange
        var cvId = CvId.Create();
        const string school = "Test School";
        const string level = "Bachelor's";
        var startDate = new DateTime(2020, 1, 1);
        var endDate = new DateTime(2024, 1, 1);

        // Act
        var education = CvEducation.Create(cvId, school, level, startDate, endDate);

        // Assert
        education.CvId.ShouldBe(cvId);
        education.School.ShouldBe(school);
        education.Level.ShouldBe(level);
        education.StartDate.ShouldBe(startDate);
        education.EndDate.ShouldBe(endDate);
    }
    
    [Fact]
    public void CvEducation_WithId_Should_CreateWithCorrectValues()
    {
        // Arrange
        var id = CvEducationId.Create();
        var cvId = CvId.Create();
        const string school = "Test School";
        const string level = "Bachelor's";
        var startDate = new DateTime(2020, 1, 1);
        var endDate = new DateTime(2024, 1, 1);

        // Act
        var education = CvEducation.Create(id, cvId, school, level, startDate, endDate);

        // Assert
        education.Id.ShouldBe(id);
        education.CvId.ShouldBe(cvId);
        education.School.ShouldBe(school);
        education.Level.ShouldBe(level);
        education.StartDate.ShouldBe(startDate);
        education.EndDate.ShouldBe(endDate);
    }
}