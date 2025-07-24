using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Experiences;

public sealed class CvExperienceId : ValueObject
{
    internal Guid Value { get; }

    private CvExperienceId(Guid value)
    {
        Value = value;
    }
    
    internal static CvExperienceId Create()
    {
        return new CvExperienceId(Guid.CreateVersion7());
    }
    
    internal static CvExperienceId Create(Guid value)
    {
        return new CvExperienceId(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}