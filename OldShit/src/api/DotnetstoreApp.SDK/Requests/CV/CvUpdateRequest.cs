namespace DotnetstoreApp.SDK.Requests.CV;

public record struct CvUpdateRequest(
    string Name,
    string Language,
    string LastName,
    string FirstName,
    DateTime DateOfBirth,
    string Introduction);