using System.Net.Http.Json;
using Aspire.Hosting;
using Dotnetstore.Intranet.Organization.Data;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Dotnetstore.Intranet.AppHost.Tests.Api;

public class ApplicationUserEndpointTests
{
    [Fact]
    public async Task Confirm_NonExistentCode_Should_ReturnError()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();
        
        // Act
        var request = new ApplicationUserConfirmEmailRequest("InvalidCode");
        var response = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.ConfirmEmailAddress, request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task Confirm_ValidCode_Should_ReturnOk()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();

        // Act
        var user = CreateUserRequest();
        var registerResponse = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user);
        registerResponse.EnsureSuccessStatusCode();

        var code = await dbContext.ApplicationUsers
            .Where(u => u.EmailAddress == user.EmailAddress)
            .Select(u => u.EmailAddressConfirmationCode)
            .FirstAsync();

        var request = new ApplicationUserConfirmEmailRequest(code!);
        var response = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.ConfirmEmailAddress, request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Create_Should_ReturnOk()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();

        // Act
        var user = CreateUserRequest();
        var response = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Create_ExistingUser_Should_ReturnBadRequest()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();
        var user = CreateUserRequest();
        await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user);

        // Act
        var response = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Login_WrongCredentials_Should_ReturnUnauthorized()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();

        // Act
        var request = new ApplicationUserLoginRequest("test@test.com", "WrongPassword123!");
        var response = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Login, request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }
    
    [Fact]
    public async Task Login_ValidCredentials_NotConfirmedEmailAddress_Should_ReturnUnauthorized()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();
        var user = CreateUserRequest();
        await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user);

        // Act
        var request = new ApplicationUserLoginRequest(user.EmailAddress, user.Password);
        var response = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Login, request);

        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Login_ValidCredentials_Should_ReturnOk()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();
        var user = CreateUserRequest();
        await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user);

        var code = await dbContext.ApplicationUsers
            .Where(u => u.EmailAddress == user.EmailAddress)
            .Select(u => u.EmailAddressConfirmationCode)
            .FirstAsync();
        var confirmRequest = new ApplicationUserConfirmEmailRequest(code!);
        var confirmResponse =
            await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.ConfirmEmailAddress,
                confirmRequest);
        confirmResponse.EnsureSuccessStatusCode();

        // Act
        var loginRequest = new ApplicationUserLoginRequest(user.EmailAddress, user.Password);
        var loginResponse =
            await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Login, loginRequest);

        // Assert
        loginResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Login_SecondUser_WithConfirmedEmailAddress_Should_ReturnUnauthorized()
    {
        // Arrange
        var (app, resourceNotificationService, dbContext, httpClient) = await StartAppAsync();
        var user1 = CreateUserRequest();
        await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user1);

        var code1 = await dbContext.ApplicationUsers
            .Where(u => u.EmailAddress == user1.EmailAddress)
            .Select(u => u.EmailAddressConfirmationCode)
            .FirstAsync();
        var confirmRequest1 = new ApplicationUserConfirmEmailRequest(code1!);
        var confirmResponse1 =
            await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.ConfirmEmailAddress,
                confirmRequest1);
        confirmResponse1.EnsureSuccessStatusCode();

        var user2 = CreateUserRequest();
        user2.EmailAddress = "testar@test.com";
        await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, user2);
        var code2 = await dbContext.ApplicationUsers
            .Where(u => u.EmailAddress == user2.EmailAddress)
            .Select(u => u.EmailAddressConfirmationCode)
            .FirstAsync();
        var confirmRequest2 = new ApplicationUserConfirmEmailRequest(code2!);
        var confirmResponse2 =
            await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.ConfirmEmailAddress,
                confirmRequest2);
        confirmResponse2.EnsureSuccessStatusCode();

        // Act
        var loginRequest = new ApplicationUserLoginRequest(user2.EmailAddress, user2.Password);
        var loginResponse = await httpClient.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Login, loginRequest);

        // Assert
        loginResponse.StatusCode.ShouldBe(HttpStatusCode.Unauthorized);
    }

    private static async Task<(
        DistributedApplication app, 
        ResourceNotificationService resourceNotificationService, 
        OrganizationDataContext dbContext, 
        HttpClient httpClient)> StartAppAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.Dotnetstore_Intranet_AppHost>();

        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });

        var app = await appHost.BuildAsync();
        var resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();
        
        var dbContext = await GetDbContextAsync(appHost);

        await resourceNotificationService.WaitForResourceAsync("webapi", KnownResourceStates.Running)
            .WaitAsync(TimeSpan.FromSeconds(30));
        
        var httpClient = app.CreateHttpClient("webapi");

        return (app, resourceNotificationService, dbContext, httpClient);
    }
    
    private static async Task<OrganizationDataContext> GetDbContextAsync(IDistributedApplicationTestingBuilder appHost)
    {
        var db = appHost.Resources.OfType<PostgresDatabaseResource>()
            .Single(r => r.Name == "DotnetstoreIntranet");

        var connectionString = await db.ConnectionStringExpression.GetValueAsync(CancellationToken.None);
        var options = new DbContextOptionsBuilder<OrganizationDataContext>()
            .UseNpgsql(connectionString)
            .Options;

        return new OrganizationDataContext(options);
    }
     
    private static ApplicationUserRegisterRequest CreateUserRequest() =>
     new(
         LastName: "Doe",
         FirstName: "John",
         MiddleName: null,
         DateOfBirth: new DateTime(1990, 1, 1),
         IsMale: true,
         SocialSecurityNumber: null,
         EmailAddress: "test@test.com",
         Password: "Password123!",
         ConfirmPassword: "Password123!");
}