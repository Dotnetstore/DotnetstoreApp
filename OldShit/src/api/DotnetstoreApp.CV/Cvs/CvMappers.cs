using DotnetstoreApp.CV.Educations;
using DotnetstoreApp.CV.Experiences;
using DotnetstoreApp.CV.Information;
using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Cvs;

internal static class CvMappers
{
    internal static CvResponse ToCvResponse(this Cv cv)
    {
        return new CvResponse(
            cv.Id.Value,
            cv.Name,
            cv.Language);
    }
    
    internal static CvFullResponse ToCvFullResponse(this Cv cv)
    {
        return new CvFullResponse(
            cv.Id.Value,
            cv.Name,
            cv.Language,
            cv.LastName,
            cv.FirstName,
            cv.DateOfBirth,
            cv.Introduction,
            cv.Experiences.Select(e => e.ToCvExperienceResponse()).ToList(),
            cv.Information.Select(i => i.ToCvInformationResponse()).ToList(),
            cv.Educations.Select(e => e.ToCvEducationResponse()).ToList());
    }
}