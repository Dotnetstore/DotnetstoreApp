using Ardalis.Result;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Experiences;

internal sealed class CvExperienceService(ICvExperienceRepository repository) : ICvExperienceService
{
    async ValueTask<Result<CvExperienceResponse>> ICvExperienceService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var experience = await repository.GetByIdAsync(CvExperienceId.Create(id), cancellationToken);

        return experience is null ? 
            Result<CvExperienceResponse>.NotFound($"Experience with ID {id} not found.") : 
            Result<CvExperienceResponse>.Success(experience.ToCvExperienceResponse());
    }

    async ValueTask<Result> ICvExperienceService.CreateAsync(CvExperienceCreateRequest request, CancellationToken cancellationToken)
    {
        var experience = CvExperienceBuilder.Create()
                .WithId()
                .WithCvId(request.CvId)
                .WithStartDate(request.StartDate)
                .WithEndDate(request.EndDate)
                .WithCompany(request.Company)
                .WithIsConsultant(request.IsConsultant)
                .WithRole(request.Role)
                .WithExtent(request.Extent)
                .WithTools(request.Tools)
                .WithCompanyNeeds(request.CompanyNeeds)
                .WithMission(request.Mission)
                .Build();

        await repository.CreateAsync(experience, cancellationToken);

        return Result.Success();
    }

    async ValueTask<Result> ICvExperienceService.UpdateAsync(Guid id, CvExperienceUpdateRequest request, CancellationToken cancellationToken)
    {
        var experience = CvExperienceBuilder.Create()
            .WithId(id)
            .WithCvId(request.CvId)
            .WithStartDate(request.StartDate)
            .WithEndDate(request.EndDate)
            .WithCompany(request.Company)
            .WithIsConsultant(request.IsConsultant)
            .WithRole(request.Role)
            .WithExtent(request.Extent)
            .WithTools(request.Tools)
            .WithCompanyNeeds(request.CompanyNeeds)
            .WithMission(request.Mission)
            .Build();

        await repository.UpdateAsync(experience, cancellationToken);

        return Result.Success();
    }

    async ValueTask<Result> ICvExperienceService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await repository.DeleteAsync(CvExperienceId.Create(id), cancellationToken);
        return Result.Success();
    }
}