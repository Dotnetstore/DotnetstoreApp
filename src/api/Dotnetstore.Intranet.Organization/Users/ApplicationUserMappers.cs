using Dotnetstore.Intranet.SDK.Responses.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Users;

internal static class ApplicationUserMappers
{
    internal static ApplicationUserTokenResponse ToApplicationUserTokenResponse(this string token,
        string refreshToken, Guid userId) =>
        new(userId, token, refreshToken);
    
    internal static ApplicationUserResponse ToApplicationUserResponse(this ApplicationUser user) =>
        new(
            user.Id.Value,
            user.LastName,
            user.FirstName,
            user.MiddleName,
            user.DateOfBirth,
            user.IsMale,
            user.SocialSecurityNumber,
            user.EmailAddress);
}