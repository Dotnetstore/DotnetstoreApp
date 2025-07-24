using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Educations;

internal static class CvEducationMappers
{
    internal static CvEducationResponse ToCvEducationResponse(this CvEducation education) =>
        new(
            education.Id.Value,
            education.CvId.Value,
            education.School,
            education.Level,
            education.StartDate,
            education.EndDate);
}