using Ardalis.Result;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Responses.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Users;

internal interface IApplicationUserService
{
    ValueTask<IEnumerable<ApplicationUserResponse>> GetAllAsync(CancellationToken cancellationToken);
    
    ValueTask<Result<ApplicationUserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    ValueTask<Result<ApplicationUserResponse>> CreateAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken);
    
    ValueTask<Result<ApplicationUserTokenResponse>> LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken);

    ValueTask<Result<ApplicationUserTokenResponse>> RefreshTokenAsync(ApplicationUserRefreshTokenRequest request,
        CancellationToken cancellationToken);
}