// using Dotnetstore.Intranet.Organization.Users.RenewRefreshToken;
// using Dotnetstore.Intranet.SDK.Requests.Organization.Users;
// using Shouldly;
// using Xunit;
//
// namespace Dotnetstore.Intranet.Organization.Tests.Users.RenewRefreshToken;
//
// public class ApplicationUserRefreshTokenRequestValidationTests
// {
//     [Theory]
//     [InlineData("08828BD0-DE8E-4DBC-A47A-9DEE5F08A0E0", "valid-refresh-token")]
//     public void ApplicationUserRefreshTokenRequestValidation_Should_PassValidationWhenValidDataProvided(string userId, string refreshToken)
//     {
//         // Arrange
//         var request = new ApplicationUserRefreshTokenRequest(Guid.Parse(userId), refreshToken);
//         var validator = new ApplicationUserRefreshTokenRequestValidation();
//
//         // Act
//         var result = validator.Validate(request);
//
//         // Assert
//         result.IsValid.ShouldBeTrue();
//     }
//     
//     [Theory]
//     [InlineData("00000000-0000-0000-0000-000000000000", "")]
//     [InlineData("00000000-0000-0000-0000-000000000000", "sdfhgsdhfdsfgdsfgdsfgdsfgdsfgdsfgdsfgdsfgdsfgdsfgdsfgdsfgdsfgdsfg")]
//     [InlineData("08828BD0-DE8E-4DBC-A47A-9DEE5F08A0E0", "")]
//     public void ApplicationUserRefreshTokenRequestValidation_Should_FailValidationWhenInvalidDataProvided(string userId, string refreshToken)
//     {
//         // Arrange
//         var request = new ApplicationUserRefreshTokenRequest(Guid.Parse(userId), refreshToken);
//         var validator = new ApplicationUserRefreshTokenRequestValidation();
//
//         // Act
//         var result = validator.Validate(request);
//
//         // Assert
//         result.IsValid.ShouldBeFalse();
//     }
// }