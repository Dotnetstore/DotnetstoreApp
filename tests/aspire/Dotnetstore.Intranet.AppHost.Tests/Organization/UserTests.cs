using System.Net.Http.Json;
using Dotnetstore.Intranet.AppHost.Tests.Fixtures;
using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
using Dotnetstore.Intranet.SDK.Services;
using Shouldly;

namespace Dotnetstore.Intranet.AppHost.Tests.Organization;

public sealed class UserTests
{
    private readonly HttpClient _client = ApiFixture.CreateHttpClientAsync().GetAwaiter().GetResult();

    [Fact]
    public async Task CreateUser_Should_ReturnOk()
    {
        // Arrange
        var request = CreateUserRequest();
        
        // Act
        var response = await _client.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, request);
        
        // Assert
        response.EnsureSuccessStatusCode();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    // [Fact]
    // public async Task GetUserById_Should_Return200Ok()
    // {
    //     // Arrange
    //     var request = CreateUserRequest();
    //     var createResponse = await _client.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, request);
    //     createResponse.EnsureSuccessStatusCode();
    //
    //     var idAsString = createResponse.Headers.Location!.OriginalString.Replace("/api/users/", "");
    //     var id = Guid.Parse(idAsString);
    //     
    //     // Act
    //     var response = await _client.GetAsync(ApiEndpoints.Organization.ApplicationUser.GetById.Replace("{id:guid}", id.ToString()));
    //     
    //     // Assert
    //     response.EnsureSuccessStatusCode();
    //     response.StatusCode.ShouldBe(HttpStatusCode.OK);
    //     var user = await response.Content.ReadFromJsonAsync<ApplicationUserRegisterRequest>();
    //     user.FirstName.ShouldBe("John");
    // }
    
    // [Fact]
    // public async Task UserLogin_Should_ReturnResponseWithToken()
    // {
    //     // Arrange
    //     var request = CreateUserRequest();
    //     await _client.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, request);
    //     
    //     var loginRequest = new ApplicationUserLoginRequest(
    //         request.EmailAddress,
    //         request.Password);
    //     
    //     // Act
    //     var loginResponse = await _client.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Login, loginRequest);
    //     
    //     // Assert
    //     loginResponse.EnsureSuccessStatusCode();
    //     loginResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    //     var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<ApplicationUserTokenResponse>();
    //     tokenResponse.Token.ShouldNotBeNullOrEmpty();
    //     tokenResponse.RefreshToken.ShouldNotBeNullOrEmpty();
    // }
    
    // [Fact]
    // public async Task RenewRefreshToken_Should_ReturnNewToken()
    // {
    //     // Arrange
    //     var request = CreateUserRequest();
    //     await _client.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Create, request);
    //     
    //     var loginRequest = new ApplicationUserLoginRequest(
    //         request.EmailAddress,
    //         request.Password);
    //     
    //     var loginResponse = await _client.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.Login, loginRequest);
    //     var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<ApplicationUserTokenResponse>();
    //     var renewRequest = new ApplicationUserRefreshTokenRequest(tokenResponse.UserId, tokenResponse.RefreshToken);
    //     
    //     // Act
    //     var renewResponse = await _client.PostAsJsonAsync(ApiEndpoints.Organization.ApplicationUser.RenewRefreshToken, renewRequest);
    //     
    //     // Assert
    //     renewResponse.EnsureSuccessStatusCode();
    //     renewResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
    //     var newTokenResponse = await renewResponse.Content.ReadFromJsonAsync<ApplicationUserTokenResponse>();
    //     newTokenResponse.Token.ShouldNotBeNullOrEmpty();
    //     newTokenResponse.RefreshToken.ShouldNotBeNullOrEmpty();
    // }
    
    private static ApplicationUserRegisterRequest CreateUserRequest() =>
        new(
            LastName: "Doe",
            FirstName: "John",
            MiddleName: null,
            DateOfBirth: new DateTime(1990, 1, 1),
            IsMale: true,
            SocialSecurityNumber: "123-45-6789",
            EmailAddress: "test@test.com",
            Password: "Password123!",
            ConfirmPassword: "Password123!");
}