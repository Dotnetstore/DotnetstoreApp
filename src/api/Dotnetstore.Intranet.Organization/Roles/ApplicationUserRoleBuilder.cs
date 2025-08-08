using Ardalis.GuardClauses;

namespace Dotnetstore.Intranet.Organization.Roles;

internal sealed class ApplicationUserRoleBuilder :
    BaseInformationBuilder<ApplicationUserRoleBuilder>, 
    ICreateApplicationUserRoleId, 
    ICreateName, 
    ICreateDescription
{
    private Guid _id;
    private string _name = string.Empty;
    private string _description = string.Empty;

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
    
    ApplicationUserRoleBuilder ICreateDescription.WithDescription(string description)
    {
        var newDescription = Guard.Against.NullOrWhiteSpace(description, nameof(description));
        newDescription = Guard.Against.StringTooLong(newDescription, DataSchemeConstants.UserRoleDescriptionLength, nameof(description));
        _description = newDescription;
        return this;
    }

    public ApplicationUserRole Build()
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
    ApplicationUserRoleBuilder WithDescription(string description);
}