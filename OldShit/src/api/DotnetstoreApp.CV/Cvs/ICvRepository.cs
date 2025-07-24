namespace DotnetstoreApp.CV.Cvs;

public interface ICvRepository
{
    ValueTask<IEnumerable<Cv>> GetAllAsync(CancellationToken cancellationToken = default);
    
    ValueTask<Cv?> GetByIdAsync(CvId id, CancellationToken cancellationToken = default);
    
    ValueTask CreateAsync(Cv cv, CancellationToken cancellationToken = default);
    
    void Update(Cv cv, CancellationToken cancellationToken = default);
    
    ValueTask DeleteAsync(CvId id, CancellationToken cancellationToken = default);
}