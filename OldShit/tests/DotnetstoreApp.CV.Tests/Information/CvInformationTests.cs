using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Information;
using DotnetstoreApp.SDK.Enums;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Information;

public sealed class CvInformationTests
{
    [Fact]
    public void CvInformation_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(CvInformation);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == "Id" && p.PropertyType == typeof(CvInformationId));
        properties.ShouldContain(p => p.Name == "CvId" && p.PropertyType == typeof(CvId));
        properties.ShouldContain(p => p.Name == "Name" && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == "CvInformationType" && p.PropertyType == typeof(CvInformationType));
    }
    
    [Fact]
    public void Create_WithoutId_Should_ReturnNewCvInformation()
    {
        // Arrange
        var cvId = CvId.Create(Guid.NewGuid());
        const string name = "Test CV Information";
        const CvInformationType type = CvInformationType.Architecture;

        // Act
        var cvInformation = CvInformation.Create(cvId, name, type);

        // Assert
        cvInformation.ShouldNotBeNull();
        cvInformation.CvId.ShouldBe(cvId);
        cvInformation.Name.ShouldBe(name);
        cvInformation.CvInformationType.ShouldBe(type);
    }
    
    [Fact]
    public void Create_WithId_Should_ReturnCvInformationWithGivenId()
    {
        // Arrange
        var id = CvInformationId.Create(Guid.NewGuid());
        var cvId = CvId.Create(Guid.NewGuid());
        const string name = "Test CV Information";
        const CvInformationType type = CvInformationType.Architecture;

        // Act
        var cvInformation = CvInformation.Create(id, cvId, name, type);

        // Assert
        cvInformation.ShouldNotBeNull();
        cvInformation.Id.ShouldBe(id);
        cvInformation.CvId.ShouldBe(cvId);
        cvInformation.Name.ShouldBe(name);
        cvInformation.CvInformationType.ShouldBe(type);
    }
}