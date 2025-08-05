using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Data;

internal sealed class OrganizationDatabase
{
    internal static List<ApplicationUser> Users { get; } = [];
    
    internal static List<ApplicationUserRole> Roles { get; } = [
        ApplicationUserRole.Create("Administrator", "Administrator description", DateTime.Now, isSystem: true)];
    
    internal static List<ApplicationUserInRole> UserInRoles { get; } = [];
}