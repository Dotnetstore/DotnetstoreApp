using Microsoft.AspNetCore.Identity;

namespace DotnetstoreApp.Organization.Users;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string LastName { get; init; } = string.Empty;
    
    public string FirstName { get; init; } = string.Empty;
    
    public string? MiddleName { get; init; }

    public DateTime DateOfBirth { get; init; }

    public bool IsMale { get; init; }
}