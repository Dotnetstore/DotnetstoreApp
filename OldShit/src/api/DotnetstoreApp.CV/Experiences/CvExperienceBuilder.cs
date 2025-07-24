using Ardalis.GuardClauses;
using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.SDK.Services;

namespace DotnetstoreApp.CV.Experiences;

internal sealed class CvExperienceBuilder : 
    ICreateId, 
    ICreateCvId, 
    ICreateStartDate, 
    ICreateEndDate, 
    ICreateCompany, 
    ICreateIsConsultant, 
    ICreateRole, 
    ICreateExtent, 
    ICreateTools, 
    ICreateCompanyNeeds, 
    ICreateMission, 
    IBuild
{
    private Guid _id;
    private Guid _cvId;
    private DateTime _startDate;
    private DateTime? _endDate;
    private string _company = string.Empty;
    private bool _isConsultant;
    private string _role = string.Empty;
    private int _extent;
    private string _tools = string.Empty;
    private string _companyNeeds = string.Empty;
    private string _mission = string.Empty;
    
    private CvExperienceBuilder()
    {
    }
    
    internal static ICreateId Create()
    {
        return new CvExperienceBuilder();
    }
    
    ICreateCvId ICreateId.WithId()
    {
        _id = Guid.CreateVersion7();
        return this;
    }

    ICreateCvId ICreateId.WithId(Guid id)
    {
        _id = Guard.Against.Default(id, nameof(id));
        return this;
    }

    ICreateStartDate ICreateCvId.WithCvId(Guid cvId)
    {
        _cvId = Guard.Against.Default(cvId, nameof(cvId));
        return this;
    }

    ICreateEndDate ICreateStartDate.WithStartDate(DateTime startDate)
    {
        _startDate = Guard.Against.Default(startDate, nameof(startDate));
        if (_startDate.Kind != DateTimeKind.Utc)
        {
            _startDate = _startDate.ToUniversalTime();
        }
        return this;
    }

    ICreateCompany ICreateEndDate.WithEndDate(DateTime? endDate)
    {
        if (endDate.HasValue)
        {
            _endDate = Guard.Against.Default(endDate.Value, nameof(endDate));
            if (_endDate.Value.Kind != DateTimeKind.Utc)
            {
                _endDate = _endDate.Value.ToUniversalTime();
            }
        }
        else
        {
            _endDate = null;
        }
        return this;
    }

    ICreateIsConsultant ICreateCompany.WithCompany(string company)
    {
        _company = Guard.Against.NullOrWhiteSpace(company, nameof(company));
        _company = Guard.Against.StringTooLong(_company, DataSchemeConstants.MaxCvExperienceCompanyLength, nameof(company));
        return this;
    }

    ICreateRole ICreateIsConsultant.WithIsConsultant(bool isConsultant)
    {
        _isConsultant = isConsultant;
        return this;
    }

    ICreateExtent ICreateRole.WithRole(string role)
    {
        _role = Guard.Against.NullOrWhiteSpace(role, nameof(role));
        _role = Guard.Against.StringTooLong(_role, DataSchemeConstants.MaxCvExperienceRoleLength, nameof(role));
        return this;
    }

    ICreateTools ICreateExtent.WithExtent(int extent)
    {
        _extent = Guard.Against.OutOfRange(extent, nameof(extent), DataSchemeConstants.MinCvExperienceExtentValue, DataSchemeConstants.MaxCvExperienceExtentValue);
        return this;
    }

    ICreateCompanyNeeds ICreateTools.WithTools(string tools)
    {
        _tools = Guard.Against.NullOrWhiteSpace(tools, nameof(tools));
        _tools = Guard.Against.StringTooLong(_tools, DataSchemeConstants.MaxCvExperienceToolsLength, nameof(tools));
        return this;
    }

    ICreateMission ICreateCompanyNeeds.WithCompanyNeeds(string companyNeeds)
    {
        _companyNeeds = Guard.Against.NullOrWhiteSpace(companyNeeds, nameof(companyNeeds));
        return this;
    }

    IBuild ICreateMission.WithMission(string mission)
    {
        _mission = Guard.Against.NullOrWhiteSpace(mission, nameof(mission));
        return this;
    }

    CvExperience IBuild.Build()
    {
        return CvExperience.Create(
            CvExperienceId.Create(_id),
            CvId.Create(_cvId),
            _startDate,
            _endDate,
            _company,
            _isConsultant,
            _role,
            _extent,
            _tools,
            _companyNeeds,
            _mission);
    }
}

internal interface ICreateId
{
    ICreateCvId WithId();
    ICreateCvId WithId(Guid id);
}

internal interface ICreateCvId
{
    ICreateStartDate WithCvId(Guid cvId);
}

internal interface ICreateStartDate
{
    ICreateEndDate WithStartDate(DateTime startDate);
}

internal interface ICreateEndDate
{
    ICreateCompany WithEndDate(DateTime? endDate);
}

internal interface ICreateCompany
{
    ICreateIsConsultant WithCompany(string company);
}

internal interface ICreateIsConsultant
{
    ICreateRole WithIsConsultant(bool isConsultant);
}

internal interface ICreateRole
{
    ICreateExtent WithRole(string role);
}

internal interface ICreateExtent
{
    ICreateTools WithExtent(int extent);
}

internal interface ICreateTools
{
    ICreateCompanyNeeds WithTools(string tools);
}

internal interface ICreateCompanyNeeds
{
    ICreateMission WithCompanyNeeds(string companyNeeds);
}

internal interface ICreateMission
{
    IBuild WithMission(string mission);
}

internal interface IBuild
{
    CvExperience Build();
}