using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Experiences;

public sealed class CvExperience : AggregateRoot<CvExperienceId>
{
    public CvId CvId { get; }
    
    public DateTime StartDate { get; }
    
    public DateTime? EndDate { get; }
    
    public string Company { get; }
    
    public bool IsConsultant { get; }
    
    public string Role { get; }
    
    public int Extent { get; }
    
    public string Tools { get; }
    
    public string CompanyNeeds { get; }
    
    public string Mission { get; }
    
    public Cv Cv { get; private set; } = null!;

    private CvExperience(
        CvExperienceId id,
        CvId cvId, 
        DateTime startDate, 
        DateTime? endDate, 
        string company, 
        bool isConsultant, 
        string role, 
        int extent, 
        string tools, 
        string companyNeeds, 
        string mission) : base(id)
    {
        CvId = cvId;
        StartDate = startDate;
        EndDate = endDate;
        Company = company;
        IsConsultant = isConsultant;
        Role = role;
        Extent = extent;
        Tools = tools;
        CompanyNeeds = companyNeeds;
        Mission = mission;
    }
    
    internal static CvExperience Create(
        CvId cvId, 
        DateTime startDate, 
        DateTime? endDate, 
        string company, 
        bool isConsultant, 
        string role, 
        int extent, 
        string tools, 
        string companyNeeds, 
        string mission)
    {
        return new CvExperience(
            CvExperienceId.Create(), 
            cvId, 
            startDate, 
            endDate, 
            company, 
            isConsultant, 
            role, 
            extent, 
            tools, 
            companyNeeds, 
            mission);
    }
    
    internal static CvExperience Create(
        CvExperienceId id,
        CvId cvId, 
        DateTime startDate, 
        DateTime? endDate, 
        string company, 
        bool isConsultant, 
        string role, 
        int extent, 
        string tools, 
        string companyNeeds, 
        string mission)
    {
        return new CvExperience(
            id, 
            cvId, 
            startDate, 
            endDate, 
            company, 
            isConsultant, 
            role, 
            extent, 
            tools, 
            companyNeeds, 
            mission);
    }
}