using DotnetstoreApp.CV.Cvs;
using DotnetstoreApp.CV.Educations;
using DotnetstoreApp.CV.Experiences;
using DotnetstoreApp.CV.Information;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetstoreApp.CV.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCv(this IServiceCollection services)
    {
        services
            .AddScoped<ICvService, CvService>()
            .AddScoped<ICvRepository, CvRepository>()
            .AddScoped<ICvInformationService, CvInformationService>()
            .AddScoped<ICvInformationRepository, CvInformationRepository>()
            .AddScoped<ICvExperienceService, CvExperienceService>()
            .AddScoped<ICvExperienceRepository, CvExperienceRepository>()
            .AddScoped<ICvEducationService, CvEducationService>()
            .AddScoped<ICvEducationRepository, CvEducationRepository>();
        
        return services;
    }
}