using Dotnetstore.Intranet.SharedKernel.Models;

namespace Dotnetstore.Intranet.Organization.Roles;

public class ApplicationUserRoleId : ValueObject
{
    internal Guid Value { get; }

    private ApplicationUserRoleId(Guid value)
    {
        Value = value;
    }
    
    internal static ApplicationUserRoleId Create()
    {
        return new ApplicationUserRoleId(Guid.CreateVersion7());
    }
    
    internal static ApplicationUserRoleId Create(Guid value)
    {
        return new ApplicationUserRoleId(value);
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