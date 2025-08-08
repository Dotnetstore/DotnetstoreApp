using Dotnetstore.Intranet.Organization.Data;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

internal sealed class ApplicationUserInRoleRepository(OrganizationDataContext context)
    : GenericRepository<ApplicationUserInRole>(context), IApplicationUserInRoleRepository
{
}