namespace DotnetstoreApp.SDK.Responses.CV;

public record struct CvEducationResponse(
    Guid Id,
    Guid CvId,
    string School,
    string Level,
    DateTime StartDate,
    DateTime? EndDate);