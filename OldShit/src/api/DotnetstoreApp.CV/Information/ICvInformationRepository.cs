namespace DotnetstoreApp.CV.Information;

public interface ICvInformationRepository
{
    ValueTask CreateAsync(CvInformation information, CancellationToken cancellationToken = default);
    
    ValueTask DeleteAsync(CvInformationId id, CancellationToken cancellationToken = default);
}