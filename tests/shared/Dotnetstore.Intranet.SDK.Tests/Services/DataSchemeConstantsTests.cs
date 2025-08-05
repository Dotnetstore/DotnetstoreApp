using Dotnetstore.Intranet.SDK.Services;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SDK.Tests.Services;

public class DataSchemeConstantsTests
{
    [Fact]
    public void UserLastNameMaxLength_Should_Be25()
    {
        DataSchemeConstants.UserLastNameMaxLength.ShouldBe(25);
    }

    [Fact]
    public void UserFirstNameMaxLength_Should_Be25()
    {
        DataSchemeConstants.UserFirstNameMaxLength.ShouldBe(25);
    }

    [Fact]
    public void UserMiddleNameMaxLength_Should_Be25()
    {
        DataSchemeConstants.UserMiddleNameMaxLength.ShouldBe(25);
    }

    [Fact]
    public void UserSocialSecurityNumberMaxLength_Should_Be13()
    {
        DataSchemeConstants.UserSocialSecurityNumberMaxLength.ShouldBe(13);
    }

    [Fact]
    public void UserEmailMaxLength_Should_Be255()
    {
        DataSchemeConstants.UserEmailMaxLength.ShouldBe(255);
    }

    [Fact]
    public void UserEmailConfirmationCodeMaxLength_Should_Be50()
    {
        DataSchemeConstants.UserEmailConfirmationCodeMaxLength.ShouldBe(50);
    }

    [Fact]
    public void UserPasswordMaxLength_Should_Be100()
    {
        DataSchemeConstants.UserPasswordMaxLength.ShouldBe(100);
    }

    [Fact]
    public void UserDateOfBirthMin_Should_BeMinus70()
    {
        DataSchemeConstants.UserDateOfBirthMin.ShouldBe(-70);
    }

    [Fact]
    public void UserDateOfBirthMax_Should_BeMinus15()
    {
        DataSchemeConstants.UserDateOfBirthMax.ShouldBe(-15);
    }

    [Fact]
    public void UserRoleNameMaxLength_Should_Be50()
    {
        DataSchemeConstants.UserRoleNameMaxLength.ShouldBe(50);
    }

    [Fact]
    public void UserRoleNameDescriptionLength_Should_Be200()
    {
        DataSchemeConstants.UserRoleDescriptionLength.ShouldBe(200);
    }
}