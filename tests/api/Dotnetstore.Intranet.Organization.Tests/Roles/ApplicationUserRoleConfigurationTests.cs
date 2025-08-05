using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.SDK.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Shouldly;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Roles;

public class ApplicationUserRoleConfigurationTests
{
    private readonly IMutableEntityType _entityType;
    
    public ApplicationUserRoleConfigurationTests()
    {
        var builder = new ModelBuilder();
        var entityTypeBuilder = builder.Entity<ApplicationUserRole>();
        var configuration = new ApplicationUserRoleConfiguration();
        
        configuration.Configure(entityTypeBuilder);
        _entityType = entityTypeBuilder.Metadata;
    }
    
    [Fact]
    public void ApplicationUserRoleConfiguration_Should_Implement_IEntityTypeConfiguration()
    {
        // Arrange
        var configuration = new ApplicationUserRoleConfiguration();

        // Act & Assert
        configuration.ShouldBeAssignableTo<IEntityTypeConfiguration<ApplicationUserRole>>();
    }

    [Fact]
    public void ApplicationUserRoleConfiguration_Should_ImplementIdCorrect()
    {
        // Assert
        _entityType.ShouldNotBeNull();
        var idProperty = _entityType.FindProperty(nameof(ApplicationUserRole.Id));
        idProperty.ShouldNotBeNull();
        idProperty.IsKey().ShouldBeTrue();
        idProperty.IsNullable.ShouldBeFalse();
        idProperty.GetMaxLength().ShouldBeNull();
        idProperty.GetValueConverter().ShouldNotBeNull();
        idProperty.GetValueConverter()!.ConvertToProviderExpression.Body.ToString().ShouldContain("v.Value");
        idProperty.GetValueConverter()!.ConvertFromProviderExpression.Body.ToString().ShouldContain("Create(v)");
        idProperty.IsUniqueIndex().ShouldBeTrue();
    }

    [Fact]
    public void ApplicationUserRoleConfiguration_Should_ImplementNameCorrect()
    {
        // Assert
        var nameProperty = _entityType.FindProperty(nameof(ApplicationUserRole.Name));
        nameProperty.ShouldNotBeNull();
        nameProperty.IsNullable.ShouldBeFalse();
        nameProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserRoleNameMaxLength);
        nameProperty.IsUnicode()!.Value.ShouldBeFalse();
        nameProperty.IsUniqueIndex().ShouldBeFalse();
    }

    [Fact]
    public void ApplicationUserRoleConfiguration_Should_ImplementDescriptionCorrect()
    {
        // Assert
        var descriptionProperty = _entityType.FindProperty(nameof(ApplicationUserRole.Description));
        descriptionProperty.ShouldNotBeNull();
        descriptionProperty.IsNullable.ShouldBeFalse();
        descriptionProperty.GetMaxLength().ShouldBe(DataSchemeConstants.UserRoleDescriptionLength);
        descriptionProperty.IsUnicode()!.Value.ShouldBeFalse();
        descriptionProperty.IsUniqueIndex().ShouldBeFalse();
    }
}