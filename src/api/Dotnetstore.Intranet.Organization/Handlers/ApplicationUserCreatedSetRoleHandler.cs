using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Handlers;

internal sealed class ApplicationUserCreatedSetRoleHandler(
    IApplicationUserService applicationUserService,
    IApplicationUserRoleService applicationUserRoleService,
    IApplicationUserInRoleService applicationUserInRoleService,
    ILogger<ApplicationUserCreatedSetRoleHandler> logger) : IHandleMessages<ApplicationUserAddedEvent>
{
    public async Task Handle(ApplicationUserAddedEvent message, IMessageHandlerContext context)
    {
        logger.LogInformation("Handling ApplicationUserAddedEvent in ApplicationUserCreatedSetRoleHandler for user {UserId} with email {EmailAddress}",
            message.UserId, message.EmailAddress);
        
        var (isFirst, user) = await GetUserAsync(ApplicationUserId.Create(message.UserId), context);
        if (user is null) return;
        
        if (isFirst && await AddUserToRoleAsync(user.Id, "Administrator", context))
        {
            logger.LogInformation("Assigned Administrator role to user {UserId}", user.Id);
            return;
        }
        
        if (await AddUserToRoleAsync(user.Id, "User", context))
            logger.LogInformation("Assigned User role to user {UserId}", user.Id);
    }
    
    private async ValueTask<(bool IsFirst, ApplicationUser? User)> GetUserAsync(
        ApplicationUserId userId,
        IMessageHandlerContext context)
    {
        var users = await applicationUserService.GetAllNotDeletedAsync(context.CancellationToken);
        var usersAsList = users.ToList();
        
        if (usersAsList.Count == 0)
        {
            logger.LogWarning("No users found in the system. Cannot set role for user {UserId}", userId);
            return (false, null);
        }
        
        var user = usersAsList.FirstOrDefault(u => u.Id == userId);
        
        if (user is null)
        {
            logger.LogInformation("User with ID {UserId} not found in the system.", userId);
            return (false, null);
        }
        
        if (usersAsList.Count == 1)
        {
            logger.LogInformation("Only one user found in the system. Automatically setting role for user {UserId}", userId);
            return (true, user);
        }
        
        return (false, user);
    }
    
    private async ValueTask<bool> AddUserToRoleAsync(
        ApplicationUserId userId,
        string roleName,
        IMessageHandlerContext context)
    {
        var role = await applicationUserRoleService.GetByNameAsync(roleName, context.CancellationToken);
        
        if (role is null)
        {
            logger.LogError("Role {RoleName} not found. Cannot add user {UserId} to role.", roleName, userId);
            return false;
        }
        
        await applicationUserInRoleService.CreateAsync(userId, role.Id, context.CancellationToken);
        return true;
    }
}