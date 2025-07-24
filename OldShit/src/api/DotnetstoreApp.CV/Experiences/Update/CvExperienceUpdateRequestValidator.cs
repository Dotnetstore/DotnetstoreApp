using FluentValidation;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Services;

namespace DotnetstoreApp.CV.Experiences.Update;

internal sealed class CvExperienceUpdateRequestValidator : AbstractValidator<CvExperienceUpdateRequest>
{
    public CvExperienceUpdateRequestValidator()
    {
        RuleFor(x => x.CvId)
            .NotEmpty().WithMessage("CV Id får inte vara tomt.");
            
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Startdatum får inte vara tomt.")
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Startdatum måste vara före eller lika med slutdatum.")
            .GreaterThan(x => DateTime.Now.AddYears(-60)).WithMessage("Startdatum får inte vara mer än 60 år i det förflutna.");
            
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Slutdatum måste vara efter eller lika med startdatum.").When(x => x.EndDate.HasValue);
            
        RuleFor(x => x.Company)
            .NotEmpty().WithMessage("Företag får inte vara tomt.")
            .MaximumLength(DataSchemeConstants.MaxCvExperienceCompanyLength).WithMessage($"Företag får inte vara längre än {DataSchemeConstants.MaxCvExperienceCompanyLength} tecken.");
            
        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Roll får inte vara tomt.")
            .MaximumLength(DataSchemeConstants.MaxCvExperienceRoleLength).WithMessage($"Roll får inte vara längre än {DataSchemeConstants.MaxCvExperienceRoleLength} tecken.");
            
        RuleFor(x => x.Extent)
            .InclusiveBetween(1, 100).WithMessage("Omfattning måste vara mellan 1 och 100%.");
            
        RuleFor(x => x.Tools)
            .NotEmpty().WithMessage("Metoder & verktyg får inte vara tomt.")
            .MaximumLength(DataSchemeConstants.MaxCvExperienceToolsLength).WithMessage($"Metoder & verktyg får inte vara längre än {DataSchemeConstants.MaxCvExperienceToolsLength} tecken.");

        RuleFor(x => x.CompanyNeeds)
            .NotEmpty().WithMessage("Företagets behov får inte vara tomt.");

        RuleFor(x => x.Mission)
            .NotEmpty().WithMessage("Uppdrag får inte vara tomt.");
    }
}