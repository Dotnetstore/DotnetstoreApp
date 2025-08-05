namespace Dotnetstore.Intranet.EmailService.Services;

public interface ISmtpService
{
    ValueTask SendEmailTextAsync(
        string senderName, 
        string senderEmail,
        string fullName,
        string emailAddress, 
        string subject, 
        string messageBody,
        CancellationToken cancellationToken = default);
}