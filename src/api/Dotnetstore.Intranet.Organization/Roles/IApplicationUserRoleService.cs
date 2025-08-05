namespace Dotnetstore.Intranet.Organization.Roles;

internal interface IApplicationUserRoleService
{
    ValueTask<ApplicationUserRole?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);
}