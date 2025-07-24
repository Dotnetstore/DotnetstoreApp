namespace DotnetstoreApp.SDK.Requests.CV;

public record struct CvExperienceUpdateRequest(
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