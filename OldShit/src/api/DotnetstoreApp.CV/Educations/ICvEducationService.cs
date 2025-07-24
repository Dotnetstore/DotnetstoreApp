using Ardalis.Result;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Educations;

public interface ICvEducationService
{
    ValueTask<Result<CvEducationResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    ValueTask CreateAsync(CvEducationCreateRequest request, CancellationToken cancellationToken = default);
    
    ValueTask UpdateAsync(Guid id, CvEducationUpdateRequest request, CancellationToken cancellationToken = default);
    
    ValueTask DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}