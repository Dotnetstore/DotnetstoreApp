using DotnetstoreApp.CV.Experiences;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Experiences;

public sealed class CvExperienceBuilderTests
{
    [Fact]
    public void Create_Should_ReturnCvExperience()
    {
        // Arrange
        var experience = CvExperienceBuilder.Create()
            .WithId()
            .WithCvId(Guid.NewGuid())
            .WithStartDate(DateTime.UtcNow)
            .WithEndDate(DateTime.UtcNow.AddYears(1))
            .WithCompany("Test Company")
            .WithIsConsultant(false)
            .WithRole("Developer")
            .WithExtent(100)
            .WithTools("C#, ASP.NET")
            .WithCompanyNeeds("Need skilled developers")
            .WithMission("Develop software solutions")
            .Build();

        // Act & Assert
        experience.ShouldNotBeNull();
        experience.Id.Value.ShouldNotBe(Guid.Empty);
        experience.CvId.Value.ShouldNotBe(Guid.Empty);
        experience.Company.ShouldBe("Test Company");
        experience.Role.ShouldBe("Developer");
        experience.Extent.ShouldBe(100);
        experience.Tools.ShouldBe("C#, ASP.NET");
        experience.CompanyNeeds.ShouldBe("Need skilled developers");
        experience.Mission.ShouldBe("Develop software solutions");
    }
}