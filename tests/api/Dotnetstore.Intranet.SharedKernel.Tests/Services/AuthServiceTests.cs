using Dotnetstore.Intranet.SharedKernel.Services;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SharedKernel.Tests.Services;

public class AuthServiceTests
{
    private readonly IAuthService _authService = new AuthService();

    [Fact]
    public void HashPassword_ShouldReturnHashedPassword_WhenCalled()
    {
        // Arrange
        const string password = "TestPassword123";

        // Act
        var hashedPassword = _authService.HashPassword(password);

        // Assert
        hashedPassword.ShouldNotBeNull();
        hashedPassword.ShouldNotBe(password);
    }

    [Fact]
    public void VerifyPassword_ShouldReturnTrue_WhenPasswordMatchesHashedPassword()
    {
        // Arrange
        const string password = "TestPassword123";
        var hashedPassword = _authService.HashPassword(password);

        // Act
        var isVerified = _authService.VerifyPassword(password, hashedPassword);

        // Assert
        isVerified.ShouldBeTrue();
    }

    [Fact]
    public void VerifyPassword_ShouldReturnFalse_WhenPasswordDoesNotMatchHashedPassword()
    {
        // Arrange
        const string password = "TestPassword123";
        const string wrongPassword = "WrongPassword456";
        var hashedPassword = _authService.HashPassword(password);

        // Act
        var isVerified = _authService.VerifyPassword(wrongPassword, hashedPassword);
        
        // Assert
        isVerified.ShouldBeFalse();
    }
}