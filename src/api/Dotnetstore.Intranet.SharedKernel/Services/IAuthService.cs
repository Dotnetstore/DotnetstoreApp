namespace Dotnetstore.Intranet.SharedKernel.Services;

public interface IAuthService
{
    string CreateToken(
        Guid userId,
        string lastName,
        string firstName,
        string emailAddress,
        bool isMale,
        DateTime dateOfBirth);

    string GenerateRefreshToken();
}