using Dotnetstore.Intranet.Organization.Users;
using Dotnetstore.Intranet.TestHelper;
using Xunit;

namespace Dotnetstore.Intranet.Organization.Tests.Users;

public class ApplicationUserIdTests
{
    [Fact]
    public void Create_Should_ReturnNewId()
        => ValueObjectTestHelper.TestCreate(ApplicationUserId.Create, x => x.Value);
    
    [Fact]
    public void Create_WithGuid_Should_ReturnIdWithSpecifiedValue()
        => ValueObjectTestHelper.TestCreateWithGuid(ApplicationUserId.Create, x => x.Value);
    
    [Fact]
    public void ToString_Should_ReturnStringRepresentationOfValue()
        => ValueObjectTestHelper.TestToString(ApplicationUserId.Create, x => x.Value);
    
    [Fact]
    public void Equality_Should_BeBasedOnValue()
        => ValueObjectTestHelper.TestEquality(ApplicationUserId.Create, ApplicationUserId.Create, x => x.Value);
    
    [Fact]
    public void GetHashCode_Should_ReturnHashBasedOnValue()
        => ValueObjectTestHelper.TestGetHashCode(ApplicationUserId.Create);
}