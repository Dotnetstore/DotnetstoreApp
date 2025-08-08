using Ardalis.GuardClauses;

namespace Dotnetstore.Intranet.Organization.Users;

internal sealed class ApplicationUserBuilder : 
    BaseInformationBuilder<ApplicationUserBuilder>,
    ICreateApplicationUserId, 
    ICreateLastName, 
    ICreateFirstName, 
    ICreateMiddleName, 
    ICreateDateOfBirth, 
    ICreateIsMale, 
    ICreateSocialSecurityNumber, 
    ICreateEmailAddress, 
    ICreatePasswordHash,
    ICreateEmailAddressIsConfirmed,
    ICreateEmailAddressConfirmationCode,
    ICreateAccountIsApproved
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
    private bool _emailAddressIsConfirmed;
    private string? _emailAddressConfirmationCode;
    private bool _accountIsApproved;

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
        Guard.Against.OutOfRange(dateOfBirth, nameof(dateOfBirth), DateTime.UtcNow.AddYears(DataSchemeConstants.UserDateOfBirthMin), DateTime.UtcNow.AddYears(DataSchemeConstants.UserDateOfBirthMax), "Date of birth must be between 70 years ago and 15 years ago from today.");
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

    ICreateEmailAddressIsConfirmed ICreatePasswordHash.WithPasswordHash(string passwordHash)
    {
        Guard.Against.NullOrWhiteSpace(passwordHash, nameof(passwordHash), "Password hash cannot be null or empty.");
        Guard.Against.StringTooLong(passwordHash, DataSchemeConstants.UserPasswordMaxLength, nameof(passwordHash), $"Password hash cannot be longer than {DataSchemeConstants.UserPasswordMaxLength} characters.");
        _passwordHash = passwordHash;
        return this;
    }

    ICreateEmailAddressConfirmationCode ICreateEmailAddressIsConfirmed.WithEmailAddressIsConfirmed(bool emailAddressIsConfirmed)
    {
        _emailAddressIsConfirmed = emailAddressIsConfirmed;
        return this;
    }

    ICreateAccountIsApproved ICreateEmailAddressConfirmationCode.WithEmailAddressConfirmationCode(string? emailAddressConfirmationCode)
    {
        if (!string.IsNullOrEmpty(emailAddressConfirmationCode))
        {
            Guard.Against.StringTooLong(emailAddressConfirmationCode, DataSchemeConstants.UserEmailConfirmationCodeMaxLength, nameof(emailAddressConfirmationCode),
                $"Email address confirmation code cannot be longer than {DataSchemeConstants.UserEmailConfirmationCodeMaxLength} characters.");
        }
        _emailAddressConfirmationCode = emailAddressConfirmationCode;
        return this;
    }

    ApplicationUserBuilder ICreateAccountIsApproved.WithAccountIsApproved(bool accountIsApproved)
    {
        _accountIsApproved = accountIsApproved;
        return this;
    }

    public ApplicationUser Build()
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
            _isGdpr,
            _emailAddressIsConfirmed,
            _emailAddressConfirmationCode,
            _accountIsApproved);
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
    ICreateEmailAddressIsConfirmed WithPasswordHash(string passwordHash);
}

internal interface ICreateEmailAddressIsConfirmed
{
    ICreateEmailAddressConfirmationCode WithEmailAddressIsConfirmed(bool emailAddressIsConfirmed = false);
}

internal interface ICreateEmailAddressConfirmationCode
{
    ICreateAccountIsApproved WithEmailAddressConfirmationCode(string? emailAddressConfirmationCode = null);
}

internal interface ICreateAccountIsApproved
{
    ApplicationUserBuilder WithAccountIsApproved(bool accountIsApproved = false);
}

// internal interface IBaseInformation
// {
//     IBaseInformation WithCreatedDate(DateTime createdDate);
//     IBaseInformation WithCreatedBy(Guid? createdBy = null);
//     IBaseInformation WithLastUpdatedBy(Guid? lastUpdatedBy = null);
//     IBaseInformation WithLastUpdatedDate(DateTime? lastUpdatedDate = null);
//     IBaseInformation WithIsDeleted(bool isDeleted = false);
//     IBaseInformation WithDeletedBy(Guid? deletedBy = null);
//     IBaseInformation WithDeletedDate(DateTime? deletedDate = null);
//     IBaseInformation WithIsSystem(bool isSystem = false);
//     IBaseInformation WithIsGdpr(bool isGdpr = false);
//     IBaseInformation WithEmailAddressIsConfirmed(bool emailAddressIsConfirmed = false);
//     IBaseInformation WithEmailAddressConfirmationCode(string? emailAddressConfirmationCode = null);
//     IBaseInformation WithAccountIsApproved(bool accountIsApproved = false);
//     ApplicationUser Build();
// }