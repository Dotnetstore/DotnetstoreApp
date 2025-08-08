using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.Services;
using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

internal sealed class ApplicationUserInRoleService(
    IUnitOfWork unitOfWork) : IApplicationUserInRoleService
{
    async ValueTask<Result> IApplicationUserInRoleService.CreateAsync(
        ApplicationUserId userId,
        ApplicationUserRoleId roleId,
        CancellationToken cancellationToken)
    {
        var userInRole = ApplicationUserInRoleBuilder.Create()
            .WithId()
            .WithUserId(userId.Value)
            .WithRoleId(roleId.Value)
            .WithCreatedDate(DateTime.UtcNow)
            .Build();

        await Task.CompletedTask;
        
        await unitOfWork.UserInRoles.InsertAsync(userInRole, cancellationToken).ConfigureAwait(false);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        if (result < 1)
        {
            return Result.Error("Failed to create user in role.");
        }
        
        return Result.Success();
    }
}