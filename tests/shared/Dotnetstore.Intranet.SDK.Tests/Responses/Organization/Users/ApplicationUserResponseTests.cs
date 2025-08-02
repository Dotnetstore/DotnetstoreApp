// using Dotnetstore.Intranet.SDK.Responses.Organization.Users;
// using Shouldly;
// using Xunit;
//
// namespace Dotnetstore.Intranet.SDK.Tests.Responses.Organization.Users;
//
// public class ApplicationUserResponseTests
// {
//     [Fact]
//     public void UserResponse_Should_ContainCorrectProperties()
//     {
//         // Arrange
//         var type = typeof(ApplicationUserResponse);
//
//         // Act
//         var properties = type.GetProperties();
//         
//         // Assert
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.Id) && p.PropertyType == typeof(Guid));
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.LastName) && p.PropertyType == typeof(string));
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.FirstName) && p.PropertyType == typeof(string));
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.MiddleName) && p.PropertyType == typeof(string));
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.DateOfBirth) && p.PropertyType == typeof(DateTime));
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.IsMale) && p.PropertyType == typeof(bool));
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.SocialSecurityNumber) && p.PropertyType == typeof(string));
//         properties.ShouldContain(p => p.Name == nameof(ApplicationUserResponse.Email) && p.PropertyType == typeof(string));
//         properties.Length.ShouldBe(8, "UserResponse should have exactly 8 properties.");
//     }
// }