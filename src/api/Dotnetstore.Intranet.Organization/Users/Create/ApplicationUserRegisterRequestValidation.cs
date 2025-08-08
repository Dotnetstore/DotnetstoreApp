namespace Dotnetstore.Intranet.Organization.Users.Create;

internal sealed class ApplicationUserRegisterRequestValidation : Validator<ApplicationUserRegisterRequest>
{
    public ApplicationUserRegisterRequestValidation()
    {
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(DataSchemeConstants.UserLastNameMaxLength).WithMessage("Last name must not exceed {MaxLength} characters.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(DataSchemeConstants.UserFirstNameMaxLength).WithMessage("First name must not exceed {MaxLength} characters.");
        
        RuleFor(x => x.MiddleName)
            .MaximumLength(DataSchemeConstants.UserMiddleNameMaxLength).WithMessage("Middle name must not exceed {MaxLength} characters.").When(x => !string.IsNullOrEmpty(x.MiddleName));
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("Date of birth is required.")
            .LessThan(DateTime.UtcNow).WithMessage("Date of Birth must be in the past.")
            .GreaterThan(DateTime.UtcNow.AddYears(-70)).WithMessage("Date of Birth must be at least 70 years ago.");
        
        RuleFor(x => x.SocialSecurityNumber)
            .Must(x => Personnummer.Personnummer.Valid(x))
            .MaximumLength(DataSchemeConstants.UserSocialSecurityNumberMaxLength).WithMessage("Social Security Number must not exceed {MaxLength} characters.").When(x => !string.IsNullOrEmpty(x.SocialSecurityNumber));
        
        RuleFor(x => x.EmailAddress)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(DataSchemeConstants.UserEmailMaxLength).WithMessage("Email must not exceed {MaxLength} characters.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least {MinLength} characters long.")
            .MaximumLength(DataSchemeConstants.UserPasswordMaxLength).WithMessage("Password must not exceed {MaxLength} characters.");
        
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm Password is required.")
            .Equal(x => x.Password).WithMessage("Confirm Password must match Password.")
            .MaximumLength(DataSchemeConstants.UserPasswordMaxLength).WithMessage("Confirm Password must not exceed {MaxLength} characters.");
    }
}