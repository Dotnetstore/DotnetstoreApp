using Ardalis.Result;
using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

internal interface IApplicationUserInRoleService
{
    ValueTask<Result> CreateAsync(
        ApplicationUserId userId,
        ApplicationUserRoleId roleId,
        CancellationToken cancellationToken = default);
}