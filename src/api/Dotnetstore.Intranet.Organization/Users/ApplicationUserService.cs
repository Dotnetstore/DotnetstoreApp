using Ardalis.Result;
using Dotnetstore.Intranet.Email.Services;
using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SharedKernel.Services;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.Intranet.Organization.Users;

public sealed class ApplicationUserService(
    IAuthService authService,
    TimeProvider timeProvider,
    IApplicationUserEmail applicationUserEmail,
    ILogger<ApplicationUserService> logger) : IApplicationUserService
{
    // async ValueTask<IEnumerable<ApplicationUserResponse>> IApplicationUserService.GetAllAsync(CancellationToken cancellationToken)  
    // {
    //     await Task.CompletedTask;
    //     var users = OrganizationDatabase.Users
    //         .OrderBy(x => x.LastName)
    //         .ThenBy(x => x.FirstName)
    //         .ToList();
    //     
    //     return users.Select(x => x.ToApplicationUserResponse());
    // }
    //
    // async ValueTask<Result<ApplicationUserResponse>> IApplicationUserService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    // {
    //     await Task.CompletedTask;
    //
    //     if (id == Guid.Empty)
    //     {
    //         logger.LogWarning("Invalid user ID provided: {Id}", id);
    //         return Result<ApplicationUserResponse>.Error("Invalid user ID provided.");
    //     }
    //     
    //     var userId = ApplicationUserId.Create(id);
    //     
    //     var user = OrganizationDatabase.Users.FirstOrDefault(x => x.Id == userId);
    //     
    //     if (user is null)
    //     {
    //         logger.LogWarning("User with ID {Id} not found in the database.", id);
    //         return Result<ApplicationUserResponse>.NotFound("User not found in the database.");
    //     }
    //
    //     var response = user.ToApplicationUserResponse();
    //     logger.LogInformation("User with ID {Id} retrieved successfully.", id);
    //     return Result<ApplicationUserResponse>.Success(response);
    // }

    async ValueTask<Result> IApplicationUserService.CreateAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken)
    {
        var result = await CreateUserAsync(request, cancellationToken);

        return !result.IsSuccess ? result : Result.Success();

        // var response = user.ToApplicationUserResponse();
        //
        // logger.LogInformation("User {EmailAddress} created successfully with ID {Id}.", request.EmailAddress, user.Id);
        //
        // return Result<ApplicationUserResponse>.Success(response);
    }

    private async ValueTask<Result> CreateUserAsync(
        ApplicationUserRegisterRequest request, 
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling user creation request for: {EmailAddress}", request.EmailAddress);
        
        var userValidation = await ValidateUserCreationRequestAsync(request, cancellationToken);

        if (!userValidation.ValidationSuccess)
            return Result.Error("aan user with this email already exists.");
        
        var emailConfirmationCode = Guid.CreateVersion7().ToString().Replace("-", string.Empty);
        var user = CreateUser(emailConfirmationCode, userValidation.IsFirstUser, request);
        OrganizationDatabase.Users.Add(user);
        
        logger.LogInformation("User {EmailAddress} created successfully with ID {Id}.", request.EmailAddress, user.Id);
        _ = SendUserCreatedEmailAsync(
            $"{request.LastName} {request.FirstName}",
            request.EmailAddress, 
            emailConfirmationCode, 
            cancellationToken);
        
        return Result.Success();
    }

    private ApplicationUser CreateUser(
        string emailConfirmationCode,
        bool isFirstUser,
        ApplicationUserRegisterRequest request)
    {
        var passwordHash = authService.HashPassword(request.Password);
        
        return ApplicationUserBuilder.Create()
            .WithId()
            .WithLastName(request.LastName)
            .WithFirstName(request.FirstName)
            .WithMiddleName(request.MiddleName)
            .WithDateOfBirth(request.DateOfBirth)
            .WithIsMale(request.IsMale)
            .WithSocialSecurityNumber(request.SocialSecurityNumber)
            .WithEmailAddress(request.EmailAddress)
            .WithPasswordHash(passwordHash)
            .WithCreatedDate(timeProvider.GetUtcNow().DateTime)
            .WithEmailAddressIsConfirmed()
            .WithEmailAddressConfirmationCode(emailConfirmationCode)
            .WithAccountIsApproved(isFirstUser)
            .Build();
    }
    
    private async ValueTask<(bool ValidationSuccess, bool IsFirstUser)> ValidateUserCreationRequestAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        logger.LogInformation("Validating user creation request for: {EmailAddress}", request.EmailAddress);
        
        var users = OrganizationDatabase.Users;
        
        var existingUser = users.FirstOrDefault(x => x.EmailAddress == request.EmailAddress);

        if (existingUser is null) return (true, users.Count < 1);
        
        logger.LogWarning("User with email {EmailAddress} already exists.", request.EmailAddress);
        return (false, false);

    }

    private Task SendUserCreatedEmailAsync(
        string fullName,
        string emailAddress,
        string emailConfirmationCode,
        CancellationToken cancellationToken)
    {
        applicationUserEmail.SendAccountConfirmationEmailAsync(fullName, emailAddress, emailConfirmationCode, cancellationToken);
        logger.LogInformation("User {EmailAddress} created and event published.", emailAddress);
        return Task.CompletedTask;
    }

    // async ValueTask<Result<ApplicationUserTokenResponse>> IApplicationUserService.LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken)
    // {
    //     await Task.CompletedTask;
    //     logger.LogInformation("Handling login request for user: {Username}", request.Username);
    //     var user = OrganizationDatabase.Users.FirstOrDefault(x => x.EmailAddress == request.Username);
    //
    //     if (user is null || !BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash, HashType.SHA512))
    //     {
    //         logger.LogWarning("Login failed for user {Username}: Invalid credentials", request.Username);
    //         return Result<ApplicationUserTokenResponse>.NotFound("User name or password is incorrect.");
    //     }
    //     
    //     var token = authService.CreateToken(
    //         user.Id.Value,
    //         user.LastName,
    //         user.FirstName,
    //         user.EmailAddress,
    //         user.IsMale,
    //         user.DateOfBirth);
    //     
    //     var refreshToken = await GenerateAndSaveRefreshTokenAsync(user.Id, cancellationToken);
    //     var response = token.ToApplicationUserTokenResponse(refreshToken, user.Id.Value);
    //     
    //     return Result<ApplicationUserTokenResponse>.Success(response);
    // }
    //
    // private async ValueTask<Result<string>> GenerateAndSaveRefreshTokenAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    // {
    //     await Task.CompletedTask;
    //     logger.LogInformation("Generating new refresh token for user: {UserId}", userId);
    //     var userToUpdate = OrganizationDatabase.Users.FirstOrDefault(x => x.Id == userId);
    //     
    //     if (userToUpdate is null)
    //     {
    //         logger.LogWarning("User not found in the database: {UserId}", userId);
    //         return Result<string>.NotFound("User not found in the database.");
    //     }
    //
    //     OrganizationDatabase.Users.Remove(userToUpdate);
    //     
    //     userToUpdate.RefreshToken = authService.GenerateRefreshToken();
    //     userToUpdate.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
    //     
    //     OrganizationDatabase.Users.Add(userToUpdate);
    //     
    //     logger.LogInformation("User {UserId} updated the refresh token", userId);
    //     return userToUpdate.RefreshToken;
    // }
    //
    // private async ValueTask<ApplicationUser?> ValidateRefreshTokenAsync(string refreshToken, CancellationToken ct)
    // {
    //     await Task.CompletedTask;
    //     
    //     var user = OrganizationDatabase.Users.FirstOrDefault(x => x.RefreshToken == refreshToken);
    //     
    //     if (user is null || 
    //         user.RefreshTokenExpiryTime < timeProvider.GetUtcNow().DateTime)
    //     {
    //         return null;
    //     }
    //     
    //     return user;
    // }
    //
    // async ValueTask<Result<ApplicationUserTokenResponse>> IApplicationUserService.RefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    // {
    //     await Task.CompletedTask;
    //     
    //     var user = await ValidateRefreshTokenAsync(refreshToken, cancellationToken);
    //     
    //     if (user is null)
    //     {
    //         logger.LogWarning("Invalid refresh token");
    //         return Result<ApplicationUserTokenResponse>.NotFound("Invalid refresh token.");
    //     }
    //     
    //     logger.LogInformation("Refreshing token for user: {UserId}", user.Id);
    //     
    //     var newToken = authService.CreateToken(
    //         user.Id.Value,
    //         user.LastName,
    //         user.FirstName,
    //         user.EmailAddress,
    //         user.IsMale,
    //         user.DateOfBirth);
    //     
    //     var newRefreshToken = await GenerateAndSaveRefreshTokenAsync(user.Id, cancellationToken);
    //     var response = newToken.ToApplicationUserTokenResponse(newRefreshToken, user.Id.Value);
    //     logger.LogInformation("Successfully refreshed token for user: {UserId}", user.Id);
    //     
    //     return Result<ApplicationUserTokenResponse>.Success(response);
    // }
}