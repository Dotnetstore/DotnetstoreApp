using DotnetstoreApp.SDK.Enums;
using Shouldly;
using Xunit;

namespace DotnetstoreApp.SDK.Tests.Enums;

public sealed class CvInformationTypeTests
{
    [Theory]
    [InlineData("Architecture", CvInformationType.Architecture)]
    [InlineData("Cloud", CvInformationType.Cloud)]
    [InlineData("DesiredRole", CvInformationType.DesiredRole)]
    [InlineData("Devops", CvInformationType.Devops)]
    [InlineData("Language", CvInformationType.Language)]
    [InlineData("Leadership", CvInformationType.Leadership)]
    [InlineData("Programming", CvInformationType.Programming)]
    public void Parse_ShouldReturnCorrectEnum(string value, CvInformationType expected)
    {
        var result = Enum.Parse<CvInformationType>(value);
        result.ShouldBe(expected);
    }
}