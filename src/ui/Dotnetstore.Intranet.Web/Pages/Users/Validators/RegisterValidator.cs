using Dotnetstore.Intranet.SDK.Services;
using Dotnetstore.Intranet.Web.Pages.Users.Models;
using FluentValidation;

namespace Dotnetstore.Intranet.Web.Pages.Users.Validators;

internal sealed class RegisterValidator : AbstractValidator<RegisterModel>
{
    public RegisterValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty().NotNull().WithMessage("Last name is required.")
            .MaximumLength(DataSchemeConstants.UserLastNameMaxLength).WithMessage($"Last name cannot exceed {DataSchemeConstants.UserLastNameMaxLength} characters.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().NotNull().WithMessage("First name is required.")
            .MaximumLength(DataSchemeConstants.UserFirstNameMaxLength).WithMessage($"First name cannot exceed {DataSchemeConstants.UserFirstNameMaxLength} characters.");
        
        RuleFor(x => x.MiddleName)
            .MaximumLength(DataSchemeConstants.UserMiddleNameMaxLength).WithMessage($"Middle name cannot exceed {DataSchemeConstants.UserMiddleNameMaxLength} characters.")
            .When(x => !string.IsNullOrEmpty(x.MiddleName));
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().NotNull().WithMessage("Date of birth is required.")
            .LessThan(DateTime.Now.AddYears(DataSchemeConstants.UserDateOfBirthMax)).WithMessage("User must be at least 15 years old.")
            .GreaterThanOrEqualTo(DateTime.Now.AddYears(DataSchemeConstants.UserDateOfBirthMin)).WithMessage("User must be at most 70 years old.");
        
        RuleFor(x => x.SocialSecurityNumber)
            .MaximumLength(DataSchemeConstants.UserSocialSecurityNumberMaxLength).WithMessage($"Social Security Number cannot exceed {DataSchemeConstants.UserSocialSecurityNumberMaxLength} characters.")
            .Must(x => Personnummer.Personnummer.Valid(x)).When(x => !string.IsNullOrEmpty(x.SocialSecurityNumber)).WithMessage("Social Security Number must be a valid format.");
        
        RuleFor(x => x.EmailAddress)
            .NotEmpty().NotNull().WithMessage("Email address is required.")
            .EmailAddress().WithMessage("Email address must be a valid email format.")
            .MaximumLength(DataSchemeConstants.UserEmailMaxLength).WithMessage($"Email address cannot exceed {DataSchemeConstants.UserEmailMaxLength} characters.");
        
        RuleFor(x => x.Password)
            .NotEmpty().NotNull().WithMessage("Password is required.")
            .MaximumLength(DataSchemeConstants.UserPasswordMaxLength).WithMessage($"Password cannot exceed {DataSchemeConstants.UserPasswordMaxLength} characters.");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().NotNull().WithMessage("Confirm password is required.")
            .Equal(x => x.Password).WithMessage("Confirm password must match the password.")
            .MaximumLength(DataSchemeConstants.UserPasswordMaxLength).WithMessage($"Confirm password cannot exceed {DataSchemeConstants.UserPasswordMaxLength} characters.");
    }
}