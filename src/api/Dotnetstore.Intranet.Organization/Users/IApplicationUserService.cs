using Ardalis.Result;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;

namespace Dotnetstore.Intranet.Organization.Users;

public interface IApplicationUserService
{
    // ValueTask<IEnumerable<ApplicationUserResponse>> GetAllAsync(CancellationToken cancellationToken);
    //
    // ValueTask<Result<ApplicationUserResponse>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    ValueTask<Result> CreateAsync(ApplicationUserRegisterRequest request, CancellationToken cancellationToken);
    
    // ValueTask<Result<ApplicationUserTokenResponse>> LoginAsync(ApplicationUserLoginRequest request, CancellationToken cancellationToken);
    //
    // ValueTask<Result<ApplicationUserTokenResponse>> RefreshTokenAsync(string refreshToken,
    //     CancellationToken cancellationToken = default);
}