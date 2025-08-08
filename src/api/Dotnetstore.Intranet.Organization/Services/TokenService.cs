using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Dotnetstore.Intranet.Organization.Users;
using Dotnetstore.Intranet.SDK.Models;
using Microsoft.IdentityModel.Tokens;

namespace Dotnetstore.Intranet.Organization.Services;

internal sealed class TokenService(
    AppSettings appSettings,
    IUnitOfWork unitOfWork,
    ILogger<TokenService> logger) : ITokenService
{
    async ValueTask<(string Token, string RefreshToken)> ITokenService.UpdateRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByRefreshTokenAsync(refreshToken, cancellationToken);
        if (user is null)
        {
            logger.LogWarning("Attempted to update refresh token for a user that does not exist: {RefreshToken}", refreshToken);
            return (string.Empty, string.Empty);
        }

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(2);
        
        unitOfWork.Users.Update(user);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (result < 1)
        {
            logger.LogError("Failed to update refresh token for user with ID {UserId}", user.Id);
            return (string.Empty, string.Empty);
        }

        var token = GenerateToken(user);

        logger.LogInformation("Successfully updated refresh token for user with ID {UserId}", user.Id);
        return (token, refreshToken);
    }

    async ValueTask ITokenService.UpdateRefreshTokenAsync(
        ApplicationUser user, 
        CancellationToken cancellationToken)
    {
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(2);
        
        unitOfWork.Users.Update(user);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (result < 1)
        {
            logger.LogError("Failed to update refresh token for user with ID {UserId}", user.Id);
        }
        else
        {
            logger.LogInformation("Successfully updated refresh token for user with ID {UserId}", user.Id);
        }
    }

    string ITokenService.GenerateToken(ApplicationUser user)
    {
        return GenerateToken(user);
    }

    string ITokenService.GenerateRefreshToken()
    {
        return GenerateRefreshToken();
    }
    
    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }
    
    private string GenerateToken(ApplicationUser user)
    {
        var claims = GetClaims(user);
        var tokenDescriptor = CreateSecurityTokenDescriptor(claims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor CreateSecurityTokenDescriptor(List<Claim> claims)
    {
        var hmac = new HMACSHA512(Convert.FromBase64String(appSettings.SecurityKey));
        var key = hmac.Key;
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
        
        return new SecurityTokenDescriptor
        {
            Issuer = appSettings.JwtIssuer,
            Audience = appSettings.JwtAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(appSettings.JwtExpirationInMinutes),
            SigningCredentials = signingCredentials
        };
    }

    private static List<Claim> GetClaims(ApplicationUser user)
    {
        var list = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Surname, user.LastName),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Email, user.EmailAddress),
            new(ClaimTypes.Name, user.EmailAddress),
            new(ClaimTypes.Gender, user.IsMale ? "Male" : "Female"),
            new("DateOfBirth", user.DateOfBirth.ToString(CultureInfo.InvariantCulture))
        };

        var roles = user.ApplicationUserInRoles.Select(x => x.Role);
        list.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return list;
    }
}