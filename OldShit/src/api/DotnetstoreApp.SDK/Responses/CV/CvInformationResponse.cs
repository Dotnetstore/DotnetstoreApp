using DotnetstoreApp.SDK.Enums;

namespace DotnetstoreApp.SDK.Responses.CV;

public record struct CvInformationResponse(
    Guid Id,
    Guid CvId,
    string Name,
    CvInformationType InformationType);