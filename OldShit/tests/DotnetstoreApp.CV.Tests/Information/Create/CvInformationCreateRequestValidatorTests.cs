using DotnetstoreApp.CV.Information.Create;
using DotnetstoreApp.SDK.Enums;
using DotnetstoreApp.SDK.Requests.CV;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Information.Create;

public sealed class CvInformationCreateRequestValidatorTests
{
    [Fact]
    public void Should_Validate_Valid_Request()
    {
        // Arrange
        var validator = new CvInformationCreateRequestValidator();
        var request = new CvInformationCreateRequest
        {
            CvId = Guid.NewGuid(),
            Name = "Test Information",
            CvInformationType = CvInformationType.Architecture
        };

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.ShouldBeTrue();
    }
}