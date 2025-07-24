using Dotnetstore.Intranet.Organization.Users;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Users;

public class ApplicationUserMappersTests
{
    [Fact]
    public void ToApplicationUserTokenResponse_ShouldReturnCorrectResponse()
    {
        // Arrange
        const string token = "sampleToken";
        const string refreshToken = "sampleRefreshToken";
        var userId = Guid.NewGuid();

        // Act
        var response = token.ToApplicationUserTokenResponse(refreshToken, userId);

        // Assert
        response.Token.ShouldBe(token);
        response.RefreshToken.ShouldBe(refreshToken);
        response.UserId.ShouldBe(userId);
    }

    [Fact]
    public void ToApplicationUserResponse_ShouldReturnCorrectResponse()
    {
        // Arrange
        var user = ApplicationUser.Create(
            ApplicationUserId.Create(Guid.NewGuid()),
            "Doe",
            "John",
            "A.",
            new DateTime(1990, 1, 1),
            true,
            "123-45-6789",
            "test@test.com",
            "password123",
            DateTime.Now);

        // Act
        var response = user.ToApplicationUserResponse();

        // Assert
        response.Id.ShouldBe(user.Id.Value);
        response.LastName.ShouldBe(user.LastName);
        response.FirstName.ShouldBe(user.FirstName);
        response.MiddleName.ShouldBe(user.MiddleName);
        response.DateOfBirth.ShouldBe(user.DateOfBirth);
        response.IsMale.ShouldBe(user.IsMale);
        response.SocialSecurityNumber.ShouldBe(user.SocialSecurityNumber);
        response.Email.ShouldBe(user.EmailAddress);
    }
}