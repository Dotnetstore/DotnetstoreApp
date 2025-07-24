using DotnetstoreApp.CV.Cvs.Update;
using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Cvs.Update;

public sealed class CvUpdateRequestValidatorTests
{
    [Fact]
    public void CvUpdateRequestValidator_Should_Validate_Correctly()
    {
        // Arrange
        var validator = new CvUpdateRequestValidator();
        var request = new CvUpdateRequest
        {
            Name = "Sample CV",
            Language = "English",
            LastName = "Doe",
            FirstName = "John",
            DateOfBirth = DateTime.UtcNow.AddYears(-30),
            Introduction = "This is a sample introduction."
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue();
    }
}