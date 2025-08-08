namespace Dotnetstore.Intranet.Organization.Users;

internal sealed class ApplicationUserFactory(
    IAuthService authService,
    TimeProvider timeProvider)
{
    internal ApplicationUser Create(
        string emailConfirmationCode,
        ApplicationUserRegisterRequest request)
    {
        var passwordHash = authService.HashPassword(request.Password);

        return ApplicationUserBuilder.Create()
            .WithId()
            .WithLastName(request.LastName)
            .WithFirstName(request.FirstName)
            .WithMiddleName(request.MiddleName)
            .WithDateOfBirth(request.DateOfBirth)
            .WithIsMale(request.IsMale)
            .WithSocialSecurityNumber(request.SocialSecurityNumber)
            .WithEmailAddress(request.EmailAddress)
            .WithPasswordHash(passwordHash)
            .WithEmailAddressIsConfirmed()
            .WithEmailAddressConfirmationCode(emailConfirmationCode)
            .WithAccountIsApproved()
            .WithCreatedDate(timeProvider.GetUtcNow().DateTime)
            .Build();
    }
}