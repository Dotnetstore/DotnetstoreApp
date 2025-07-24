using Ardalis.Result;
using DotnetstoreApp.SDK.Requests.CV;

namespace DotnetstoreApp.CV.Information;

public interface ICvInformationService
{
    ValueTask<Result> CreateAsync(CvInformationCreateRequest request, CancellationToken cancellationToken = default);
    
    ValueTask<Result> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}