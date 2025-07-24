using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.SDK.Enums;
using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Information;

public sealed class CvInformation : AggregateRoot<CvInformationId>
{
    public CvId CvId { get; }
    
    public string Name { get; }
    
    public CvInformationType CvInformationType { get; }

    private CvInformation(
        CvInformationId id,
        CvId cvId,
        string name,
        CvInformationType cvInformationType) : base(id)
    {
        CvId = cvId;
        Name = name;
        CvInformationType = cvInformationType;
    }
    
    internal static CvInformation Create(
        CvId cvId,
        string name,
        CvInformationType cvInformationType)
    {
        return new CvInformation(
            CvInformationId.Create(Guid.CreateVersion7()), 
            cvId,
            name,
            cvInformationType);
    }
    
    internal static CvInformation Create(
        CvInformationId id,
        CvId cvId,
        string name,
        CvInformationType cvInformationType)
    {
        return new CvInformation(
            id, 
            cvId,
            name,
            cvInformationType);
    }
}