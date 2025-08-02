namespace Dotnetstore.Intranet.Web.Pages.Users.Models;

public class RegisterModel
{
    public string LastName { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string? MiddleName { get; init; }
    public DateTime? DateOfBirth { get; init; } = new(2000, 1, 1);
    public bool IsMale { get; init; }
    public string? SocialSecurityNumber { get; init; }
    public string EmailAddress { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string ConfirmPassword { get; init; } = null!;
}