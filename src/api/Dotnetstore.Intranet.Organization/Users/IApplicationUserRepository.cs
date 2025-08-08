namespace Dotnetstore.Intranet.Organization.Users;

internal interface IApplicationUserRepository : IGenericRepository<ApplicationUser>
{
    ValueTask<IEnumerable<ApplicationUser>> GetAllNotDeletedAsync(CancellationToken cancellationToken = default);
    
    ValueTask<ApplicationUser?> GetValidUserByUsernameAsync(string username, CancellationToken cancellationToken = default);
    
    ValueTask<ApplicationUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default);
    
    ValueTask<ApplicationUser?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    
    ValueTask<ApplicationUser?> GetByEmailConfirmationCodeAsync(string emailConfirmationCode, CancellationToken cancellationToken = default);
}