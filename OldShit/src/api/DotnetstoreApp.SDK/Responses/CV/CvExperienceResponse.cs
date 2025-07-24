namespace DotnetstoreApp.SDK.Responses.CV;

public record struct CvExperienceResponse(
    Guid Id,
    Guid CvId,
    DateTime StartDate,
    DateTime? EndDate,
    string Company,
    bool IsConsultant,
    string Role,
    int Extent,
    string Tools,
    string CompanyNeeds,
    string Mission);