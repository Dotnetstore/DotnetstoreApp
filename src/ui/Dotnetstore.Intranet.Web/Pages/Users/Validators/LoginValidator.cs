using Dotnetstore.Intranet.SDK.Services;
using Dotnetstore.Intranet.Web.Pages.Users.Models;
using FluentValidation;

namespace Dotnetstore.Intranet.Web.Pages.Users.Validators;

internal sealed class LoginValidator : AbstractValidator<LoginModel>
{
    public LoginValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().NotNull().WithMessage("Username is required.")
            .EmailAddress().WithMessage("Username must be a valid email address.")
            .MaximumLength(DataSchemeConstants.UserEmailMaxLength).WithMessage($"Username cannot exceed {DataSchemeConstants.UserEmailMaxLength} characters.");
        
        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password is required.")
            .MaximumLength(DataSchemeConstants.UserPasswordMaxLength).WithMessage($"Password cannot exceed {DataSchemeConstants.UserPasswordMaxLength} characters.");
    }
}