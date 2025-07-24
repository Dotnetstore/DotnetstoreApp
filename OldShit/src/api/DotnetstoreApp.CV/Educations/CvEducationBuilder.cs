using Ardalis.GuardClauses;
using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.SDK.Services;

namespace DotnetstoreApp.CV.Educations;

internal sealed class CvEducationBuilder :
    ICreateId,
    ICreateCvId,
    ICreateSchool,
    ICreateLevel,
    ICreateStartDate,
    ICreateEndDate,
    IBuild
{
    private Guid _id;
    private Guid _cvId;
    private string _school = string.Empty;
    private string _level = string.Empty;
    private DateTime _startDate;
    private DateTime? _endDate;

    private CvEducationBuilder()
    {
    }

    internal static ICreateId Create()
    {
        return new CvEducationBuilder();
    }

    public ICreateCvId WithId()
    {
        _id = Guid.CreateVersion7();
        return this;
    }

    public ICreateCvId WithId(Guid id)
    {
        _id = Guard.Against.Default(id, nameof(id));
        return this;
    }

    public ICreateSchool WithCvId(Guid cvId)
    {
        _cvId = Guard.Against.Default(cvId, nameof(cvId));
        return this;
    }

    public ICreateLevel WithSchool(string school)
    {
        _school = Guard.Against.NullOrWhiteSpace(school, nameof(school));
        _school = Guard.Against.StringTooLong(_school, DataSchemeConstants.MaxCvEducationSchoolNameLength, nameof(school));
        return this;
    }

    public ICreateStartDate WithLevel(string level)
    {
        _level = Guard.Against.NullOrWhiteSpace(level, nameof(level));
        _level = Guard.Against.StringTooLong(_level, DataSchemeConstants.MaxCvEducationLevelNameLength, nameof(level));
        return this;
    }

    public ICreateEndDate WithStartDate(DateTime startDate)
    {
        _startDate = Guard.Against.Default(startDate, nameof(startDate));
        return this;
    }

    public IBuild WithEndDate(DateTime? endDate)
    {
        _endDate = endDate;
        return this;
    }

    public CvEducation Build()
    {
        return CvEducation.Create(
            CvEducationId.Create(_id),
            CvId.Create(_cvId),
            _school,
            _level,
            _startDate,
            _endDate);
    }
}

internal interface ICreateId
{
    ICreateCvId WithId();
    ICreateCvId WithId(Guid id);
}

internal interface ICreateCvId
{
    ICreateSchool WithCvId(Guid cvId);
}

internal interface ICreateSchool
{
    ICreateLevel WithSchool(string school);
}

internal interface ICreateLevel
{
    ICreateStartDate WithLevel(string level);
}

internal interface ICreateStartDate
{
    ICreateEndDate WithStartDate(DateTime startDate);
}

internal interface ICreateEndDate
{
    IBuild WithEndDate(DateTime? endDate);
}

internal interface IBuild
{
    CvEducation Build();
}