namespace Dotnetstore.Intranet.Organization.Users.ConfirmEmail;

internal sealed class ApplicationUserConfirmEmailRequestValidation : Validator<ApplicationUserConfirmEmailRequest>
{
    public ApplicationUserConfirmEmailRequestValidation()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Confirmation code is required.");
    }
}