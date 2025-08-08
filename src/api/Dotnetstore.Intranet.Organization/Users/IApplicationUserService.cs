namespace Dotnetstore.Intranet.Organization.Users;

internal interface IApplicationUserService
{
    ValueTask<IEnumerable<ApplicationUser>> GetAllNotDeletedAsync(CancellationToken cancellationToken);
    
    ValueTask<Result> CreateAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken);
    
    ValueTask<Result> ConfirmUsersEmailAddressAsync(string confirmationCode, CancellationToken cancellationToken);
    
    ValueTask<Result> SetApproveStatusAsync(ApplicationUserId userId, bool isApproved, CancellationToken cancellationToken);
    
    ValueTask<Result<(string Token, string RefreshToken)>> LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken);
}