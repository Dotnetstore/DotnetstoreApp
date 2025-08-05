using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.Intranet.EmailService.Services;

internal sealed class SmtpService(ILogger<SmtpService> logger) : ISmtpService
{
    async ValueTask ISmtpService.SendEmailTextAsync(
        string senderName, 
        string senderEmail, 
        string fullName, 
        string emailAddress,
        string subject, 
        string messageBody,
        CancellationToken cancellationToken)
    {
        var from = new MailboxAddress(senderName, senderEmail);
        var to = new MailboxAddress(fullName, emailAddress);
        var bodyEncoding = System.Text.Encoding.UTF8;
        
        var message = new MimeMessage();
        message.From.Add(from);
        message.To.Add(to);
        message.Subject = subject;
        message.Body = new TextPart("plain", bodyEncoding) { Text = messageBody };
        
        using var client = new SmtpClient();
        await client.ConnectAsync("smtp.example.com", 587, MailKit.Security.SecureSocketOptions.StartTls, cancellationToken);
        await client.AuthenticateAsync("username", "password", cancellationToken);
        await client.SendAsync(message, cancellationToken);
        await client.DisconnectAsync(true, cancellationToken);
        
        logger.LogInformation("Email sent to {EmailAddress} with subject '{Subject}'", emailAddress, subject);
    }
}