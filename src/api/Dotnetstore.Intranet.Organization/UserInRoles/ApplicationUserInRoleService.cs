using Ardalis.Result;
using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

internal sealed class ApplicationUserInRoleService : IApplicationUserInRoleService
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
        
        OrganizationDatabase.UserInRoles.Add(userInRole);
        
        return Result.Success();
    }
}