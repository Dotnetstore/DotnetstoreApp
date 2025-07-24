using DotnetstoreApp.SharedKernel.Models;

namespace DotnetstoreApp.CV.Information;

public sealed class CvInformationId : ValueObject
{
    internal Guid Value { get; }

    private CvInformationId(Guid value)
    {
        Value = value;
    }
    
    internal static CvInformationId Create()
    {
        return new CvInformationId(Guid.CreateVersion7());
    }
    
    internal static CvInformationId Create(Guid value)
    {
        return new CvInformationId(value);
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