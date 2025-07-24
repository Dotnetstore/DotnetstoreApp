using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Educations;

public sealed class CvEducation : AggregateRoot<CvEducationId>
{
    public CvId CvId { get; }
    public string School { get; }
    
    public string Level { get; }
    
    public DateTime StartDate { get; }
    
    public DateTime? EndDate { get; }

    private CvEducation(
        CvEducationId id, 
        CvId cvId,
        string school, 
        string level, 
        DateTime startDate, 
        DateTime? endDate) : base(id)
    {
        CvId = cvId;
        School = school;
        Level = level;
        StartDate = startDate;
        EndDate = endDate;
    }

    internal static CvEducation Create(
        CvId cvId,
        string school, 
        string level, 
        DateTime startDate, 
        DateTime? endDate)
    {
        return new CvEducation(CvEducationId.Create(), cvId, school, level, startDate, endDate);
    }

    internal static CvEducation Create(
        CvEducationId id,
        CvId cvId,
        string school,
        string level,
        DateTime startDate,
        DateTime? endDate)
    {
        return new CvEducation(id, cvId, school, level, startDate, endDate);
    }
}