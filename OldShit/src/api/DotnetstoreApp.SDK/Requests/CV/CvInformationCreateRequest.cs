using DotnetstoreApp.SDK.Enums;

namespace DotnetstoreApp.SDK.Requests.CV;

public record struct CvInformationCreateRequest(
    Guid CvId,
    string Name,
    CvInformationType CvInformationType);