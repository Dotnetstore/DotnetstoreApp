using DotnetstoreApp.CV.Cvs;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Cvs;

public sealed class CvBuilderTests
{
    [Fact]
    public void Create_WithoutId_Should_ReturnValidCvWithGeneratedId()
    {
        // Arrange
        const string name = "CV 1";
        const string language = "English";
        const string lastName = "Doe";
        const string firstName = "John";
        var dateOfBirth = new DateTime(1990, 1, 1);
        const string introduction = "This is a test CV.";
        
        // Act
        var cv = CvBuilder.Create()
            .WithId()
            .WithName(name)
            .WithLanguage(language)
            .WithLastName(lastName)
            .WithFirstName(firstName)
            .WithDateOfBirth(dateOfBirth)
            .WithIntroduction(introduction)
            .Build();
        
        // Assert
        cv.Id.ShouldNotBeNull();
        cv.Name.ShouldBe(name);
        cv.Language.ShouldBe(language);
        cv.LastName.ShouldBe(lastName);
        cv.FirstName.ShouldBe(firstName);
        cv.DateOfBirth.ShouldBe(dateOfBirth);
        cv.Introduction.ShouldBe(introduction);
    }
    
    [Fact]
    public void Create_WithId_Should_ReturnValidCvWithSpecifiedId()
    {
        // Arrange
        var specifiedId = Guid.CreateVersion7();
        const string name = "CV 2";
        const string language = "French";
        const string lastName = "Smith";
        const string firstName = "Jane";
        var dateOfBirth = new DateTime(1995, 5, 5);
        const string introduction = "This is another test CV.";
        
        // Act
        var cv = CvBuilder.Create()
            .WithId(specifiedId)
            .WithName(name)
            .WithLanguage(language)
            .WithLastName(lastName)
            .WithFirstName(firstName)
            .WithDateOfBirth(dateOfBirth)
            .WithIntroduction(introduction)
            .Build();
        
        // Assert
        cv.Id.Value.ShouldBe(specifiedId);
        cv.Name.ShouldBe(name);
        cv.Language.ShouldBe(language);
        cv.LastName.ShouldBe(lastName);
        cv.FirstName.ShouldBe(firstName);
        cv.DateOfBirth.ShouldBe(dateOfBirth);
        cv.Introduction.ShouldBe(introduction);
    }
}