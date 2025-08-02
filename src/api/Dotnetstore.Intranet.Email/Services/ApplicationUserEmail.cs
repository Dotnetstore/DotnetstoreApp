using System.Text;
using Dotnetstore.Intranet.SDK.Models;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace Dotnetstore.Intranet.Email.Services;

internal sealed class ApplicationUserEmail(
    AppSettings appSettings,
    ILogger<ApplicationUserEmail> logger) : IApplicationUserEmail
{
    async Task IApplicationUserEmail.SendAccountConfirmationEmailAsync(
        string fullName,
        string emailAddress, 
        string confirmationCode,
        CancellationToken cancellationToken)
    {
        var message = new MimeMessage();
        var from = new MailboxAddress("Dotnetstore", appSettings.SmtpNoReplyAddress);
        message.From.Add(from);

        var sb = new StringBuilder();
        sb.AppendLine($"Dear {fullName}!");
        sb.AppendLine();
        sb.AppendLine("Thank you for registering with Dotnetstore Intranet. Please confirm your email address by clicking the link below:");
        sb.AppendLine();
        sb.AppendLine($"<a href=\"{appSettings.HttpClientBaseAddress}/confirm?code={confirmationCode}\">Confirm Account</a>");
        sb.AppendLine();
        sb.AppendLine("If you did not register, please ignore this email.");
        sb.AppendLine();
        sb.AppendLine("Best regards,");
        sb.AppendLine("Dotnetstore Team");
        
        var to = new MailboxAddress(fullName, emailAddress);
        message.To.Add(to);
        message.Subject = "Account Confirmation";
        message.Body = new TextPart("html") { Text = sb.ToString() };
        
        using var client = new SmtpClient();
        await client.ConnectAsync(appSettings.SmtpServer, appSettings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
        await client.AuthenticateAsync(appSettings.SmtpUsername, appSettings.SmtpPassword, cancellationToken);
        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
        
        logger.LogInformation("Account confirmation email sent to {EmailAddress} for user {FullName}.", emailAddress, fullName);
    }
}