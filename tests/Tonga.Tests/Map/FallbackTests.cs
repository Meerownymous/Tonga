using System;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public class FallbackMapTests
{
    [Fact]
    public void DeliversExistingValue()
    {
        Assert.Equal(
            42,
            Fallback._(
                AsMap._(
                    AsPair._(3, 42)
                ),
                (key) => throw new Exception("this should not be thrown in this tyest")
            )[3]
        );
    }

    [Fact]
    public void DeliversValueFromFallbackFunction()
    {
        Assert.Equal(
            4,
            Fallback._(
                Empty._<int,int>(),
                key => key * 2
            )[2]
        );
    }

    [Fact]
    public void DeliversValueFromFallbackMap()
    {
        Assert.Equal(
            37,
            Fallback._(
                AsMap._(
                    AsPair._(7, 42)
                ),
                AsMap._(
                    AsPair._(13, 37)
                )
            )[13]
        );
    }

    [Fact]
    public void DoesNotGetValueWhenAlsoMissingInFallbackMap()
    {
        var map =
            Fallback._(
                AsMap._(
                    AsPair._(7, 42)
                ),
                AsMap._(
                    AsPair._(13, 37)
                )
            );

        Assert.Throws<ArgumentException>(() => map[666]);
    }
}
