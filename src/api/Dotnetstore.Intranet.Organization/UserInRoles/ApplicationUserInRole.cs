using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.Users;
using Dotnetstore.Intranet.SharedKernel.Models;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

public sealed class ApplicationUserInRole : AggregateRoot<ApplicationUserInRoleId>
{
    public ApplicationUserRoleId ApplicationUserRoleId { get; init; }
    
    public ApplicationUserId ApplicationUserId { get; init; }
    
    public ApplicationUserRole Role { get; init; } = null!;
    
    public ApplicationUser User { get; init; } = null!;
    
    private ApplicationUserInRole(
        ApplicationUserInRoleId id,
        ApplicationUserRoleId applicationUserRoleId,
        ApplicationUserId applicationUserId,
        DateTime createdDate,
        Guid? createdBy = null,
        Guid? lastUpdatedBy = null,
        DateTime? lastUpdatedDate = null,
        bool isDeleted = false,
        Guid? deletedBy = null,
        DateTime? deletedDate = null,
        bool isSystem = false,
        bool isGdpr = false) : base(id)
    {
        ApplicationUserRoleId = applicationUserRoleId;
        ApplicationUserId = applicationUserId;
        CreatedDate = createdDate;
        CreatedBy = createdBy;
        LastUpdatedBy = lastUpdatedBy;
        LastUpdatedDate = lastUpdatedDate;
        IsDeleted = isDeleted;
        DeletedBy = deletedBy;
        DeletedDate = deletedDate;
        IsSystem = isSystem;
        IsGdpr = isGdpr;
    }
    
    internal static ApplicationUserInRole Create(
        ApplicationUserInRoleId id,
        ApplicationUserRoleId applicationUserRoleId,
        ApplicationUserId applicationUserId,
        DateTime createdDate,
        Guid? createdBy = null,
        Guid? lastUpdatedBy = null,
        DateTime? lastUpdatedDate = null,
        bool isDeleted = false,
        Guid? deletedBy = null,
        DateTime? deletedDate = null,
        bool isSystem = false,
        bool isGdpr = false)
    {
        return new ApplicationUserInRole(
            id, 
            applicationUserRoleId, 
            applicationUserId, 
            createdDate, 
            createdBy, 
            lastUpdatedBy, 
            lastUpdatedDate, 
            isDeleted, 
            deletedBy, 
            deletedDate, 
            isSystem, 
            isGdpr);
    }
    
    internal static ApplicationUserInRole Create(
        ApplicationUserRoleId applicationUserRoleId,
        ApplicationUserId applicationUserId,
        DateTime createdDate,
        Guid? createdBy = null,
        Guid? lastUpdatedBy = null,
        DateTime? lastUpdatedDate = null,
        bool isDeleted = false,
        Guid? deletedBy = null,
        DateTime? deletedDate = null,
        bool isSystem = false,
        bool isGdpr = false)
    {
        return new ApplicationUserInRole(
            ApplicationUserInRoleId.Create(),
            applicationUserRoleId, 
            applicationUserId, 
            createdDate, 
            createdBy, 
            lastUpdatedBy, 
            lastUpdatedDate, 
            isDeleted, 
            deletedBy, 
            deletedDate, 
            isSystem, 
            isGdpr);
    }
}