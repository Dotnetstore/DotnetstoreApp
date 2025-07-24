using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Experiences;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Experiences;

public sealed class CvExperienceTests
{
    [Fact]
    public void CvExperience_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvExperience);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(CvExperience.Id) && p.PropertyType == typeof(CvExperienceId));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.CvId) && p.PropertyType == typeof(CvId));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.StartDate) && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.EndDate) && p.PropertyType == typeof(DateTime?));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.Company) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.IsConsultant) && p.PropertyType == typeof(bool));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.Role) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.Extent) && p.PropertyType == typeof(int));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.Tools) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.CompanyNeeds) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.Mission) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(CvExperience.Cv) && p.PropertyType == typeof(Cv));
    }
    
    [Fact]
    public void Create_WithoutId_Should_ReturnCvExperience()
    {
        // Arrange
        var cvId = CvId.Create(Guid.NewGuid());
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddYears(1);
        const string company = "Test Company";
        const bool isConsultant = false;
        const string role = "Software Engineer";
        const int extent = 100;
        const string tools = "C#, .NET";
        const string companyNeeds = "Improve software quality";
        const string mission = "Develop and maintain software applications";

        // Act
        var experience = CvExperience.Create(
            cvId, 
            startDate, 
            endDate, 
            company, 
            isConsultant, 
            role, 
            extent, 
            tools, 
            companyNeeds, 
            mission);

        // Assert
        experience.ShouldNotBeNull();
        experience.CvId.ShouldBe(cvId);
        experience.StartDate.ShouldBe(startDate);
        experience.EndDate.ShouldBe(endDate);
        experience.Company.ShouldBe(company);
        experience.IsConsultant.ShouldBe(isConsultant);
        experience.Role.ShouldBe(role);
        experience.Extent.ShouldBe(extent);
        experience.Tools.ShouldBe(tools);
        experience.CompanyNeeds.ShouldBe(companyNeeds);
        experience.Mission.ShouldBe(mission);
    }
    
    [Fact]
    public void Create_WithId_Should_ReturnCvExperience()
    {
        // Arrange
        var cvId = CvId.Create(Guid.NewGuid());
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddYears(1);
        const string company = "Test Company";
        const bool isConsultant = false;
        const string role = "Software Engineer";
        const int extent = 100;
        const string tools = "C#, .NET";
        const string companyNeeds = "Improve software quality";
        const string mission = "Develop and maintain software applications";
        var id = CvExperienceId.Create(Guid.NewGuid());

        // Act
        var experience = CvExperience.Create(
            id, 
            cvId, 
            startDate, 
            endDate, 
            company, 
            isConsultant, 
            role, 
            extent, 
            tools, 
            companyNeeds, 
            mission);

        // Assert
        experience.ShouldNotBeNull();
        experience.Id.ShouldBe(id);
        experience.CvId.ShouldBe(cvId);
        experience.StartDate.ShouldBe(startDate);
        experience.EndDate.ShouldBe(endDate);
        experience.Company.ShouldBe(company);
        experience.IsConsultant.ShouldBe(isConsultant);
        experience.Role.ShouldBe(role);
        experience.Extent.ShouldBe(extent);
        experience.Tools.ShouldBe(tools);
        experience.CompanyNeeds.ShouldBe(companyNeeds);
        experience.Mission.ShouldBe(mission);
    }
}