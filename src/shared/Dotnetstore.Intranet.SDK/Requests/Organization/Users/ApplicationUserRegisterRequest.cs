namespace Dotnetstore.Intranet.SDK.Requests.Organization.Users;

public record struct ApplicationUserRegisterRequest(
    string LastName,
    string FirstName,
    string? MiddleName,
    DateTime DateOfBirth,
    bool IsMale,
    string? SocialSecurityNumber,
    string EmailAddress,
    string Password,
    string ConfirmPassword);