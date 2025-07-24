using DotnetstoreApp.SDK.Services;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Services;

public sealed class DataSchemeConstantsTests
{
    [Fact]
    public void DataSchemeConstants_ShouldHaveExpectedValues()
    {
        DataSchemeConstants.MaxCvNameLength.ShouldBe(100);
        DataSchemeConstants.MaxCvLanguageLength.ShouldBe(50);
        DataSchemeConstants.MaxCvLastNameLength.ShouldBe(25);
        DataSchemeConstants.MaxCvFirstNameLength.ShouldBe(25);
        DataSchemeConstants.MaxCvIntroductionLength.ShouldBe(4000);
        
        DataSchemeConstants.MaxCvInformationNameLength.ShouldBe(100);
        
        DataSchemeConstants.MinCvExperienceExtentValue.ShouldBe(1);
        DataSchemeConstants.MaxCvExperienceExtentValue.ShouldBe(100);
        DataSchemeConstants.MaxCvExperienceCompanyLength.ShouldBe(100);
        DataSchemeConstants.MaxCvExperienceRoleLength.ShouldBe(50);
        DataSchemeConstants.MaxCvExperienceToolsLength.ShouldBe(4000);
        
        DataSchemeConstants.MaxCvEducationSchoolNameLength.ShouldBe(100);
        DataSchemeConstants.MaxCvEducationLevelNameLength.ShouldBe(100);
    }
}