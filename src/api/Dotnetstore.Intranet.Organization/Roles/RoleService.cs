using Dotnetstore.Intranet.Organization.Data;

namespace Dotnetstore.Intranet.Organization.Roles;

internal sealed class RoleService : IRoleService
{
    async ValueTask<ApplicationUserRole?> IRoleService.GetByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        await Task.Delay(100, cancellationToken);

        var role = OrganizationDatabase.Roles.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));

        return role;
    }
}