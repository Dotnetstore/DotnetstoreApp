using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Services;

public interface IEventService
{
    ValueTask ApplicationUserAddedEventAsync(
        string emailAddressVerificationCode,
        ApplicationUserId userId,
        string firstName, 
        string lastName, 
        string emailAddress,
        CancellationToken cancellationToken);
}