using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Cvs;

public sealed class CvId : ValueObject
{
    internal Guid Value { get; }

    private CvId(Guid value)
    {
        Value = value;
    }
    
    internal static CvId Create()
    {
        return new CvId(Guid.CreateVersion7());
    }
    
    internal static CvId Create(Guid value)
    {
        return new CvId(value);
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