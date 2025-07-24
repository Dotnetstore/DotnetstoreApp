using Ardalis.Result;
using DotnetstoreApp.CV.Information.Create;
using DotnetstoreApp.SDK.Requests.CV;

namespace DotnetstoreApp.CV.Information;

internal sealed class CvInformationService(ICvInformationRepository repository) : ICvInformationService
{
    async ValueTask<Result> ICvInformationService.CreateAsync(CvInformationCreateRequest request, CancellationToken cancellationToken)
    {
        var validator = new CvInformationCreateRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
        {
            return Result.Error("Validation failed");
        }
        
        var information = CvInformationBuilder.Create()
            .WithId()
            .WithCvId(request.CvId)
            .WithName(request.Name)
            .WithInformationType(request.CvInformationType)
            .Build();
        
        await repository.CreateAsync(information, cancellationToken);
        
        return Result.Success();
    }

    async ValueTask<Result> ICvInformationService.DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        if(id == Guid.Empty)
        {
            return Result.Error("Invalid ID");
        }
        
        await repository.DeleteAsync(CvInformationId.Create(id), cancellationToken);
        
        return Result.Success();
    }
}