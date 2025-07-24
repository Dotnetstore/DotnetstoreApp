using DotnetstoreApp.CV.Cvs;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Cvs;

public sealed class CvMappersTests
{
    [Fact]
    public void ToCvResponse_Should_MapCvToCvResponse()
    {
        // Arrange
        var cv = Cv.Create("CV 1", "English", "Doe", "John", new DateTime(1990, 1, 1), "A brief introduction");

        // Act
        var response = cv.ToCvResponse();

        // Assert
        cv.Id.Value.ShouldBe(response.Id);
        cv.Name.ShouldBe(response.Name);
        cv.Language.ShouldBe(response.Language);
    }
    
    [Fact]
    public void ToCvFullResponse_Should_MapCvToCvFullResponse()
    {
        // Arrange
        var cv = Cv.Create("CV 1", "English", "Doe", "John", new DateTime(1990, 1, 1), "A brief introduction");

        // Act
        var response = cv.ToCvFullResponse();

        // Assert
        cv.Id.Value.ShouldBe(response.Id);
        cv.Name.ShouldBe(response.Name);
        cv.Language.ShouldBe(response.Language);
        cv.LastName.ShouldBe(response.LastName);
        cv.FirstName.ShouldBe(response.FirstName);
        cv.DateOfBirth.ShouldBe(response.DateOfBirth);
        cv.Introduction.ShouldBe(response.Introduction);
    }
}