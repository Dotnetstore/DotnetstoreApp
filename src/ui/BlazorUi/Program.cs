using System.Globalization;
using MudBlazor.Services;
using BlazorUi.Components;
using BlazorUi.Services;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var cancellationToken = new CancellationTokenSource().Token;

// Add services to the container.
builder.Services
    .AddMudServices()
    .AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services
    .AddLocalization(options => options.ResourcesPath = "Resources");

var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("sv") };
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

builder.Services.AddScoped<ResXMudLocalizer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app
    .UseRequestLocalization(app.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value)
    .UseHttpsRedirection()
    .UseAntiforgery();

app.MapStaticAssets();
app
    .MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync(cancellationToken);