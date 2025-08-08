using Dotnetstore.Intranet.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Handlers;

internal sealed class ApplicationUserCreatedCheckApproveStatusHandler(
    IApplicationUserService applicationUserService,
    ILogger<ApplicationUserCreatedSetRoleHandler> logger) : IHandleMessages<ApplicationUserAddedEvent>
{
    public async Task Handle(ApplicationUserAddedEvent message, IMessageHandlerContext context)
    {
        logger.LogInformation("Handling ApplicationUserAddedEvent in ApplicationUserCreatedCheckApproveStatusHandler for user {UserId} with email {EmailAddress}",
            message.UserId, message.EmailAddress);
        
        var users = await applicationUserService.GetAllNotDeletedAsync(context.CancellationToken);
        var usersAsList = users.ToList();
        
        if (usersAsList.Count == 0)
        {
            logger.LogWarning("No users found in the system. Cannot check approve status for user {UserId}", message.UserId);
            return;
        }
        
        var user = usersAsList.FirstOrDefault(u => u.Id == ApplicationUserId.Create(message.UserId));
        if (user is null)
        {
            logger.LogInformation("User with ID {UserId} not found in the system.", message.UserId);
            return;
        }
        
        if (usersAsList.Count == 1)
        {
            logger.LogInformation("Only one user found in the system. Automatically setting approve status for user {UserId}", message.UserId);
            await applicationUserService.SetApproveStatusAsync(user.Id, true, context.CancellationToken);
        }
    }
}