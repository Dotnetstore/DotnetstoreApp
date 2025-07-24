using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Educations;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Educations;

public sealed class CvEducationMappersTests
{
    [Fact]
    public void ToCvEducationResponse_Should_MapPropertiesCorrectly()
    {
        // Arrange
        var education = CvEducation.Create(
            CvId.Create(Guid.NewGuid()),
            "Test School",
            "High school",
            new DateTime(2020, 1, 1),
            new DateTime(2024, 1, 1));

        // Act
        var response = education.ToCvEducationResponse();

        // Assert
        response.Id.ShouldBe(education.Id.Value);
        response.CvId.ShouldBe(education.CvId.Value);
        response.School.ShouldBe(education.School);
        response.Level.ShouldBe(education.Level);
        response.StartDate.ShouldBe(education.StartDate);
        response.EndDate.ShouldBe(education.EndDate);
    }
}