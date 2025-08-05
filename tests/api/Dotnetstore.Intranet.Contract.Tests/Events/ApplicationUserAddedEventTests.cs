using Dotnetstore.Intranet.Contract.Events;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Contract.Tests.Events;

public class ApplicationUserAddedEventTests
{
    [Fact]
    public void ApplicationUserAddedEvent_ShouldHaveCorrectProperties()
    {
        // Arrange
        var type = typeof(ApplicationUserAddedEvent);

        // Act
        var properties = type.GetProperties();

        // Assert
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserAddedEvent.EmailAddress) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserAddedEvent.EmailAddressVerificationCode) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserAddedEvent.FullName) && p.PropertyType == typeof(string));
        properties.ShouldContain(p => p.Name == nameof(ApplicationUserAddedEvent.UserId) && p.PropertyType == typeof(Guid));
        properties.Length.ShouldBe(4);
    }
}