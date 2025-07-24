namespace DotnetstoreApp.SDK.Requests.CV;

public record struct CvEducationCreateRequest(
    Guid CvId,
    string School,
    string Level,
    DateTime StartDate,
    DateTime? EndDate);