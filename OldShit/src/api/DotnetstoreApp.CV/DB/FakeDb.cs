using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Educations;
using DotnetstoreApp.CV.Experiences;
using DotnetstoreApp.CV.Information;

namespace DotnetstoreApp.CV.DB;

internal static class FakeDb
{
    internal static readonly List<Cv> CvList = [];
    
    internal static readonly List<CvExperience> ExperienceList = [];
    
    internal static readonly List<CvInformation> InformationList = [];
    
    internal static readonly List<CvEducation> EducationList = [];
    
    internal static void AddDummyData()
    {
        if (CvList.Count > 0) return;

        var cv = CvBuilder.Create()
            .WithId()
            .WithName("CV 1")
            .WithLanguage("English")
            .WithLastName("Doe")
            .WithFirstName("John")
            .WithDateOfBirth(new DateTime(1990, 1, 1))
            .WithIntroduction("This is a sample CV.")
            .Build();

        CvList.Add(cv);

        // InformationList.Add(CvInformation.Create(cv.Id, "Clean architecture", CvInformationType.Architecture));
        // InformationList.Add(CvInformation.Create(cv.Id, "AWS", CvInformationType.Cloud));
        // InformationList.Add(CvInformation.Create(cv.Id, "Architect", CvInformationType.DesiredRole));
        // InformationList.Add(CvInformation.Create(cv.Id, "Azure devops", CvInformationType.Devops));
        // InformationList.Add(CvInformation.Create(cv.Id, "English", CvInformationType.Language));
        // InformationList.Add(CvInformation.Create(cv.Id, "Tech lead", CvInformationType.Leadership));
        // InformationList.Add(CvInformation.Create(cv.Id, "C#", CvInformationType.Programming));
        //
        // ExperienceList.Add(CvExperience.Create(cv.Id, DateTime.Now, DateTime.Now, "Company A", true, "Developer", 100, "Rider", "Needs", "Mission"));
        // ExperienceList.Add(CvExperience.Create(cv.Id, DateTime.Now, DateTime.Now, "Company B", false, "Senior Developer", 50, "Visual Studio", "Needs", "Mission"));
        //
        // EducationList.Add(CvEducation.Create(cv.Id, "University A", "University", DateTime.Now, DateTime.Now));
    }
}