using Dotnetstore.Intranet.SharedKernel.Models;

namespace Dotnetstore.Intranet.Organization.Users;

public class ApplicationUserId : ValueObject
{
    internal Guid Value { get; }

    private ApplicationUserId(Guid value)
    {
        Value = value;
    }
    
    internal static ApplicationUserId Create()
    {
        return new ApplicationUserId(Guid.CreateVersion7());
    }
    
    internal static ApplicationUserId Create(Guid value)
    {
        return new ApplicationUserId(value);
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