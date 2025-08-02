namespace Dotnetstore.Intranet.SharedKernel.Models;

public abstract class PersonIdentity<TId>(TId id) : Person<TId>(id)
    where TId : notnull
{
    public string EmailAddress { get; protected init; } = null!;

    public string PasswordHash { get; protected init; } = null!;
    
    public string? RefreshToken { get; init; }
    
    public DateTime? RefreshTokenExpiryTime { get; init; }

    public bool EmailAddressIsConfirmed { get; init; }

    public string? EmailAddressConfirmationCode { get; init; }

    public bool AccountIsApproved { get; init; }
}