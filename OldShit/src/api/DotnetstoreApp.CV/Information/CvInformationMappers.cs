using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Information;

internal static class CvInformationMappers
{
    internal static CvInformationResponse ToCvInformationResponse(this CvInformation information)
    {
        return new CvInformationResponse(
            information.Id.Value,
            information.CvId.Value,
            information.Name,
            information.CvInformationType);
    }
}