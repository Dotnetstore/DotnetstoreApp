using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Services;

internal interface IUnitOfWork
{
    IApplicationUserRoleRepository Roles { get; }
    IApplicationUserInRoleRepository UserInRoles { get; }
    IApplicationUserRepository Users { get; }
    void CreateTransaction();
    void Commit();
    void Rollback();
    ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}