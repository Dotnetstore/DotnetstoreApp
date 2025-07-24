using FluentValidation;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Services;

namespace DotnetstoreApp.CV.Cvs.Update;

internal sealed class CvUpdateRequestValidator : AbstractValidator<CvUpdateRequest>
{
    public CvUpdateRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotNull().NotEmpty().WithMessage("Name is required.")
            .MaximumLength(DataSchemeConstants.MaxCvNameLength).WithMessage($"Name must not exceed {DataSchemeConstants.MaxCvNameLength} characters.");
        
        RuleFor(c => c.Language)
            .NotNull().NotEmpty().WithMessage("Language is required.")
            .MaximumLength(DataSchemeConstants.MaxCvLanguageLength).WithMessage($"Language must not exceed {DataSchemeConstants.MaxCvLanguageLength} characters.");
        
        RuleFor(c => c.LastName)  
            .NotNull().NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(DataSchemeConstants.MaxCvLastNameLength).WithMessage($"Last name must not exceed {DataSchemeConstants.MaxCvLastNameLength} characters.");
        
        RuleFor(c => c.FirstName)
            .NotNull().NotEmpty().WithMessage("First name is required.")
            .MaximumLength(DataSchemeConstants.MaxCvFirstNameLength).WithMessage($"First name must not exceed {DataSchemeConstants.MaxCvFirstNameLength} characters.");
        
        RuleFor(c => c.DateOfBirth)
            .NotNull().WithMessage("Date of birth is required.")
            .LessThan(DateTime.UtcNow.AddYears(-15)).WithMessage("Date of birth must be in the past.")
            .GreaterThan(DateTime.UtcNow.AddYears(-70)).WithMessage("Date of birth must be within the last 70 years.");
        
        RuleFor(c => c.Introduction)
            .NotNull().NotEmpty().WithMessage("Introduction is required.")
            .MaximumLength(DataSchemeConstants.MaxCvIntroductionLength).WithMessage($"Introduction must not exceed {DataSchemeConstants.MaxCvIntroductionLength} characters.");
    }
}