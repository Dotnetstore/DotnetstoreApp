namespace Dotnetstore.Intranet.SharedKernel.Services;

public interface IAuthService
{
    string HashPassword(string password);
    
    bool VerifyPassword(string password, string hashedPassword);
    
    string CreateToken(
        Guid userId,
        string lastName,
        string firstName,
        string emailAddress,
        bool isMale,
        DateTime dateOfBirth);

    string GenerateRefreshToken();
}