using DotnetstoreApp.CV.Information;
using DotnetstoreApp.SDK.Enums;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Information;

public sealed class CvInformationBuilderTests
{
    [Fact]
    public void CvInformationBuilder_WithoutId_Should_ReturnValidCvInformation()
    {
        // Arrange
        var cvId = Guid.NewGuid();
        const string name = "Test Information";
        const CvInformationType informationType = CvInformationType.Architecture;

        // Act
        var cvInformation = CvInformationBuilder.Create()
            .WithId()
            .WithCvId(cvId)
            .WithName(name)
            .WithInformationType(informationType)
            .Build();

        // Assert
        cvInformation.Id.ShouldNotBeNull();
        cvInformation.CvId.Value.ShouldBe(cvId);
        cvInformation.Name.ShouldBe(name);
        cvInformation.CvInformationType.ShouldBe(informationType);
    }
    
    [Fact]
    public void CvInformationBuilder_WithId_Should_ReturnValidCvInformation()
    {
        // Arrange
        var id = Guid.NewGuid();
        var cvId = Guid.NewGuid();
        const string name = "Test Information";
        const CvInformationType informationType = CvInformationType.Architecture;

        // Act
        var cvInformation = CvInformationBuilder.Create()
            .WithId(id)
            .WithCvId(cvId)
            .WithName(name)
            .WithInformationType(informationType)
            .Build();

        // Assert
        cvInformation.Id.Value.ShouldBe(id);
        cvInformation.CvId.Value.ShouldBe(cvId);
        cvInformation.Name.ShouldBe(name);
        cvInformation.CvInformationType.ShouldBe(informationType);
    }
}