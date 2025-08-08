using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Services;

internal sealed class EventService(IMessageSession messageSession) : IEventService
{
    ValueTask IEventService.ApplicationUserAddedEventAsync(
        string emailAddressVerificationCode, 
        ApplicationUserId userId,
        string firstName, 
        string lastName, 
        string emailAddress, 
        CancellationToken cancellationToken)
    {
        var newEvent = new ApplicationUserAddedEvent
        {
            EmailAddressVerificationCode = emailAddressVerificationCode,
            UserId = userId.Value,
            EmailAddress = emailAddress,
            FullName = $"{lastName} {firstName}"
        };
        
        _ = messageSession.Publish(newEvent, cancellationToken: cancellationToken);
        return ValueTask.CompletedTask;
    }
}