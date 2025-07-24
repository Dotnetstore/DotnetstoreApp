using DotnetstoreApp.CV.Educations;
using DotnetstoreApp.CV.Experiences;
using DotnetstoreApp.CV.Information;
using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Cvs;

public sealed class Cv : AggregateRoot<CvId>
{
    public string Name { get; }

    public string Language { get; }
    
    public string LastName { get; }
    
    public string FirstName { get; }
    
    public DateTime DateOfBirth { get; }
    
    public string Introduction { get; }
    
    public ICollection<CvExperience> Experiences { get; set; } = new List<CvExperience>();
    public ICollection<CvInformation> Information { get; set; } = new List<CvInformation>();
    public ICollection<CvEducation> Educations { get; set; } = new List<CvEducation>();

    private Cv(
        CvId id, 
        string name, 
        string language,
        string lastName,
        string firstName,
        DateTime dateOfBirth,
        string introduction) : base(id)
    {
        Name = name;
        Language = language;
        LastName = lastName;
        FirstName = firstName;
        DateOfBirth = dateOfBirth;
        Introduction = introduction;
    }

    internal static Cv Create(
        string name, 
        string language,
        string lastName,
        string firstName,
        DateTime dateOfBirth,
        string introduction)
    {
        return new Cv(CvId.Create(), name, language, lastName, firstName, dateOfBirth, introduction);
    }

    internal static Cv Create(
        CvId id, 
        string name, 
        string language,
        string lastName,
        string firstName,
        DateTime dateOfBirth,
        string introduction)
    {
        return new Cv(id, name, language, lastName, firstName, dateOfBirth, introduction);
    }
}