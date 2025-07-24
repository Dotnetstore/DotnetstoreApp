using DotnetstoreApp.CV.Experiences.Create;
using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Experiences.Create;

public sealed class CvExperienceCreateRequestValidatorTests
{
    [Fact]
    public void CvExperienceCreateRequestValidator_Should_Validate_Valid_Request()
    {
        // Arrange
        var request = new CvExperienceCreateRequest(
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

        var validator = new CvExperienceCreateRequestValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue();
    }
}