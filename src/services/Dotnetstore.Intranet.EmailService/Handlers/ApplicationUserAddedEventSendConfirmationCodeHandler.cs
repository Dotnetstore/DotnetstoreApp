using System.Text;
using Dotnetstore.Intranet.Contract.Events;
using Dotnetstore.Intranet.EmailService.Services;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.Intranet.EmailService.Handlers;

public sealed class ApplicationUserAddedEventSendConfirmationCodeHandler(
    ISmtpService smtpService,
    ILogger<ApplicationUserAddedEventSendConfirmationCodeHandler> logger) : IHandleMessages<ApplicationUserAddedEvent>
{
    public async Task Handle(ApplicationUserAddedEvent message, IMessageHandlerContext context)
    {
        try
        {
            logger.LogInformation($"Received an event in ApplicationUserAddedEventSendConfirmationCodeHandler with email address {message.EmailAddress}");
        
            var sb = new StringBuilder();
            sb.AppendLine($"Dear {message.EmailAddress}!");
            sb.AppendLine();
            sb.AppendLine("Thank you for registering with Dotnetstore Intranet. Please confirm your email address by clicking the link below:");
        
            sb.AppendLine($"<a href=https://localhost:7012/users/confirm?code={message.EmailAddressVerificationCode}\">Confirm Account</a>");
            sb.AppendLine();
            sb.AppendLine("If you did not register, please ignore this email.");
            sb.AppendLine();
            sb.AppendLine("Best regards,");
            sb.AppendLine("Dotnetstore Team");
        
            _ = smtpService.SendEmailTextAsync(
                "Dotnetstore Intranet",
                "noreply@dotnetstore.se",
                message.FullName,
                message.EmailAddress,
                "Account Confirmation",
                sb.ToString(),
                context.CancellationToken);
        
            logger.LogInformation("Account confirmation email sent to {EmailAddress} for user {FullName}.", message.EmailAddress, message.FullName);
            await Task.CompletedTask;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error sending account confirmation email to {EmailAddress} for user {FullName}.", message.EmailAddress, message.FullName);
        }
    }
}