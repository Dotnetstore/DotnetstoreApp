using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Services;
using FluentValidation;

namespace DotnetstoreApp.CV.Information.Create;

internal sealed class CvInformationCreateRequestValidator : AbstractValidator<CvInformationCreateRequest>
{
    public CvInformationCreateRequestValidator()
    {
        RuleFor(x => x.CvId).NotEmpty().WithMessage("CV ID is required.")
            .Must(x => x != Guid.Empty).WithMessage("CV ID must not be empty.");
        
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.")
            .MaximumLength(DataSchemeConstants.MaxCvInformationNameLength).WithMessage($"Name must not exceed {DataSchemeConstants.MaxCvInformationNameLength} characters.");
    }
}