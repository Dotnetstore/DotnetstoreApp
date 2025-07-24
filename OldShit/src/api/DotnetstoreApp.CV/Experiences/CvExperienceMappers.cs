using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Experiences;

internal static class CvExperienceMappers
{
    internal static CvExperienceResponse ToCvExperienceResponse(this CvExperience experience) => new(
        experience.Id.Value,
        experience.CvId.Value,
        experience.StartDate,
        experience.EndDate,
        experience.Company,
        experience.IsConsultant,
        experience.Role,
        experience.Extent,
        experience.Tools,
        experience.CompanyNeeds,
        experience.Mission);
}