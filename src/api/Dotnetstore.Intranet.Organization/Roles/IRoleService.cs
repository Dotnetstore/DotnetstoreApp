namespace Dotnetstore.Intranet.Organization.Roles;

internal interface IRoleService
{
    ValueTask<ApplicationUserRole?> GetByNameAsync(string roleName, CancellationToken cancellationToken = default);
}