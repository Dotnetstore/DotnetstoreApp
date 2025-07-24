using Dotnetstore.Intranet.SharedKernel.Models;

namespace Dotnetstore.Intranet.Organization.Users;

public class ApplicationUser : PersonIdentity<ApplicationUserId>
{
    private ApplicationUser(
        ApplicationUserId id,
        string lastName,
        string firstName,
        string? middleName,
        DateTime dateOfBirth,
        bool isMale,
        string? socialSecurityNumber,
        string emailAddress,
        string passwordHash,
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
        LastName = lastName;
        FirstName = firstName;
        MiddleName = middleName;
        DateOfBirth = dateOfBirth;
        IsMale = isMale;
        SocialSecurityNumber = socialSecurityNumber;
        EmailAddress = emailAddress;
        PasswordHash = passwordHash;
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
    
    internal static ApplicationUser Create(
        ApplicationUserId id,
        string lastName,
        string firstName,
        string? middleName,
        DateTime dateOfBirth,
        bool isMale,
        string? socialSecurityNumber,
        string emailAddress,
        string passwordHash,
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
        return new ApplicationUser(
            id,
            lastName,
            firstName,
            middleName,
            dateOfBirth,
            isMale,
            socialSecurityNumber,
            emailAddress,
            passwordHash,
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
    
    internal static ApplicationUser Create(
        string lastName,
        string firstName,
        string? middleName,
        DateTime dateOfBirth,
        bool isMale,
        string? socialSecurityNumber,
        string emailAddress,
        string passwordHash,
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
        return new ApplicationUser(
            ApplicationUserId.Create(),
            lastName,
            firstName,
            middleName,
            dateOfBirth,
            isMale,
            socialSecurityNumber,
            emailAddress,
            passwordHash,
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