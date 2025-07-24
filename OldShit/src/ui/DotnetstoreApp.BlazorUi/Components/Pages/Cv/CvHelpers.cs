using DotnetstoreApp.SDK.Enums;
using DotnetstoreApp.SDK.Services;
using FluentValidation;

namespace DotnetstoreApp.BlazorUi.Components.Pages.Cv;

internal static class CvHelpers
{
    internal static string GetInformationTypeName(CvInformationType type)
    {
        return type switch
        {
            CvInformationType.Architecture => "Arkitektur",
            CvInformationType.Cloud => "Moln",
            CvInformationType.DesiredRole => "Önskad roll",
            CvInformationType.Devops => "Devops",
            CvInformationType.Language => "Språk",
            CvInformationType.Leadership => "Ledarskap",
            CvInformationType.Programming => "Programmering",
            _ => "Okänd"
        };
    }
}

internal sealed class CvInformationModel
{
    public Guid CvId { get; set; }
    public string Name { get; set; } = string.Empty;
    public CvInformationType InformationType { get; set; }
}

internal sealed class CvExperienceModel
{
    public Guid CvId { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public string Company { get; set; } = string.Empty;
    
    public bool IsConsultant { get; set; }
    
    public string Role { get; set; } = string.Empty;
    
    public int Extent { get; set; }
    
    public string Tools { get; set; } = string.Empty;
    
    public string CompanyNeeds { get; set; } = string.Empty;
    
    public string Mission { get; set; } = string.Empty;
}
    
internal sealed class CvEducationModel
{
    public Guid CvId { get; set; }
    
    public DateTime? StartDate { get; set; }
    
    public DateTime? EndDate { get; set; }
    
    public string School { get; set; } = string.Empty;
    
    public string Level { get; set; } = string.Empty;
}

internal sealed class CvInformationFluentValidator : AbstractValidator<CvInformationModel>
{
    public CvInformationFluentValidator()
    {
        RuleFor(x => x.CvId).NotEmpty().WithMessage("CV ID is required.");
            
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(DataSchemeConstants.MaxCvInformationNameLength).WithMessage($"Name must not exceed {DataSchemeConstants.MaxCvInformationNameLength} characters.");
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CvInformationModel>.CreateWithOptions((CvInformationModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return [];
        return result.Errors.Select(e => e.ErrorMessage);
    };
}

internal sealed class CvExperienceFluentValidator : AbstractValidator<CvExperienceModel>
    {
        public CvExperienceFluentValidator()
        {
            RuleFor(x => x.CvId)
                .NotEmpty().WithMessage("CV Id får inte vara tomt.");
            
            RuleFor(x => x.StartDate)
                .NotEmpty().WithMessage("Startdatum får inte vara tomt.")
                .LessThanOrEqualTo(x => x.EndDate).WithMessage("Startdatum måste vara före eller lika med slutdatum.").When(x => x.EndDate.HasValue)
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
        
        public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
        {
            var result = await ValidateAsync(ValidationContext<CvExperienceModel>.CreateWithOptions((CvExperienceModel)model, x => x.IncludeProperties(propertyName)));
            if (result.IsValid)
                return [];
            return result.Errors.Select(e => e.ErrorMessage);
        };
    }
    
internal sealed class CvEducationFluentValidator : AbstractValidator<CvEducationModel>
{
    public CvEducationFluentValidator()
    {
        RuleFor(x => x.CvId)
            .NotEmpty().WithMessage("CV Id får inte vara tomt.");
            
        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("Startdatum får inte vara tomt.")
            .LessThanOrEqualTo(x => x.EndDate).WithMessage("Startdatum måste vara före eller lika med slutdatum.").When(x => x.EndDate.HasValue)
            .GreaterThan(x => DateTime.Now.AddYears(-60)).WithMessage("Startdatum får inte vara mer än 60 år i det förflutna.");
            
        RuleFor(x => x.EndDate)
            .GreaterThanOrEqualTo(x => x.StartDate).WithMessage("Slutdatum måste vara efter eller lika med startdatum.").When(x => x.EndDate.HasValue);
            
        RuleFor(x => x.School)
            .NotEmpty().WithMessage("Skola får inte vara tomt.")
            .MaximumLength(DataSchemeConstants.MaxCvExperienceCompanyLength).WithMessage($"Skola får inte vara längre än {DataSchemeConstants.MaxCvExperienceCompanyLength} tecken.");
            
        RuleFor(x => x.Level)
            .NotEmpty().WithMessage("Nivå får inte vara tomt.")
            .MaximumLength(DataSchemeConstants.MaxCvExperienceRoleLength).WithMessage($"Nivå får inte vara längre än {DataSchemeConstants.MaxCvExperienceRoleLength} tecken.");
    }
        
    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result = await ValidateAsync(ValidationContext<CvEducationModel>.CreateWithOptions((CvEducationModel)model, x => x.IncludeProperties(propertyName)));
        if (result.IsValid)
            return [];
        return result.Errors.Select(e => e.ErrorMessage);
    };
}