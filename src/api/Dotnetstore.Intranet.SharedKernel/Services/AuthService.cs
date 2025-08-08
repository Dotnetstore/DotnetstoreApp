using BCrypt.Net;

namespace Dotnetstore.Intranet.SharedKernel.Services;

public sealed class AuthService : IAuthService
{
    string IAuthService.HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512, 7);
    }

    bool IAuthService.VerifyPassword(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword, HashType.SHA512);
    }
}