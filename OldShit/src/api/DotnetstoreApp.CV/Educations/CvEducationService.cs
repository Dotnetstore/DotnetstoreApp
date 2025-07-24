using Ardalis.Result;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Educations;

public sealed class CvEducationService(ICvEducationRepository repository) : ICvEducationService
{
    async ValueTask<Result<CvEducationResponse>> ICvEducationService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var education = await repository.GetByIdAsync(CvEducationId.Create(id), cancellationToken);

        if (education is null) return Result<CvEducationResponse>.NotFound("No education found.");
        
        return Result<CvEducationResponse>.Success(education.ToCvEducationResponse());
    }

    async ValueTask ICvEducationService.CreateAsync(CvEducationCreateRequest request, CancellationToken cancellationToken)
    {
        var education = CvEducationBuilder.Create()
            .WithId()
            .WithCvId(request.CvId)
            .WithSchool(request.School)
            .WithLevel(request.Level)
            .WithStartDate(request.StartDate)
            .WithEndDate(request.EndDate)
            .Build();
        
        await repository.CreateAsync(education, cancellationToken);
    }

    async ValueTask ICvEducationService.UpdateAsync(Guid id, CvEducationUpdateRequest request, CancellationToken cancellationToken)
    {
        var education = CvEducationBuilder.Create()
            .WithId(id)
            .WithCvId(request.CvId)
            .WithSchool(request.School)
            .WithLevel(request.Level)
            .WithStartDate(request.StartDate)
            .WithEndDate(request.EndDate)
            .Build();
        
        await repository.UpdateAsync(education, cancellationToken);
    }

    async ValueTask ICvEducationService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(CvEducationId.Create(id), cancellationToken);
    }
}