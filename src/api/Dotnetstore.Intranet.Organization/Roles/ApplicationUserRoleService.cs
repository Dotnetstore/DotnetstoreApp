using Dotnetstore.Intranet.Organization.Services;

namespace Dotnetstore.Intranet.Organization.Roles;

internal sealed class ApplicationUserRoleService(IUnitOfWork unitOfWork) : IApplicationUserRoleService
{
    async ValueTask<ApplicationUserRole?> IApplicationUserRoleService.GetByNameAsync(string roleName, CancellationToken cancellationToken)
    {
        return await unitOfWork.Roles.GetByNameAsync(roleName, cancellationToken).ConfigureAwait(false);
    }
}