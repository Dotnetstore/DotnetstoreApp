using Dotnetstore.Intranet.Organization.Roles;
using Dotnetstore.Intranet.TestHelper;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Roles;

public class ApplicationUserRoleIdTests
{
    [Fact]
    public void Create_Should_ReturnNewId()
        => ValueObjectTestHelper.TestCreate(ApplicationUserRoleId.Create, x => x.Value);
    
    [Fact]
    public void Create_WithGuid_Should_ReturnIdWithSpecifiedValue()
        => ValueObjectTestHelper.TestCreateWithGuid(ApplicationUserRoleId.Create, x => x.Value);
    
    [Fact]
    public void ToString_Should_ReturnStringRepresentationOfValue()
        => ValueObjectTestHelper.TestToString(ApplicationUserRoleId.Create, x => x.Value);
    
    [Fact]
    public void Equality_Should_BeBasedOnValue()
        => ValueObjectTestHelper.TestEquality(ApplicationUserRoleId.Create, ApplicationUserRoleId.Create, x => x.Value);
    
    [Fact]
    public void GetHashCode_Should_ReturnHashBasedOnValue()
        => ValueObjectTestHelper.TestGetHashCode(ApplicationUserRoleId.Create);
}