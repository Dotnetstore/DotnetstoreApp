using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Educations;

public sealed class CvEducationId : ValueObject
{
    internal Guid Value { get; }

    private CvEducationId(Guid value)
    {
        Value = value;
    }
    
    internal static CvEducationId Create()
    {
        return new CvEducationId(Guid.CreateVersion7());
    }
    
    internal static CvEducationId Create(Guid value)
    {
        return new CvEducationId(value);
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