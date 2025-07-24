using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dotnetstore.Intranet.SharedKernel.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Dotnetstore.Intranet.SharedKernel.Services;

public sealed class AuthService(IOptions<AppSettings> appSettings) : IAuthService
{
    string IAuthService.CreateToken(
        Guid userId, 
        string lastName, 
        string firstName, 
        string emailAddress, 
        bool isMale,
        DateTime dateOfBirth)
    {
        var claims = GetClaims(userId, lastName, firstName, emailAddress, isMale, dateOfBirth);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Value.SecurityKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var tokenDescriptor = SecurityTokenDescriptor(claims, signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    private SecurityTokenDescriptor SecurityTokenDescriptor(List<Claim> claims, SigningCredentials creds)
    {
        return new SecurityTokenDescriptor
        {
            Issuer = appSettings.Value.JwtIssuer,
            Audience = appSettings.Value.JwtAudience,
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(appSettings.Value.JwtExpirationInMinutes),
            SigningCredentials = creds
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
        new(ClaimTypes.Gender, isMale ? "Male" : "Female"),
        new("DateOfBirth", dateOfBirth.ToString(CultureInfo.InvariantCulture))
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