using Ardalis.GuardClauses;
using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.Organization.Users;
using Dotnetstore.Intranet.SharedKernel.Services;

namespace Dotnetstore.Intranet.Organization.UserInRoles;

internal sealed class ApplicationUserInRoleBuilder:
    BaseInformationBuilder<ApplicationUserInRoleBuilder>,
    ICreateId,
    ICreateUserId,
    ICreateRoleId
{
    private Guid _id;
    private Guid _userId;
    private Guid _roleId;

    private ApplicationUserInRoleBuilder()
    {
    }

    internal static ICreateId Create()
    {
        return new ApplicationUserInRoleBuilder();
    }

    ICreateUserId ICreateId.WithId()
    {
        _id = Guid.CreateVersion7();
        return this;
    }

    ICreateUserId ICreateId.WithId(Guid id)
    {
        var newId = Guard.Against.Default(id, nameof(id));
        _id = newId;
        return this;
    }

    ICreateRoleId ICreateUserId.WithUserId(Guid userId)
    {
        var newUserId = Guard.Against.Default(userId, nameof(userId));
        _userId = newUserId;
        return this;
    }

    ApplicationUserInRoleBuilder ICreateRoleId.WithRoleId(Guid roleId)
    {
        var newRoleId = Guard.Against.Default(roleId, nameof(roleId));
        _roleId = newRoleId;
        return this;
    }

    public ApplicationUserInRole Build()
    {
        return ApplicationUserInRole.Create(
            ApplicationUserInRoleId.Create(_id),
            ApplicationUserRoleId.Create(_roleId),
            ApplicationUserId.Create(_userId),
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

internal interface ICreateId
{
    ICreateUserId WithId();
    ICreateUserId WithId(Guid id);
}

internal interface ICreateUserId
{
    ICreateRoleId WithUserId(Guid userId);
}

internal interface ICreateRoleId
{
    ApplicationUserInRoleBuilder WithRoleId(Guid roleId);
}