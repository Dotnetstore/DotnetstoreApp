namespace DotnetstoreApp.SDK.Responses.CV;

public record struct CvFullResponse(
    Guid Id,
    string Name,
    string Language,
    string LastName,
    string FirstName,
    DateTime DateOfBirth,
    string Introduction,
    List<CvExperienceResponse> Experiences,
    List<CvInformationResponse> Information,
    List<CvEducationResponse> Educations);