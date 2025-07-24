using DotnetstoreApp.CV.DB;

namespace DotnetstoreApp.CV.Information;

public sealed class CvInformationRepository : ICvInformationRepository
{
    async ValueTask ICvInformationRepository.CreateAsync(CvInformation information, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        FakeDb.InformationList.Add(information);
    }

    async ValueTask ICvInformationRepository.DeleteAsync(CvInformationId id, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var information = FakeDb.InformationList.FirstOrDefault(x => x.Id == id);
        
        if (information is not null)
        {
            FakeDb.InformationList.Remove(information);
        }
    }
}