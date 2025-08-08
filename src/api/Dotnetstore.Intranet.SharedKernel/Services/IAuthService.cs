namespace Dotnetstore.Intranet.SharedKernel.Services;

public interface IAuthService
{
    string HashPassword(string password);
    
    bool VerifyPassword(string password, string hashedPassword);
}