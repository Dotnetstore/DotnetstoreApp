namespace DotnetstoreApp.CV.Experiences;

public interface ICvExperienceRepository
{
    ValueTask<CvExperience?> GetByIdAsync(CvExperienceId id, CancellationToken cancellationToken = default);
    
    ValueTask CreateAsync(CvExperience cvExperience, CancellationToken cancellationToken = default);
    
    ValueTask UpdateAsync(CvExperience cvExperience, CancellationToken cancellationToken = default);
    
    ValueTask DeleteAsync(CvExperienceId id, CancellationToken cancellationToken = default);
}