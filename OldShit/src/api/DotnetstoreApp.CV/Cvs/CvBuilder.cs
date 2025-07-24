using Ardalis.GuardClauses;
using DotnetstoreApp.SDK.Services;

namespace DotnetstoreApp.CV.Cvs;

internal sealed class CvBuilder : 
    ICreateId, 
    ICreateName, 
    ICreateLanguage, 
    ICreateLastName,
    ICreateFirstName,
    ICreateDateOfBirth,
    ICreateIntroduction,
    IBuild
{
    private Guid _cvId;
    private string _name = string.Empty;
    private string _language = string.Empty;
    private string _lastName = string.Empty;
    private string _firstName = string.Empty;
    private DateTime _dateOfBirth;
    private string _introduction = string.Empty;

    private CvBuilder()
    {
    }

    internal static ICreateId Create()
    {
        return new CvBuilder();
    }
    
    ICreateName ICreateId.WithId()
    {
        _cvId = Guid.CreateVersion7();
        return this;
    }

    ICreateName ICreateId.WithId(Guid cvId)
    {
        var id = Guard.Against.Default(cvId, nameof(cvId));
        _cvId = id;
        return this;
    }

    ICreateLanguage ICreateName.WithName(string name)
    {
        var cvName = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        cvName = Guard.Against.StringTooLong(cvName, DataSchemeConstants.MaxCvNameLength, nameof(name));
        _name = cvName;
        return this;
    }

    ICreateLastName ICreateLanguage.WithLanguage(string language)
    {
        var cvLanguage = Guard.Against.NullOrWhiteSpace(language, nameof(language));
        cvLanguage = Guard.Against.StringTooLong(cvLanguage, DataSchemeConstants.MaxCvLanguageLength, nameof(language));
        _language = cvLanguage;
        return this;
    }

    ICreateFirstName ICreateLastName.WithLastName(string lastName)
    {
        var cvLastName = Guard.Against.NullOrWhiteSpace(lastName, nameof(lastName));
        cvLastName = Guard.Against.StringTooLong(cvLastName, DataSchemeConstants.MaxCvLastNameLength, nameof(lastName));
        _lastName = cvLastName;
        return this;
    }

    ICreateDateOfBirth ICreateFirstName.WithFirstName(string firstName)
    {
        var cvFirstName = Guard.Against.NullOrWhiteSpace(firstName, nameof(firstName));
        cvFirstName = Guard.Against.StringTooLong(cvFirstName, DataSchemeConstants.MaxCvFirstNameLength, nameof(firstName));
        _firstName = cvFirstName;
        return this;
    }

    ICreateIntroduction ICreateDateOfBirth.WithDateOfBirth(DateTime dateOfBirth)
    {
        var cvDateOfBirth = Guard.Against.Default(dateOfBirth, nameof(dateOfBirth));
        cvDateOfBirth = Guard.Against.OutOfSQLDateRange(cvDateOfBirth, nameof(dateOfBirth));
        cvDateOfBirth = Guard.Against.OutOfRange(cvDateOfBirth, nameof(dateOfBirth), DateTime.Now.AddYears(-70), DateTime.Now.AddYears(-15));
        _dateOfBirth = cvDateOfBirth;
        return this;
    }
    
    IBuild ICreateIntroduction.WithIntroduction(string introduction)
    {
        var cvIntroduction = Guard.Against.NullOrWhiteSpace(introduction, nameof(introduction));
        cvIntroduction = Guard.Against.StringTooLong(cvIntroduction, DataSchemeConstants.MaxCvIntroductionLength, nameof(introduction));
        _introduction = cvIntroduction;
        return this;
    }

    Cv IBuild.Build()
    {
        return Cv.Create(
            CvId.Create(_cvId),
            _name,
            _language,
            _lastName,
            _firstName,
            _dateOfBirth,
            _introduction);
    }
}

internal interface ICreateId
{
    ICreateName WithId();
    ICreateName WithId(Guid cvId);
}

internal interface ICreateName
{
    ICreateLanguage WithName(string name);
}

internal interface ICreateLanguage
{
    ICreateLastName WithLanguage(string language);
}

internal interface ICreateLastName
{
    ICreateFirstName WithLastName(string lastName);
}

internal interface ICreateFirstName
{
    ICreateDateOfBirth WithFirstName(string firstName);
}

internal interface ICreateDateOfBirth
{
    ICreateIntroduction WithDateOfBirth(DateTime dateOfBirth);
}

internal interface ICreateIntroduction
{
    IBuild WithIntroduction(string introduction);
}

internal interface IBuild
{
    Cv Build();
}