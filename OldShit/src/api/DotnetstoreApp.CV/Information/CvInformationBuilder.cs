using Ardalis.GuardClauses;
using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.SDK.Enums;
using DotnetstoreApp.SDK.Services;

namespace DotnetstoreApp.CV.Information;

internal sealed class CvInformationBuilder :
    ICreateId,
    ICreateCvId,
    ICreateName,
    ICreateInformationType,
    IBuild
{
    private Guid _id;
    private Guid _cvId;
    private string _name = string.Empty;
    private CvInformationType _cvInformationType;

    private CvInformationBuilder()
    {
    }

    internal static ICreateId Create()
    {
        return new CvInformationBuilder();
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

    ICreateName ICreateCvId.WithCvId(Guid cvId)
    {
        _cvId = Guard.Against.Default(cvId, nameof(cvId));
        return this;
    }

    ICreateInformationType ICreateName.WithName(string name)
    {
        _name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        _name = Guard.Against.StringTooLong(_name, DataSchemeConstants.MaxCvInformationNameLength, nameof(name));
        return this;
    }

    IBuild ICreateInformationType.WithInformationType(CvInformationType cvInformationType)
    {
        _cvInformationType = Guard.Against.Null(cvInformationType, nameof(cvInformationType));
        return this;
    }

    CvInformation IBuild.Build()
    {
        return CvInformation.Create(
            CvInformationId.Create(_id),
            CvId.Create(_cvId),
            _name,
            _cvInformationType);
    }
}

internal interface ICreateId
{
    ICreateCvId WithId();
    ICreateCvId WithId(Guid id);
}

internal interface ICreateCvId
{
    ICreateName WithCvId(Guid cvId);
}

internal interface ICreateName
{
    ICreateInformationType WithName(string name);
}

internal interface ICreateInformationType
{
    IBuild WithInformationType(CvInformationType cvInformationType);
}

internal interface IBuild
{
    CvInformation Build();
}