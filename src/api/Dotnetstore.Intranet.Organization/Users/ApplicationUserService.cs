using Dotnetstore.Intranet.Organization.Services;

namespace Dotnetstore.Intranet.Organization.Users;

internal sealed class ApplicationUserService(
    IAuthService authService,
    TimeProvider timeProvider,
    IEventService eventService,
    ITokenService tokenService,
    IUnitOfWork unitOfWork,
    ILogger<ApplicationUserService> logger) : IApplicationUserService
{
    private readonly ApplicationUserFactory _applicationUserFactory = new(authService, timeProvider);
    
    async ValueTask<IEnumerable<ApplicationUser>> IApplicationUserService.GetAllNotDeletedAsync(CancellationToken cancellationToken)
    {
        return await unitOfWork.Users.GetAllNotDeletedAsync(cancellationToken).ConfigureAwait(false);
    }

    async ValueTask<Result> IApplicationUserService.CreateAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken)
    {
        var emailConfirmationCode = Guid.CreateVersion7().ToString().Replace("-", string.Empty);
        var result = await CheckIfUserExistAndAddUserAsync(request, emailConfirmationCode, cancellationToken).ConfigureAwait(false);
        
        if (!result.IsSuccess)
        {
            logger.LogWarning("Failed to create user with email {EmailAddress}: {ErrorMessage}", request.EmailAddress, string.Join(", ", result.Errors));
            return Result.Error("Failed to create user: " + string.Join(", ", result.Errors));
        }
        
        _ = eventService.ApplicationUserAddedEventAsync(
            emailConfirmationCode,
            result.Value.Id,
            request.FirstName,
            request.LastName,
            request.EmailAddress,
            cancellationToken);
        
        return Result.Success();
    }

    async ValueTask<Result> IApplicationUserService.ConfirmUsersEmailAddressAsync(string confirmationCode, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.GetByEmailConfirmationCodeAsync(confirmationCode, cancellationToken).ConfigureAwait(false);
        
        if (user is null)
        {
            logger.LogWarning("Email confirmation failed: No user found with confirmation code {ConfirmationCode}", confirmationCode);
            return Result.Error("No user found with the provided confirmation code.");
        }
        
        user.EmailAddressIsConfirmed = true;
        user.EmailAddressConfirmationCode = null;
        user.LastUpdatedDate = DateTime.UtcNow;
        
        unitOfWork.Users.Update(user);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        if (result < 1)
        {
            logger.LogError("Failed to confirm email address for user with confirmation code {ConfirmationCode}", confirmationCode);
            return Result.Error("Failed to confirm email address.");
        }
        
        logger.LogInformation("Email address confirmed for user with confirmation code {ConfirmationCode}", confirmationCode);
        return Result.Success();
    }

    private async ValueTask<Result<ApplicationUser>> CheckIfUserExistAndAddUserAsync(
        ApplicationUserRegisterRequest request, 
        string emailConfirmationCode,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling user creation request for: {EmailAddress}", request.EmailAddress);
        
        var existingUser = await unitOfWork.Users.GetByUsernameAsync(request.EmailAddress, cancellationToken).ConfigureAwait(false);

        if (existingUser is not null)
        {
            logger.LogWarning("User with email {EmailAddress} already exists.", request.EmailAddress);
            return Result<ApplicationUser>.Error("A user with this email already exists.");
        }
        
        var user = _applicationUserFactory.Create(emailConfirmationCode, request);
        
        await unitOfWork.Users.InsertAsync(user, cancellationToken).ConfigureAwait(false);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        if (result < 1)
        {
            logger.LogError("Failed to save new user with email {EmailAddress} to the database.", request.EmailAddress);
            return Result<ApplicationUser>.Error("Failed to save new user to the database.");
        }

        return Result<ApplicationUser>.Success(user);
    }

    async ValueTask<Result<(string Token, string RefreshToken)>> IApplicationUserService.LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling login request for user: {Username}", request.Username);
        
        var user = await unitOfWork.Users.GetValidUserByUsernameAsync(request.Username, cancellationToken).ConfigureAwait(false);
        
        if (user is null ||
            !authService.VerifyPassword(request.Password, user.PasswordHash))
        {
            logger.LogWarning("Login failed for user {Username}: User not found or not approved", request.Username);
            return Result<(string Token, string RefreshToken)>.NotFound("User name or password is incorrect.");
        }
        var token = tokenService.GenerateToken(user);
        await tokenService.UpdateRefreshTokenAsync(user, cancellationToken).ConfigureAwait(false);
        
        logger.LogInformation("Login successful for user: {Username}", request.Username);
        return Result<(string Token, string RefreshToken)>.Success((token, user.RefreshToken!));
    }

    async ValueTask<Result> IApplicationUserService.SetApproveStatusAsync(ApplicationUserId userId, bool isApproved, CancellationToken cancellationToken)
    {
        logger.LogInformation("Setting approve status for user {UserId} to {IsApproved}", userId, isApproved);
        
        var user = await unitOfWork.Users.GetByIdAsync(userId, cancellationToken).ConfigureAwait(false);
        
        if (user is null)
        {
            logger.LogWarning("User with ID {UserId} not found in the database.", userId);
            return Result.Error("User not found in the database.");
        }
        
        user.AccountIsApproved = isApproved;
        user.LastUpdatedDate = DateTime.UtcNow;
        
        unitOfWork.Users.Update(user);
        var result = await unitOfWork.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        if (result < 1)
        {
            logger.LogError("Failed to update user {UserId} approve status in the database.", userId);
            return Result.Error("Failed to update user approve status in the database.");
        }
        
        logger.LogInformation("User {UserId} approve status set to {IsApproved}", userId, isApproved);
        return Result.Success();
    }
}