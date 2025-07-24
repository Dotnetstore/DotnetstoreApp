using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using FastEndpoints;
using FluentValidation;

namespace Dotnetstore.Intranet.Organization.Users.Login;

internal sealed class ApplicationUserLoginRequestValidation : Validator<ApplicationUserLoginRequest>
{
    public ApplicationUserLoginRequestValidation()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("User name is required.")
            .EmailAddress().WithMessage("Wrong user name or password.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Wrong user name or password.");
    }
}