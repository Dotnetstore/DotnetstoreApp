using DotnetstoreApp.CV.DB;

namespace DotnetstoreApp.CV.Cvs;

internal sealed class CvRepository : ICvRepository
{
    async ValueTask<IEnumerable<Cv>> ICvRepository.GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var list = FakeDb.CvList;
        return list;
    }

    async ValueTask<Cv?> ICvRepository.GetByIdAsync(CvId id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var cv = FakeDb.CvList.FirstOrDefault(x => x.Id == id);

        if (cv is null)
            return null;
        
        cv.Information = FakeDb.InformationList.Where(x => x.CvId == cv.Id).ToList();
        cv.Experiences = FakeDb.ExperienceList.Where(x => x.CvId == cv.Id).ToList();
        cv.Educations = FakeDb.EducationList.Where(x => x.CvId == cv.Id).ToList();
        
        return cv;
    }

    async ValueTask ICvRepository.CreateAsync(Cv cv, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        FakeDb.CvList.Add(cv);
    }

    void ICvRepository.Update(Cv cv, CancellationToken cancellationToken)
    {
        var existingCv = FakeDb.CvList.FirstOrDefault(x => x.Id == cv.Id);

        if (existingCv is null) return;

        FakeDb.CvList.Remove(existingCv);
        FakeDb.CvList.Add(cv);
    }

    async ValueTask ICvRepository.DeleteAsync(CvId id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var existingCv = FakeDb.CvList.FirstOrDefault(x => x.Id == id);

        if (existingCv is null) return;

        foreach (var information in existingCv.Information)
        {
            FakeDb.InformationList.Remove(information);
        }
        
        foreach (var experience in existingCv.Experiences)
        {
            FakeDb.ExperienceList.Remove(experience);
        }
        
        foreach (var education in existingCv.Educations)
        {
            FakeDb.EducationList.Remove(education);
        }
        
        FakeDb.CvList.Remove(existingCv);
    }
}