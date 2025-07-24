using DotnetstoreApp.CV.Cvs.Create;
using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Cvs.Create;

public sealed class CvCreateRequestValidatorTests
{
    [Fact]
    public void Validate_ValidRequest_ReturnsSuccess()
    {
        // Arrange
        var validator = new CvCreateRequestValidator();
        var request = new CvCreateRequest
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