using Shouldly;

namespace Dotnetstore.Intranet.TestHelper;

public static class ValueObjectTestHelper
{
    public static void TestCreate<T>(Func<T> createFunc, Func<T, Guid> getValue)
    {
        var obj = createFunc();
        var value = getValue(obj);
        value.ShouldNotBe(Guid.Empty);
        value.Version.ShouldBe(7);
        obj.ShouldBeOfType<T>();
    }

    public static void TestCreateWithGuid<T>(Func<Guid, T> createFunc, Func<T, Guid> getValue)
    {
        var guid = Guid.NewGuid();
        var obj = createFunc(guid);
        getValue(obj).ShouldBe(guid);
        obj.ShouldBeOfType<T>();
    }
    
    public static void TestToString<T>(Func<T> createFunc, Func<T, Guid> getValue)
    {
        var obj = createFunc();
        var result = obj!.ToString();
        result.ShouldBe(getValue(obj).ToString());
    }

    public static void TestEquality<T>(Func<T> createFunc, Func<Guid, T> createWithGuid, Func<T, Guid> getValue)
    {
        var guid = Guid.NewGuid();
        var obj1 = createWithGuid(guid);
        var obj2 = createWithGuid(guid);

        obj1.ShouldBe(obj2);
        obj1!.Equals(obj2).ShouldBeTrue();
    }

    public static void TestGetHashCode<T>(Func<Guid, T> createWithGuid)
    {
        var guid = Guid.NewGuid();
        var obj1 = createWithGuid(guid);
        var obj2 = createWithGuid(guid);

        obj1!.GetHashCode().ShouldBe(obj2!.GetHashCode());
    }
}