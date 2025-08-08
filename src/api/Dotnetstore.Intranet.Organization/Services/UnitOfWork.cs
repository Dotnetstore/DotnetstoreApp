using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dotnetstore.Intranet.Organization.Services;

internal class UnitOfWork(
    OrganizationDataContext context,
    IApplicationUserRoleRepository roleRepository,
    IApplicationUserInRoleRepository userInRoleRepository,
    IApplicationUserRepository userRepository)
    : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? _objTran;

    public IApplicationUserRoleRepository Roles { get; } = roleRepository;
    public IApplicationUserInRoleRepository UserInRoles { get; } = userInRoleRepository;
    public IApplicationUserRepository Users { get; } = userRepository;

    public void CreateTransaction()
    {
        _objTran = context.Database.BeginTransaction();
    }
        
    public void Commit()
    {
        _objTran?.Commit();
    }
        
    public void Rollback()
    {
        _objTran?.Rollback();
        _objTran?.Dispose();
    }
        
    public async ValueTask<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await context.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception(ex.Message, ex);
        }
    }
        
    public void Dispose()
    {
        context.Dispose();
    }
}