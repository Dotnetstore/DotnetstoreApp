namespace Dotnetstore.Intranet.SharedKernel.Models;

public abstract class PersonIdentity<TId>(TId id) : Person<TId>(id)
    where TId : notnull
{
    public string EmailAddress { get; protected init; } = null!;

    public string PasswordHash { get; protected init; } = null!;
    
    public string? RefreshToken { get; set; }
    
    public DateTime? RefreshTokenExpiryTime { get; set; }
}