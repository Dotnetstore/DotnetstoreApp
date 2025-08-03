using Ardalis.GuardClauses;
using Dotnetstore.Intranet.SDK.Services;

namespace Dotnetstore.Intranet.Organization.Roles;

internal sealed class ApplicationUserRoleBuilder :
    ICreateApplicationUserRoleId,
    ICreateName,
    ICreateDescription,
    IBaseInformation
{
    private Guid _id;
    private string _name = string.Empty;
    private string _description = string.Empty;
    private DateTime _createdDate = DateTime.UtcNow;
    private Guid? _createdBy;
    private Guid? _lastUpdatedBy;
    private DateTime? _lastUpdatedDate;
    private bool _isDeleted;
    private Guid? _deletedBy;
    private DateTime? _deletedDate;
    private bool _isSystem;
    private bool _isGdpr;

    private ApplicationUserRoleBuilder()
    {
    }

    internal static ICreateApplicationUserRoleId Create()
    {
        return new ApplicationUserRoleBuilder();
    }
    
    ICreateName ICreateApplicationUserRoleId.WithId()
    {
        _id = Guid.CreateVersion7();
        return this;
    }

    ICreateName ICreateApplicationUserRoleId.WithId(Guid id)
    {
        var newId = Guard.Against.Default(id, nameof(id));
        _id = newId;
        return this;
    }

    ICreateDescription ICreateName.WithName(string name)
    {
        var newName = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        newName = Guard.Against.StringTooLong(newName, DataSchemeConstants.UserRoleNameMaxLength, nameof(name));
        _name = newName;
        return this;
    }

    IBaseInformation ICreateDescription.WithDescription(string description)
    {
        var newDescription = Guard.Against.NullOrWhiteSpace(description, nameof(description));
        newDescription = Guard.Against.StringTooLong(newDescription, DataSchemeConstants.UserRoleNameDescriptionLength, nameof(description));
        _description = newDescription;
        return this;
    }

    IBaseInformation IBaseInformation.WithCreatedDate(DateTime createdDate)
    {
        var newCreatedDate = Guard.Against.Default(createdDate, nameof(createdDate));
        _createdDate = newCreatedDate;
        return this;
    }

    IBaseInformation IBaseInformation.WithCreatedBy(Guid? createdBy)
    {
        if (createdBy.HasValue && createdBy.Value == Guid.Empty)
        {
            throw new ArgumentException("CreatedBy cannot be an empty GUID.", nameof(createdBy));
        }
        
        _createdBy = createdBy;
        return this;
    }

    IBaseInformation IBaseInformation.WithLastUpdatedBy(Guid? lastUpdatedBy)
    {
        if (lastUpdatedBy.HasValue && lastUpdatedBy.Value == Guid.Empty)
        {
            throw new ArgumentException("LastUpdatedBy cannot be an empty GUID.", nameof(lastUpdatedBy));
        }
        
        _lastUpdatedBy = lastUpdatedBy;
        return this;
    }

    IBaseInformation IBaseInformation.WithLastUpdatedDate(DateTime? lastUpdatedDate)
    {
        if (lastUpdatedDate.HasValue && lastUpdatedDate.Value == default)
        {
            throw new ArgumentException("LastUpdatedDate cannot be default DateTime.", nameof(lastUpdatedDate));
        }
        
        _lastUpdatedDate = lastUpdatedDate;
        return this;
    }

    IBaseInformation IBaseInformation.WithIsDeleted(bool isDeleted)
    {
        _isDeleted = isDeleted;
        return this;
    }

    IBaseInformation IBaseInformation.WithDeletedBy(Guid? deletedBy)
    {
        if (deletedBy.HasValue && deletedBy.Value == Guid.Empty)
        {
            throw new ArgumentException("DeletedBy cannot be an empty GUID.", nameof(deletedBy));
        }
        
        _deletedBy = deletedBy;
        return this;
    }

    IBaseInformation IBaseInformation.WithDeletedDate(DateTime? deletedDate)
    {
        if (deletedDate.HasValue && deletedDate.Value == default)
        {
            throw new ArgumentException("DeletedDate cannot be default DateTime.", nameof(deletedDate));
        }
        
        _deletedDate = deletedDate;
        return this;
    }

    IBaseInformation IBaseInformation.WithIsSystem(bool isSystem)
    {
        _isSystem = isSystem;
        return this;
    }

    IBaseInformation IBaseInformation.WithIsGdpr(bool isGdpr)
    {
        _isGdpr = isGdpr;
        return this;
    }

    ApplicationUserRole IBaseInformation.Build()
    {
        return ApplicationUserRole.Create(
            ApplicationUserRoleId.Create(_id), 
            _name,
            _description,
            _createdDate,
            _createdBy,
            _lastUpdatedBy,
            _lastUpdatedDate,
            _isDeleted,
            _deletedBy,
            _deletedDate,
            _isSystem,
            _isGdpr);
    }
}

internal interface ICreateApplicationUserRoleId
{
    ICreateName WithId();
    ICreateName WithId(Guid id);
}

internal interface ICreateName
{
    ICreateDescription WithName(string name);
}

internal interface ICreateDescription
{
    IBaseInformation WithDescription(string description);
}

internal interface IBaseInformation
{
    IBaseInformation WithCreatedDate(DateTime createdDate);
    IBaseInformation WithCreatedBy(Guid? createdBy = null);
    IBaseInformation WithLastUpdatedBy(Guid? lastUpdatedBy = null);
    IBaseInformation WithLastUpdatedDate(DateTime? lastUpdatedDate = null);
    IBaseInformation WithIsDeleted(bool isDeleted = false);
    IBaseInformation WithDeletedBy(Guid? deletedBy = null);
    IBaseInformation WithDeletedDate(DateTime? deletedDate = null);
    IBaseInformation WithIsSystem(bool isSystem = false);
    IBaseInformation WithIsGdpr(bool isGdpr = false);
    ApplicationUserRole Build();
}