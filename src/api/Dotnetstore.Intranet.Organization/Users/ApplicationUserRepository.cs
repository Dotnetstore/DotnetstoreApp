using Dotnetstore.Intranet.Organization.Data;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.Intranet.Organization.Users;

internal sealed class ApplicationUserRepository(OrganizationDataContext context) : GenericRepository<ApplicationUser>(context), IApplicationUserRepository
{
    async ValueTask<IEnumerable<ApplicationUser>> IApplicationUserRepository.GetAllNotDeletedAsync(CancellationToken cancellationToken)
    {
        var query = GetQuery();
        
        return await query
            .Where(x => !x.IsDeleted)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    async ValueTask<ApplicationUser?> IApplicationUserRepository.GetValidUserByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var query = GetQuery();

        return await query
            .Where(x => x.EmailAddress == username)
            .Where(x => !x.IsDeleted)
            .Where(x => x.EmailAddressIsConfirmed)
            .Where(x => x.AccountIsApproved)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    async ValueTask<ApplicationUser?> IApplicationUserRepository.GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        var query = GetQuery();
        
        return await query
            .FirstOrDefaultAsync(x => x.EmailAddress == username, cancellationToken)
            .ConfigureAwait(false);
    }

    async ValueTask<ApplicationUser?> IApplicationUserRepository.GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var query = GetQuery();

        return await query
            .Where(x => x.RefreshToken == refreshToken)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    async ValueTask<ApplicationUser?> IApplicationUserRepository.GetByEmailConfirmationCodeAsync(string emailConfirmationCode, CancellationToken cancellationToken)
    {
        var query = GetQuery();

        return await query
            .Where(x => x.EmailAddressConfirmationCode == emailConfirmationCode)
            .Where(x => !x.IsDeleted)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);
    }

    private IQueryable<ApplicationUser> GetQuery()
    {
        return context
            .Set<ApplicationUser>()
            .AsNoTracking()
            .Include(x => x.ApplicationUserInRoles)
            .ThenInclude(x => x.Role)
            .OrderBy(x => x.LastName)
            .ThenBy(x => x.FirstName);
    }
}