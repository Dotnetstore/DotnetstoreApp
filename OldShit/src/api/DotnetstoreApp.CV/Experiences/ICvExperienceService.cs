using Ardalis.Result;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Experiences;

public interface ICvExperienceService
{
    ValueTask<Result<CvExperienceResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    ValueTask<Result> CreateAsync(CvExperienceCreateRequest request, CancellationToken cancellationToken = default);
    
    ValueTask<Result> UpdateAsync(Guid id, CvExperienceUpdateRequest request, CancellationToken cancellationToken = default);
    
    ValueTask<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}