// using Dotnetstore.Intranet.Organization.Users.Login;
// using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
// using Shouldly;
// using Xunit;
//
// namespace Dotnetstore.Intranet.Organization.Tests.Users.Login;
//
// public class ApplicationUserLoginRequestValidationTests
// {
//     [Theory]
//     [InlineData("test@test.com", "Password123")]
//     public void ApplicationUserLoginRequestValidation_Should_PassValidationWhenValidDataProvided(
//         string username,
//         string password)
//     {
//         // Arrange
//         var validator = new ApplicationUserLoginRequestValidation();
//         var request = new ApplicationUserLoginRequest(username, password);
//
//         // Act
//         var result = validator.Validate(request);
//         
//         // Assert
//         result.IsValid.ShouldBeTrue();
//     }
//     
//     [Theory]
//     [InlineData("", "Password123")]
//     [InlineData("test@test.com", "")]
//     public void ApplicationUserLoginRequestValidation_Should_FailValidationWhenInvalidDataProvided(
//         string username,
//         string password)
//     {
//         // Arrange
//         var validator = new ApplicationUserLoginRequestValidation();
//         var request = new ApplicationUserLoginRequest(username, password);
//
//         // Act
//         var result = validator.Validate(request);
//         
//         // Assert
//         result.IsValid.ShouldBeFalse();
//         result.Errors.Count.ShouldBeGreaterThan(0);
//     }
// }