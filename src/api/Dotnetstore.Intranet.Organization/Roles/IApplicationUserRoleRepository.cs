namespace Dotnetstore.Intranet.Organization.Roles;

internal interface IApplicationUserRoleRepository : IGenericRepository<ApplicationUserRole>
{
    ValueTask<ApplicationUserRole?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}