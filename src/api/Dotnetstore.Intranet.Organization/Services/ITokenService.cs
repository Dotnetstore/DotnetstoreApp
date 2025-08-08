using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Services;

internal interface ITokenService
{
    ValueTask<(string Token, string RefreshToken)> UpdateRefreshTokenAsync(
        string refreshToken,
        CancellationToken cancellationToken);
    
    ValueTask UpdateRefreshTokenAsync(
        ApplicationUser user,
        CancellationToken cancellationToken);
    string GenerateToken(ApplicationUser user);
    
    string GenerateRefreshToken();
}