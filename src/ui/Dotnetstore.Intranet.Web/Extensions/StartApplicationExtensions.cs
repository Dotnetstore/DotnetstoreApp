using System.Security.Cryptography;
using Dotnetstore.Intranet.SDK.Extensions;
using Dotnetstore.Intranet.SDK.Models;
using Dotnetstore.Intranet.ServiceDefaults;
using Dotnetstore.Intranet.Web.Pages;
using Dotnetstore.Intranet.Web.Pages.Users.Models;
using Dotnetstore.Intranet.Web.Pages.Users.Services;
using Dotnetstore.Intranet.Web.Pages.Users.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.IdentityModel.Tokens;

namespace Dotnetstore.Intranet.Web.Extensions;

internal static class StartApplicationExtensions
{
    internal static async ValueTask StartApplicationAsync(this WebApplicationBuilder builder)
    {
        var appSettings = GetAppSettings(builder);
        
        await builder
            .RegisterServices(appSettings)
            .BuildApplication()
            .RegisterLocalizationMiddleware()
            .RegisterMiddleware()
            .RunApplicationAsync();
    }
    
    private static async ValueTask RunApplicationAsync(
        this WebApplication app)
    {
        await app.RunAsync().ConfigureAwait(false);
    }
    
    private static WebApplication RegisterMiddleware(
        this WebApplication app)
    {
        app
            .UseMiddleware<JwtCookieAuthenticationMiddleware>();
        
        if (!app.Environment.IsDevelopment())
        {
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app
                .UseExceptionHandler("/Error")
                .UseHsts();
        }
        
        app
            .UseHttpsRedirection()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization();

        app
            .MapStaticAssets();

        app
            .MapRazorPages()
            .WithStaticAssets();

        return app;
    }
    
    private static WebApplication RegisterLocalizationMiddleware(
        this WebApplication app)
    {
        var supportedCultures = new[] { "en-US", "sv-SE" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);

        app
            .UseRequestLocalization(localizationOptions);
        
        return app;
    }
    
    private static WebApplication BuildApplication(this WebApplicationBuilder builder)
    {
        return builder.Build();
    }
    
    private static WebApplicationBuilder RegisterServices(
        this WebApplicationBuilder builder,
        AppSettings appSettings)
    {
        builder
            .RegisterSdk(appSettings)
            .AddServiceDefaults()
            .RegisterLocalization()
            .RegisterTokenServices(appSettings)
            .RegisterHttpClients(appSettings)
            .RegisterValidators();

        builder.Services
            .AddHttpContextAccessor()
            .Configure<RouteOptions>(options =>
            {
                options.LowercaseUrls = true;
                options.LowercaseQueryStrings = true;
                options.AppendTrailingSlash = true;
            })
            .AddTransient<AuthenticatedHttpClientHandler>();
        
        return builder;
    }
    
    private static void RegisterValidators(
        this WebApplicationBuilder builder)
    {
        builder.Services
            .AddScoped<IValidator<LoginModel>, LoginValidator>()
            .AddScoped<IValidator<RegisterModel>, RegisterValidator>();
    }
    
    private static WebApplicationBuilder RegisterHttpClients(
        this WebApplicationBuilder builder,
        AppSettings appSettings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(appSettings.HttpClientBaseAddress, nameof(appSettings.HttpClientBaseAddress));
        ArgumentException.ThrowIfNullOrWhiteSpace(appSettings.LocalApiClientName, nameof(appSettings.LocalApiClientName));

        builder.Services.AddHttpClient(appSettings.LocalApiClientName, client =>
        {
            client.BaseAddress = new Uri(appSettings.HttpClientBaseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        })
        .AddHttpMessageHandler<AuthenticatedHttpClientHandler>();

        return builder;
    }
    
    private static WebApplicationBuilder RegisterTokenServices(
        this WebApplicationBuilder builder,
        AppSettings appSettings)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(appSettings.JwtIssuer, nameof(appSettings.JwtIssuer));
        ArgumentException.ThrowIfNullOrWhiteSpace(appSettings.JwtAudience, nameof(appSettings.JwtAudience));
        ArgumentException.ThrowIfNullOrWhiteSpace(appSettings.SecurityKey, nameof(appSettings.SecurityKey));

        var hmac = new HMACSHA512(Convert.FromBase64String(appSettings.SecurityKey));

        builder.Services
            .AddSingleton(new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = appSettings.JwtIssuer,
            ValidAudience = appSettings.JwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(hmac.Key)
        })
            .AddScoped<ITokenProvider, TokenProvider>();

        return builder;
    }
    
    private static WebApplicationBuilder RegisterLocalization(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddLocalization(options => options.ResourcesPath = "Resources")
            .AddRazorPages(options =>
            {
                options.Conventions
                    .AddFolderApplicationModelConvention("/Pages", model =>
                    {
                        model.Filters.Add(new ValidateModelStatePageFilter());
                    });
            })
            .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();
        
        return builder;
    }

    private static AppSettings GetAppSettings(WebApplicationBuilder builder)
    {
        var appSettings = builder.Configuration.GetSection("AppSettings").Get<AppSettings>();
        ArgumentNullException.ThrowIfNull(appSettings, nameof(appSettings));
        builder.Services.AddSingleton(appSettings);
        return appSettings;
    }
}