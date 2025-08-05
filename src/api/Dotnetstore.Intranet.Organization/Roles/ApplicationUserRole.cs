using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.SharedKernel.Models;

namespace Dotnetstore.Intranet.Organization.Roles;

internal sealed class ApplicationUserRole : AggregateRoot<ApplicationUserRoleId>
{
    public string Name { get; init; }
    public string Description { get; init; }

    public ICollection<ApplicationUserInRole> ApplicationUserInRoles { get; init; } = new List<ApplicationUserInRole>();
    
    private ApplicationUserRole(
        ApplicationUserRoleId id,
        string name,
        string description,
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
        Name = name;
        Description = description;
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
    
    internal static ApplicationUserRole Create(
        ApplicationUserRoleId id,
        string name,
        string description,
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
        return new ApplicationUserRole(
            id, 
            name, 
            description, 
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
    
    internal static ApplicationUserRole Create(
        string name,
        string description,
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
        return new ApplicationUserRole(
            ApplicationUserRoleId.Create(),
            name, 
            description, 
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