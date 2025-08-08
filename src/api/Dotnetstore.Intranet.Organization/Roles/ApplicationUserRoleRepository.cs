using Dotnetstore.Intranet.Organization.Data;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.Intranet.Organization.Roles;

internal sealed class ApplicationUserRoleRepository(OrganizationDataContext context) : GenericRepository<ApplicationUserRole>(context), IApplicationUserRoleRepository
{
    async ValueTask<ApplicationUserRole?> IApplicationUserRoleRepository.GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await context
            .ApplicationUserRoles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase), cancellationToken)
            .ConfigureAwait(false);
    }
}