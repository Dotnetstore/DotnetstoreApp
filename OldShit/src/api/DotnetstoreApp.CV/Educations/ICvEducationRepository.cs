namespace DotnetstoreApp.CV.Educations;

public interface ICvEducationRepository
{
    ValueTask<CvEducation?> GetByIdAsync(CvEducationId id, CancellationToken cancellationToken = default);
    
    ValueTask CreateAsync(CvEducation education, CancellationToken cancellationToken = default);
    
    ValueTask UpdateAsync(CvEducation education, CancellationToken cancellationToken = default);
    
    ValueTask DeleteAsync(CvEducationId id, CancellationToken cancellationToken = default);
}