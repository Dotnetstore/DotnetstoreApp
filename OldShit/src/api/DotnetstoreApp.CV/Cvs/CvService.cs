using Ardalis.Result;
using DotnetstoreApp.CV.Cvs.Create;
using DotnetstoreApp.CV.Cvs.Update;
using DotnetstoreApp.CV.DB;
using DotnetstoreApp.SDK.Requests.CV;
using DotnetstoreApp.SDK.Responses.CV;

namespace DotnetstoreApp.CV.Cvs;

public sealed class CvService : ICvService
{
    private readonly ICvRepository _cvRepository;

    public CvService(ICvRepository cvRepository)
    {
        _cvRepository = cvRepository;
        
        if(FakeDb.CvList.Count > 0) return;
        
        FakeDb.AddDummyData();
    }
    
    async ValueTask<IEnumerable<CvResponse>> ICvService.GetAllAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return FakeDb.CvList
            .OrderBy(x => x.Name)
            .Select(x => x.ToCvResponse());
    }

    async ValueTask<Result<CvFullResponse>> ICvService.GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (id == Guid.Empty)
            return Result<CvFullResponse>.Error("Invalid CV ID");
        
        var cv = await _cvRepository.GetByIdAsync(CvId.Create(id), cancellationToken);
        
        if (cv is null)
            return Result<CvFullResponse>.NotFound($"CV with ID {id} not found");
        
        return cv.ToCvFullResponse();
    }

    async ValueTask<Result<CvResponse>> ICvService.CreateAsync(CvCreateRequest request, CancellationToken cancellationToken)
    {
        var validator = new CvCreateRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return Result<CvResponse>.Error("Validation failed");
        }
        
        var cv = CvBuilder.Create()
            .WithId()
            .WithName(request.Name)
            .WithLanguage(request.Language)
            .WithLastName(request.LastName)
            .WithFirstName(request.FirstName)
            .WithDateOfBirth(request.DateOfBirth)
            .WithIntroduction(request.Introduction)
            .Build();
        
        FakeDb.CvList.Add(cv);
        return cv.ToCvResponse();
    }

    async ValueTask<Result> ICvService.UpdateAsync(Guid id, CvUpdateRequest request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        
        if (id == Guid.Empty)
            return Result.Error("Invalid CV ID");
        
        var validator = new CvUpdateRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return Result.Error("Validation failed");
        }
        
        var cv = CvBuilder.Create()
            .WithId(id)
            .WithName(request.Name)
            .WithLanguage(request.Language)
            .WithLastName(request.LastName)
            .WithFirstName(request.FirstName)
            .WithDateOfBirth(request.DateOfBirth)
            .WithIntroduction(request.Introduction)
            .Build();
        
        _cvRepository.Update(cv, cancellationToken);
        return Result.Success();
    }

    async ValueTask<Result> ICvService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _cvRepository.DeleteAsync(CvId.Create(id), cancellationToken);
        return Result.Success();
    }
}