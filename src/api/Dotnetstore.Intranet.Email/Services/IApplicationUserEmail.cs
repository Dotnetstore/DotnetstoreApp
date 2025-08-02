namespace Dotnetstore.Intranet.Email.Services;

public interface IApplicationUserEmail
{
    Task SendAccountConfirmationEmailAsync(
        string fullName,
        string emailAddress, 
        string confirmationCode,
        CancellationToken cancellationToken);
}