using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Information;
using DotnetstoreApp.SDK.Enums;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.CV.Tests.Information;

public sealed class CvInformationMappersTests
{
    [Fact]
    public void ToCvInformationResponse_ShouldMapPropertiesCorrectly()
    {
        // Arrange
        var information = CvInformation.Create(
            CvInformationId.Create(), 
            CvId.Create(),
            "Test Information",
            CvInformationType.Architecture);

        // Act
        var response = information.ToCvInformationResponse();

        // Assert
        response.Id.ShouldBe(information.Id.Value);
        response.CvId.ShouldBe(information.CvId.Value);
        response.Name.ShouldBe(information.Name);
        response.InformationType.ShouldBe(information.CvInformationType);
    }
}