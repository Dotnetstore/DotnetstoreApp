using DotnetstoreApp.CV.DB;

namespace DotnetstoreApp.CV.Experiences;

public sealed class CvExperienceRepository : ICvExperienceRepository
{
    async ValueTask<CvExperience?> ICvExperienceRepository.GetByIdAsync(CvExperienceId id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return FakeDb.ExperienceList.FirstOrDefault(x => x.Id == id);
    }

    async ValueTask ICvExperienceRepository.CreateAsync(CvExperience cvExperience, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        FakeDb.ExperienceList.Add(cvExperience);
    }

    async ValueTask ICvExperienceRepository.UpdateAsync(CvExperience cvExperience, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var existingExperience = FakeDb.ExperienceList.FirstOrDefault(x => x.Id == cvExperience.Id);
        
        if(existingExperience is null) return;
        
        FakeDb.ExperienceList.Remove(existingExperience);
        FakeDb.ExperienceList.Add(cvExperience);
    }

    async ValueTask ICvExperienceRepository.DeleteAsync(CvExperienceId id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var existingExperience = FakeDb.ExperienceList.FirstOrDefault(x => x.Id == id);
        
        if(existingExperience is null) return;
        
        FakeDb.ExperienceList.Remove(existingExperience);
    }
}