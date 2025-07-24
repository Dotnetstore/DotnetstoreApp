using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Educations;
using DotnetstoreApp.CV.Experiences;
using DotnetstoreApp.CV.Information;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Cvs;

public sealed class CvTests
{
    [Fact]
    public void Cv_Should_ImplementCorrectProperties()
    {
        // Arrange
        var type = typeof(Cv);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(p => p.Name == "Name" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "Language" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "LastName" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "FirstName" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "DateOfBirth" && p.PropertyType == typeof(DateTime));
        properties.ShouldContain(p => p.Name == "Introduction" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "Experiences" && p.PropertyType == typeof(ICollection<CvExperience>));
        properties.ShouldContain(p => p.Name == "Information" && p.PropertyType == typeof(ICollection<CvInformation>));
        properties.ShouldContain(p => p.Name == "Educations" && p.PropertyType == typeof(ICollection<CvEducation>));
    }
    
    [Fact]
    public void Create_Should_ReturnValidCv()
    {
        // Arrange
        const string name = "CV 1";
        const string language = "English";
        const string lastName = "Doe";
        const string firstName = "John";
        var dateOfBirth = new DateTime(1990, 1, 1);
        const string introduction = "This is a sample CV.";
        
        // Act
        var cv = Cv.Create(name, language, lastName, firstName, dateOfBirth, introduction);
        
        // Assert
        cv.ShouldNotBeNull();
        cv.Name.ShouldBe(name);
        cv.Language.ShouldBe(language);
        cv.LastName.ShouldBe(lastName);
        cv.FirstName.ShouldBe(firstName);
        cv.DateOfBirth.ShouldBe(dateOfBirth);
        cv.Introduction.ShouldBe(introduction);
    }
    
    [Fact]
    public void Create_WithId_Should_ReturnValidCv()
    {
        // Arrange
        var id = CvId.Create();
        const string name = "CV 1";
        const string language = "English";
        const string lastName = "Doe";
        const string firstName = "John";
        var dateOfBirth = new DateTime(1990, 1, 1);
        const string introduction = "This is a sample CV.";
        
        // Act
        var cv = Cv.Create(id, name, language, lastName, firstName, dateOfBirth, introduction);
        
        // Assert
        cv.ShouldNotBeNull();
        cv.Id.ShouldBe(id);
        cv.Name.ShouldBe(name);
        cv.Language.ShouldBe(language);
        cv.LastName.ShouldBe(lastName);
        cv.FirstName.ShouldBe(firstName);
        cv.DateOfBirth.ShouldBe(dateOfBirth);
        cv.Introduction.ShouldBe(introduction);
    }
}