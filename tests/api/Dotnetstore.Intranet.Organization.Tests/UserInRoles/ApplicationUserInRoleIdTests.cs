using Dotnetstore.Intranet.Organization.UserInRoles;
using Dotnetstore.Intranet.TestHelper;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.UserInRoles;

public class ApplicationUserInRoleIdTests
{
    [Fact]
    public void Create_Should_ReturnNewId()
        => ValueObjectTestHelper.TestCreate(ApplicationUserInRoleId.Create, x => x.Value);
    
    [Fact]
    public void Create_WithGuid_Should_ReturnIdWithSpecifiedValue()
        => ValueObjectTestHelper.TestCreateWithGuid(ApplicationUserInRoleId.Create, x => x.Value);
    
    [Fact]
    public void ToString_Should_ReturnStringRepresentationOfValue()
        => ValueObjectTestHelper.TestToString(ApplicationUserInRoleId.Create, x => x.Value);
    
    [Fact]
    public void Equality_Should_BeBasedOnValue()
        => ValueObjectTestHelper.TestEquality(ApplicationUserInRoleId.Create, ApplicationUserInRoleId.Create, x => x.Value);
    
    [Fact]
    public void GetHashCode_Should_ReturnHashBasedOnValue()
        => ValueObjectTestHelper.TestGetHashCode(ApplicationUserInRoleId.Create);
}