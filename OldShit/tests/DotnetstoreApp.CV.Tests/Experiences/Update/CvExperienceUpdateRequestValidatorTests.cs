using DotnetstoreApp.CV.Experiences.Update;
using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Experiences.Update;

public sealed class CvExperienceUpdateRequestValidatorTests
{
    [Fact]
    public void CvExperienceUpdateRequestValidator_Should_Validate_Valid_Request()
    {
        // Arrange
        var request = new CvExperienceUpdateRequest(
            Guid.CreateVersion7(),
            DateTime.Now.AddYears(-5),
            DateTime.Now.AddYears(-3),
            "Tech Company",
            true,
            "Software Engineer",
            100,
            "C#, .NET, SQL",
            "Developed software solutions",
            "Improved system performance");

        var validator = new CvExperienceUpdateRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue();
    }
}