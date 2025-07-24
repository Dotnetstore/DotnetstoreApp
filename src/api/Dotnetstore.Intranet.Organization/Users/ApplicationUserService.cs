using Ardalis.Result;
using BCrypt.Net;
using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Responses.Organization.Users;
using Dotnetstore.Intranet.SharedKernel.Services;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.Intranet.Organization.Users;

internal sealed class ApplicationUserService(
    IAuthService authService,
    TimeProvider timeProvider,
    ILogger<ApplicationUserService> logger) : IApplicationUserService
{
    async ValueTask<IEnumerable<ApplicationUserResponse>> IApplicationUserService.GetAllAsync(CancellationToken cancellationToken)  
    {
        await Task.CompletedTask;
        var users = OrganizationDatabase.Users
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName)
            .ToList();
        
        return users.Select(x => x.ToApplicationUserResponse());
    }

    async ValueTask<Result<ApplicationUserResponse>> IApplicationUserService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        if (id == Guid.Empty)
        {
            logger.LogWarning("Invalid user ID provided: {Id}", id);
            return Result<ApplicationUserResponse>.Error("Invalid user ID provided.");
        }
        
        var userId = ApplicationUserId.Create(id);
        
        var user = OrganizationDatabase.Users.FirstOrDefault(x => x.Id == userId);
        
        if (user is null)
        {
            logger.LogWarning("User with ID {Id} not found in the database.", id);
            return Result<ApplicationUserResponse>.NotFound("User not found in the database.");
        }

        var response = user.ToApplicationUserResponse();
        logger.LogInformation("User with ID {Id} retrieved successfully.", id);
        return Result<ApplicationUserResponse>.Success(response);
    }

    async ValueTask<Result<ApplicationUserResponse>> IApplicationUserService.CreateAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        logger.LogInformation("Handling user creation request for: {EmailAddress}", request.EmailAddress);
        
        var passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(request.Password, HashType.SHA512, 19);
        var user = ApplicationUserBuilder.Create()
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
            .Build();
        
        OrganizationDatabase.Users.Add(user);
        var response = user.ToApplicationUserResponse();
        
        logger.LogInformation("User {EmailAddress} created successfully with ID {Id}.", request.EmailAddress, user.Id);
        
        return Result<ApplicationUserResponse>.Success(response);
    }

    async ValueTask<Result<ApplicationUserTokenResponse>> IApplicationUserService.LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        logger.LogInformation("Handling login request for user: {Username}", request.Username);
        var user = OrganizationDatabase.Users.FirstOrDefault(x => x.EmailAddress == request.Username);

        if (user is null || !BCrypt.Net.BCrypt.EnhancedVerify(request.Password, user.PasswordHash, HashType.SHA512))
        {
            logger.LogWarning("Login failed for user {Username}: Invalid credentials", request.Username);
            return Result<ApplicationUserTokenResponse>.NotFound("User name or password is incorrect.");
        }
        
        var token = authService.CreateToken(
            user.Id.Value,
            user.LastName,
            user.FirstName,
            user.EmailAddress,
            user.IsMale,
            user.DateOfBirth);
        
        var refreshToken = await GenerateAndSaveRefreshTokenAsync(user.Id, cancellationToken);
        var response = token.ToApplicationUserTokenResponse(refreshToken, user.Id.Value);
        
        return Result<ApplicationUserTokenResponse>.Success(response);
    }
    
    private async ValueTask<Result<string>> GenerateAndSaveRefreshTokenAsync(ApplicationUserId userId, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        logger.LogInformation("Generating new refresh token for user: {UserId}", userId);
        var userToUpdate = OrganizationDatabase.Users.FirstOrDefault(x => x.Id == userId);
        
        if (userToUpdate is null)
        {
            logger.LogWarning("User not found in the database: {UserId}", userId);
            return Result<string>.NotFound("User not found in the database.");
        }

        OrganizationDatabase.Users.Remove(userToUpdate);
        
        userToUpdate.RefreshToken = authService.GenerateRefreshToken();
        userToUpdate.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        OrganizationDatabase.Users.Add(userToUpdate);
        
        logger.LogInformation("User {UserId} updated the refresh token", userId);
        return userToUpdate.RefreshToken;
    }
    
    private async ValueTask<ApplicationUser?> ValidateRefreshTokenAsync(ApplicationUserId userId, string refreshToken, CancellationToken ct)
    {
        await Task.CompletedTask;
        
        var user = OrganizationDatabase.Users.FirstOrDefault(x => x.Id == userId);
        
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime < timeProvider.GetUtcNow().DateTime)
        {
            return null;
        }
        
        return user;
    }
    
    async ValueTask<Result<ApplicationUserTokenResponse>> IApplicationUserService.RefreshTokenAsync(ApplicationUserRefreshTokenRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        logger.LogInformation("Refreshing token for user: {UserId}", request.UserId);
        
        var user = await ValidateRefreshTokenAsync(ApplicationUserId.Create(request.UserId), request.RefreshToken, cancellationToken);
        
        if (user is null)
        {
            logger.LogWarning("Invalid refresh token for user: {UserId}", request.UserId);
            return Result<ApplicationUserTokenResponse>.NotFound("Invalid refresh token.");
        }
        
        var newToken = authService.CreateToken(
            user.Id.Value,
            user.LastName,
            user.FirstName,
            user.EmailAddress,
            user.IsMale,
            user.DateOfBirth);
        
        var newRefreshToken = await GenerateAndSaveRefreshTokenAsync(user.Id, cancellationToken);
        
        var response = newToken.ToApplicationUserTokenResponse(newRefreshToken, user.Id.Value);
        
        logger.LogInformation("Successfully refreshed token for user: {UserId}", request.UserId);
        
        return Result<ApplicationUserTokenResponse>.Success(response);
    }
}