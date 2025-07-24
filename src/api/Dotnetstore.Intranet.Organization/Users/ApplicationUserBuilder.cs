using Ardalis.GuardClauses;
using Dotnetstore.Intranet.SDK.Services;

namespace Dotnetstore.Intranet.Organization.Users;

internal sealed class ApplicationUserBuilder : 
    ICreateApplicationUserId, 
    ICreateLastName, 
    ICreateFirstName, 
    ICreateMiddleName, 
    ICreateDateOfBirth, 
    ICreateIsMale, 
    ICreateSocialSecurityNumber, 
    ICreateEmailAddress, 
    ICreatePasswordHash, 
    IBaseInformation
{
    private Guid _id;
    private string _lastName = string.Empty;
    private string _firstName = string.Empty;
    private string? _middleName;
    private DateTime _dateOfBirth;
    private bool _isMale;
    private string? _socialSecurityNumber;
    private string _emailAddress = string.Empty;
    private string _passwordHash = string.Empty;
    private DateTime _createdDate = DateTime.UtcNow;
    private Guid? _createdBy;
    private Guid? _lastUpdatedBy;
    private DateTime? _lastUpdatedDate;
    private bool _isDeleted;
    private Guid? _deletedBy;
    private DateTime? _deletedDate;
    private bool _isSystem;
    private bool _isGdpr;

    private ApplicationUserBuilder()
    {
    }

    internal static ICreateApplicationUserId Create()
    {
        return new ApplicationUserBuilder();
    }
    
    ICreateLastName ICreateApplicationUserId.WithId()
    {
        _id = Guid.CreateVersion7();
        return this;
    }

    ICreateLastName ICreateApplicationUserId.WithId(Guid id)
    {
        Guard.Against.Default(id, nameof(id), "Id cannot be default value.");
        _id = id;
        return this;
    }

    ICreateFirstName ICreateLastName.WithLastName(string lastName)
    {
        Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName), "Last name cannot be null or empty.");
        Guard.Against.StringTooLong(lastName, DataSchemeConstants.UserLastNameMaxLength, nameof(lastName), $"Last name cannot be longer than {DataSchemeConstants.UserLastNameMaxLength} characters.");
        _lastName = lastName;
        return this;
    }

    ICreateMiddleName ICreateFirstName.WithFirstName(string firstName)
    {
        Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName), "First name cannot be null or empty.");
        Guard.Against.StringTooLong(firstName, DataSchemeConstants.UserFirstNameMaxLength, nameof(firstName), $"First name cannot be longer than {DataSchemeConstants.UserFirstNameMaxLength} characters.");
        _firstName = firstName;
        return this;
    }

    ICreateDateOfBirth ICreateMiddleName.WithMiddleName(string? middleName)
    {
        if (!string.IsNullOrEmpty(middleName))
            Guard.Against.StringTooLong(middleName, DataSchemeConstants.UserMiddleNameMaxLength, nameof(middleName),
                $"Middle name cannot be longer than {DataSchemeConstants.UserMiddleNameMaxLength} characters.");
        _middleName = middleName;
        return this;
    }

    ICreateIsMale ICreateDateOfBirth.WithDateOfBirth(DateTime dateOfBirth)
    {
        Guard.Against.OutOfRange(dateOfBirth, nameof(dateOfBirth), DateTime.MinValue, DateTime.MaxValue, "Date of birth must be a valid date.");
        Guard.Against.OutOfRange(dateOfBirth, nameof(dateOfBirth), DateTime.Now.AddYears(DataSchemeConstants.UserDateOfBirthMin), DateTime.Now.AddYears(DataSchemeConstants.UserDateOfBirthMax), "Date of birth must be between 70 years ago and 15 years ago from today.");
        _dateOfBirth = dateOfBirth;
        return this;
    }

    ICreateSocialSecurityNumber ICreateIsMale.WithIsMale(bool isMale)
    {
        _isMale = isMale;
        return this;
    }

    ICreateEmailAddress ICreateSocialSecurityNumber.WithSocialSecurityNumber(string? socialSecurityNumber)
    {
        if (!string.IsNullOrEmpty(socialSecurityNumber))
        {
            Guard.Against.StringTooLong(socialSecurityNumber, DataSchemeConstants.UserSocialSecurityNumberMaxLength, nameof(socialSecurityNumber),
                $"Social security number cannot be longer than {DataSchemeConstants.UserSocialSecurityNumberMaxLength} characters.");
        }
        _socialSecurityNumber = socialSecurityNumber;
        return this;
    }

    ICreatePasswordHash ICreateEmailAddress.WithEmailAddress(string emailAddress)
    {
        Guard.Against.NullOrWhiteSpace(emailAddress, nameof(emailAddress), "Email address cannot be null or empty.");
        Guard.Against.StringTooLong(emailAddress, DataSchemeConstants.UserEmailMaxLength, nameof(emailAddress), $"Email address cannot be longer than {DataSchemeConstants.UserEmailMaxLength} characters.");
        _emailAddress = emailAddress;
        return this;
    }

    IBaseInformation ICreatePasswordHash.WithPasswordHash(string passwordHash)
    {
        Guard.Against.NullOrWhiteSpace(passwordHash, nameof(passwordHash), "Password hash cannot be null or empty.");
        Guard.Against.StringTooLong(passwordHash, DataSchemeConstants.UserPasswordMaxLength, nameof(passwordHash), $"Password hash cannot be longer than {DataSchemeConstants.UserPasswordMaxLength} characters.");
        _passwordHash = passwordHash;
        return this;
    }

    IBaseInformation IBaseInformation.WithCreatedDate(DateTime createdDate)
    {
        Guard.Against.OutOfRange(createdDate, nameof(createdDate), DateTime.MinValue, DateTime.MaxValue, "Created date must be a valid date.");
        _createdDate = createdDate;
        return this;
    }

    IBaseInformation IBaseInformation.WithCreatedBy(Guid? createdBy)
    {
        if (createdBy.HasValue)
        {
            Guard.Against.Default(createdBy, nameof(createdBy), "Created by cannot be empty.");
        }
        _createdBy = createdBy;
        return this;
    }

    IBaseInformation IBaseInformation.WithLastUpdatedBy(Guid? lastUpdatedBy)
    {
        if (lastUpdatedBy.HasValue)
        {
            Guard.Against.Default(lastUpdatedBy, nameof(lastUpdatedBy), "Last updated by cannot be empty.");
        }
        _lastUpdatedBy = lastUpdatedBy;
        return this;
    }

    IBaseInformation IBaseInformation.WithLastUpdatedDate(DateTime? lastUpdatedDate)
    {
        if (lastUpdatedDate.HasValue)
        {
            Guard.Against.OutOfRange(lastUpdatedDate.Value, nameof(lastUpdatedDate), DateTime.MinValue, DateTime.MaxValue, "Last updated date must be a valid date.");
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
        if (deletedBy.HasValue)
        {
            Guard.Against.Default(deletedBy, nameof(deletedBy), "Deleted by cannot be empty.");
        }
        _deletedBy = deletedBy;
        return this;
    }

    IBaseInformation IBaseInformation.WithDeletedDate(DateTime? deletedDate)
    {
        if (deletedDate.HasValue)
        {
            Guard.Against.OutOfRange(deletedDate.Value, nameof(deletedDate), DateTime.MinValue, DateTime.MaxValue, "Deleted date must be a valid date.");
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

    ApplicationUser IBaseInformation.Build()
    {
        return ApplicationUser.Create(
            ApplicationUserId.Create(_id), 
            _lastName,
            _firstName,
            _middleName,
            _dateOfBirth,
            _isMale,
            _socialSecurityNumber,
            _emailAddress,
            _passwordHash,
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

internal interface ICreateApplicationUserId
{
    ICreateLastName WithId();
    ICreateLastName WithId(Guid id);
}

internal interface ICreateLastName
{
    ICreateFirstName WithLastName(string lastName);
}

internal interface ICreateFirstName
{
    ICreateMiddleName WithFirstName(string firstName);
}

internal interface ICreateMiddleName
{
    ICreateDateOfBirth WithMiddleName(string? middleName = null);
}

internal interface ICreateDateOfBirth
{
    ICreateIsMale WithDateOfBirth(DateTime dateOfBirth);
}

internal interface ICreateIsMale
{
    ICreateSocialSecurityNumber WithIsMale(bool isMale);
}

internal interface ICreateSocialSecurityNumber
{
    ICreateEmailAddress WithSocialSecurityNumber(string? socialSecurityNumber = null);
}

internal interface ICreateEmailAddress
{
    ICreatePasswordHash WithEmailAddress(string emailAddress);
}

internal interface ICreatePasswordHash
{
    IBaseInformation WithPasswordHash(string passwordHash);
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
    ApplicationUser Build();
}