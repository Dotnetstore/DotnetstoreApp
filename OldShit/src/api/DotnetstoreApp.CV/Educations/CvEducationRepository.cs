using DotnetstoreApp.CV.DB;

namespace DotnetstoreApp.CV.Educations;

public sealed class CvEducationRepository : ICvEducationRepository
{
    async ValueTask<CvEducation?> ICvEducationRepository.GetByIdAsync(CvEducationId id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return FakeDb.EducationList.FirstOrDefault(x => x.Id == id);
    }

    async ValueTask ICvEducationRepository.CreateAsync(CvEducation education, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        FakeDb.EducationList.Add(education);
    }

    async ValueTask ICvEducationRepository.UpdateAsync(CvEducation education, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var existingEducation = FakeDb.EducationList.FirstOrDefault(x => x.Id == education.Id);
        
        if (existingEducation is not null)
        {
            FakeDb.EducationList.Remove(existingEducation);
        }
        
        FakeDb.EducationList.Add(education);
    }

    async ValueTask ICvEducationRepository.DeleteAsync(CvEducationId id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var education = FakeDb.EducationList.FirstOrDefault(x => x.Id == id);
        
        if (education is not null)
        {
            FakeDb.EducationList.Remove(education);
        }
    }
}