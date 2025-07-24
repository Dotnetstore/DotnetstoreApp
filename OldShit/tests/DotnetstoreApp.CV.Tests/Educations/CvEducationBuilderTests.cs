using DotnetstoreApp.CV.Educations;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Educations;

public sealed class CvEducationBuilderTests
{
    [Fact]
    public void Create_WithoutId_Should_ReturnCvEducation()
    {
        // Arrange
        var cvId = Guid.NewGuid();
        const string school = "Test School";
        const string level = "Bachelor's Degree";
        var startDate = new DateTime(2020, 1, 1);
        var endDate = new DateTime(2024, 1, 1);

        // Act
        var education = CvEducationBuilder.Create()
            .WithId()
            .WithCvId(cvId)
            .WithSchool(school)
            .WithLevel(level)
            .WithStartDate(startDate)
            .WithEndDate(endDate)
            .Build();

        // Assert
        Assert.NotNull(education);
        Assert.Equal(cvId, education.CvId.Value);
        Assert.Equal(school, education.School);
        Assert.Equal(level, education.Level);
        Assert.Equal(startDate, education.StartDate);
        Assert.Equal(endDate, education.EndDate);
    }
    
    [Fact]
    public void Create_WithId_Should_ReturnCvEducation()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cvId = Guid.NewGuid();
        const string school = "Test School";
        const string level = "Bachelor's Degree";
        var startDate = new DateTime(2020, 1, 1);
        var endDate = new DateTime(2024, 1, 1);

        // Act
        var education = CvEducationBuilder.Create()
            .WithId(id)
            .WithCvId(cvId)
            .WithSchool(school)
            .WithLevel(level)
            .WithStartDate(startDate)
            .WithEndDate(endDate)
            .Build();

        // Assert
        Assert.NotNull(education);
        Assert.Equal(id, education.Id.Value);
        Assert.Equal(cvId, education.CvId.Value);
        Assert.Equal(school, education.School);
        Assert.Equal(level, education.Level);
        Assert.Equal(startDate, education.StartDate);
        Assert.Equal(endDate, education.EndDate);
    }
}