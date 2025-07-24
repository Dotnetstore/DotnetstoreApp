namespace Dotnetstore.Intranet.SharedKernel.Models;

public abstract class Person<TId>(TId id) : AggregateRoot<TId>(id)
    where TId : notnull
{
    public string LastName { get; init; } = null!;
    
    public string FirstName { get; init; } = null!;
    
    public string? MiddleName { get; init; }

    public DateTime DateOfBirth { get; init; }

    public bool IsMale { get; init; }

    public string? SocialSecurityNumber { get; init; }
}