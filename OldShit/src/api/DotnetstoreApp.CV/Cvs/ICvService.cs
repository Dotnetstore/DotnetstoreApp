using Ardalis.Result;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Cvs;

public interface ICvService
{
    ValueTask<IEnumerable<CvResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    
    ValueTask<Result<CvFullResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    ValueTask<Result<CvResponse>> CreateAsync(CvCreateRequest request, CancellationToken cancellationToken = default);
    
    ValueTask<Result> UpdateAsync(Guid id, CvUpdateRequest request, CancellationToken cancellationToken = default);
    
    ValueTask<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}