using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using BCrypt.Net;
using Dotnetstore.Intranet.SDK.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Dotnetstore.Intranet.SharedKernel.Services;

public sealed class AuthService(IOptions<AppSettings> appSettings) : IAuthService
{
    string IAuthService.HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512, 7);
    }

    bool IAuthService.VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword, HashType.SHA512);
    }

    string IAuthService.CreateToken(
        Guid userId, 
        string lastName, 
        string firstName, 
        string emailAddress, 
        bool isMale,
        DateTime dateOfBirth)
    {
        var claims = GetClaims(userId, lastName, firstName, emailAddress, isMale, dateOfBirth);
        var tokenDescriptor = CreateSecurityTokenDescriptor(claims);
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private SecurityTokenDescriptor CreateSecurityTokenDescriptor(List<Claim> claims)
    {
        var hmac = new HMACSHA512(Convert.FromBase64String(appSettings.Value.SecurityKey));
        var key = hmac.Key;
        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
        
        return new SecurityTokenDescriptor
        {
            Issuer = appSettings.Value.JwtIssuer,
            Audience = appSettings.Value.JwtAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(appSettings.Value.JwtExpirationInMinutes),
            SigningCredentials = signingCredentials
        };
    }

    private static List<Claim> GetClaims(
        Guid userId,
        string lastName,
        string firstName,
        string emailAddress,
        bool isMale,
        DateTime dateOfBirth) =>
    [
        new(ClaimTypes.NameIdentifier, userId.ToString()),
        new(ClaimTypes.Surname, lastName),
        new(ClaimTypes.GivenName, firstName),
        new(ClaimTypes.Email, emailAddress),
        new(ClaimTypes.Name, emailAddress),
        new(ClaimTypes.Gender, isMale ? "Male" : "Female"),
        new("DateOfBirth", dateOfBirth.ToString(CultureInfo.InvariantCulture)),
        new(ClaimTypes.Role, "Administrator")
    ];
    
    string IAuthService.GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }
}