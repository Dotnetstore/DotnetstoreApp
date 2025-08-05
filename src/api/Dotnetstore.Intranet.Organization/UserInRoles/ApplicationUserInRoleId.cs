using Dotnetstore.Intranet.SharedKernel.Models;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

internal class ApplicationUserInRoleId : ValueObject
{
    internal Guid Value { get; }

    private ApplicationUserInRoleId(Guid value)
    {
        Value = value;
    }
    
    internal static ApplicationUserInRoleId Create()
    {
        return new ApplicationUserInRoleId(Guid.CreateVersion7());
    }
    
    internal static ApplicationUserInRoleId Create(Guid value)
    {
        return new ApplicationUserInRoleId(value);
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