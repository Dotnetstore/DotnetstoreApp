using Dotnetstore.Intranet.SDK.Models;
using Dotnetstore.Intranet.SharedKernel.Services;
using Microsoft.Extensions.Options;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SharedKernel.Tests.Services;

public class AuthServiceTests
{
    private readonly IAuthService _authService;
    
    public AuthServiceTests()
    {
        var appSettings = new AppSettings
        {
            HttpClientBaseAddress = "https://api.example.com",
            JwtAudience = "example-audience",
            JwtIssuer = "example-issuer",
            JwtExpirationInMinutes = 60,
            JwtTokenCookieName = "JwtToken",
            LocalApiClientName = "LocalApi",
            RefreshTokenCookieName = "RefreshToken",
            SecurityKey = "VGhpc0lzQVNlY3JldEtleUZvckp3dEF1dGhlbnRpY2F0aW9uSXRTbG91bGRCZUxvbmdFbm91Z2hBbmRJTG92ZU15RGF1Z2h0ZXJGcmVqYVNqb2Rpbg==",
            SmtpNoReplyAddress = "noreply@test.com",
            SmtpPassword = "password123",
            SmtpPort = 587,
            SmtpServer = "smtp.test.com",
            SmtpUsername = "username"
        };
        
        var appSettingsOptions = Options.Create(appSettings);
        _authService = new AuthService(appSettingsOptions);
    }
    
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

    [Fact]
    public void CreateToken_ShouldReturnValidJwtToken_WhenCalled()
    {
        // Arrange
        var userId = Guid.NewGuid();
        const string lastName = "Doe";
        const string firstName = "John";
        const string emailAddress = "test@test.com";
        const bool isMale = true;
        var dateOfBirth = new DateTime(1990, 1, 1);

        // Act
        var token = _authService.CreateToken(userId, lastName, firstName, emailAddress, isMale, dateOfBirth);

        // Assert
        token.ShouldNotBeNull();
        token.ShouldNotBeEmpty();
        token.ShouldContain("eyJ");
        token.ShouldContain(".");
    }

    [Fact]
    public void GenerateRefreshToken_ShouldReturnValidRefreshToken_WhenCalled()
    {
        // Act
        var refreshToken = _authService.GenerateRefreshToken();
        
        // Assert
        refreshToken.ShouldNotBeNull();
        refreshToken.ShouldNotBeEmpty();
        refreshToken.ShouldNotBe("null");
        refreshToken.ShouldNotBe("undefined");
        refreshToken.ShouldNotBe("NaN");
    }
}