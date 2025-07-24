namespace DotnetstoreApp.SDK.Requests.CV;

public record struct CvEducationUpdateRequest(
    Guid CvId,
    string School,
    string Level,
    DateTime StartDate,
    DateTime? EndDate);