using Dotnetstore.Intranet.SDK.Models;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SDK.Tests.Models;

public class AppSettingsTests
{
    [Fact]
    public void AppSettings_Should_ContainCorrectProperties()
    {
        // Arrange
        var type = typeof(AppSettings);
        
        // Act
        var properties = type.GetProperties();
        
        // Assert
        properties.ShouldContain(p => p.Name == nameof(AppSettings.SecurityKey) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.JwtIssuer) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.JwtAudience) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.JwtExpirationInMinutes) && p.PropertyType == typeof(int));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.HttpClientBaseAddress) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.JwtTokenCookieName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.RefreshTokenCookieName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.LocalApiClientName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.SmtpServer) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.SmtpPort) && p.PropertyType == typeof(int));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.SmtpUsername) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.SmtpPassword) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(AppSettings.SmtpNoReplyAddress) && p.PropertyType == typeof(string));
        properties.Length.ShouldBe(13);
    }
}