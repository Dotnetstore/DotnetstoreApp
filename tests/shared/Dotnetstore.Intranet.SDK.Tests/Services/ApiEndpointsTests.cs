using Dotnetstore.Intranet.SDK.Services;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.SDK.Tests.Services;

public class ApiEndpointsTests
{
    // [Fact]
    // public void Organization_ApplicationUser_GetAll_ShouldReturnCorrectEndpoint()
    // {
    //     // Arrange
    //     const string expected = "/api/users";
    //
    //     // Act
    //     const string actual = ApiEndpoints.Organization.ApplicationUser.GetAll;
    //
    //     // Assert
    //     actual.ShouldBe(expected);
    // }
    //
    // [Fact]
    // public void Organization_ApplicationUser_GetById_ShouldReturnCorrectEndpoint()
    // {
    //     // Arrange
    //     const string expected = "/api/users/{id:guid}";
    //
    //     // Act
    //     const string actual = ApiEndpoints.Organization.ApplicationUser.GetById;
    //
    //     // Assert
    //     actual.ShouldBe(expected);
    // }
    //
    // [Fact]
    // public void Organization_ApplicationUser_Login_ShouldReturnCorrectEndpoint()
    // {
    //     // Arrange
    //     const string expected = "/api/users/login";
    //
    //     // Act
    //     const string actual = ApiEndpoints.Organization.ApplicationUser.Login;
    //
    //     // Assert
    //     actual.ShouldBe(expected);
    // }

    [Fact]
    public void Organization_ApplicationUser_Create_ShouldReturnCorrectEndpoint()
    {
        // Arrange
        const string expected = "/api/users";

        // Act
        const string actual = ApiEndpoints.Organization.ApplicationUser.Create;

        // Assert
        actual.ShouldBe(expected);
    }

    // [Fact]
    // public void Organization_ApplicationUser_RenewRefreshToken_ShouldReturnCorrectEndpoint()
    // {
    //     // Arrange
    //     const string expected = "/api/users/renew-refresh-token";
    //
    //     // Act
    //     const string actual = ApiEndpoints.Organization.ApplicationUser.RenewRefreshToken;
    //
    //     // Assert
    //     actual.ShouldBe(expected);
    // }
}