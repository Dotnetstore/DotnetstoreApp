using Dotnetstore.Intranet.EmailService.Handlers;
using Dotnetstore.Intranet.EmailService.Services;
using Dotnetstore.Intranet.ServiceDefaults;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.Title = "Dotnetstore Intranet EmailService";

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

var endpointConfiguration = new EndpointConfiguration("DotnetstoreIntranetEmailService");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();

builder.Services
    .AddScoped<ISmtpService, SmtpService>()
    .AddScoped<ApplicationUserAddedEventSendConfirmationCodeHandler>();

var transport = endpointConfiguration.UseTransport(new LearningTransport());

builder.UseNServiceBus(endpointConfiguration);

var app = builder.Build();

await app.RunAsync();