using NServiceBus;

namespace Dotnetstore.Intranet.Contract.Events;

public class ApplicationUserAddedEvent : IEvent
{
    public string EmailAddressVerificationCode { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}