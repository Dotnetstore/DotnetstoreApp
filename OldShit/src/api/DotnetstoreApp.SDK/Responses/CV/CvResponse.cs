namespace DotnetstoreApp.SDK.Responses.CV;

public record struct CvResponse(
    Guid Id,
    string Name,
    string Language);