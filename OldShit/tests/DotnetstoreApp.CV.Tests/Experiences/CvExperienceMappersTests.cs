using DotnetstoreApp.CV.Experiences;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Experiences;

public sealed class CvExperienceMappersTests
{
    [Fact]
    public void ToCvExperienceResponse_Should_MapPropertiesCorrectly()
    {
        // Arrange
        var experience = CvExperienceBuilder.Create()
            .WithId()
            .WithCvId(Guid.CreateVersion7())
            .WithStartDate(new DateTime(2020, 1, 1))
            .WithEndDate(new DateTime(2020, 12, 31))
            .WithCompany("Test Company")
            .WithIsConsultant(false)
            .WithRole("Developer")
            .WithExtent(100)
            .WithTools("C#, .NET")
            .WithCompanyNeeds("Software Development")
            .WithMission("Developing applications")
            .Build();

        // Act
        var response = experience.ToCvExperienceResponse();

        // Assert
        response.Id.ShouldNotBe(Guid.Empty);
        response.CvId.ShouldBe(experience.CvId.Value);
        response.StartDate.ShouldBe(experience.StartDate);
        response.EndDate.ShouldBe(experience.EndDate);
        response.Company.ShouldBe(experience.Company);
        response.IsConsultant.ShouldBe(experience.IsConsultant);
        response.Role.ShouldBe(experience.Role);
        response.Extent.ShouldBe(experience.Extent);
        response.Tools.ShouldBe(experience.Tools);
        response.CompanyNeeds.ShouldBe(experience.CompanyNeeds);
        response.Mission.ShouldBe(experience.Mission);
    }
}