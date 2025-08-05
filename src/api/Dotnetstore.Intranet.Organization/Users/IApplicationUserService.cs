using Ardalis.Result;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Users;

internal interface IApplicationUserService
{
    // ValueTask<IEnumerable<ApplicationUserResponse>> GetAllAsync(CancellationToken cancellationToken);
    ValueTask<IEnumerable<ApplicationUser>> GetAllNotDeletedAsync(CancellationToken cancellationToken);
    //
    // ValueTask<Result<ApplicationUserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    ValueTask<Result> CreateAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken);
    
    ValueTask<Result> SetApproveStatusAsync(ApplicationUserId userId, bool isApproved, CancellationToken cancellationToken);
    
    // ValueTask<Result<ApplicationUserTokenResponse>> LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken);
    //
    // ValueTask<Result<ApplicationUserTokenResponse>> RefreshTokenAsync(string refreshToken,
    //     CancellationToken cancellationToken = default);
}