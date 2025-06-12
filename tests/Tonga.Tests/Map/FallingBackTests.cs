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
            (3, 42).AsMap()
                .AsBackFalling(_ => throw new Exception("this should not be thrown in this test"))
            [3]
        );
    }

    [Fact]
    public void DeliversValueFromFallbackFunction()
    {
        Assert.Equal(
            4,
            new Empty<int,int>()
                .AsBackFalling(key => key * 2)[2]
        );
    }

    [Fact]
    public void DeliversValueFromFallbackMap()
    {
        Assert.Equal(
            37,
            (7, 42)
                .AsMap()
                .AsBackFalling(
                    (13, 37).AsMap()
                )[13]
        );
    }

    [Fact]
    public void DoesNotGetValueWhenAlsoMissingInFallbackMap()
    {
        var map =
            (7, 42).AsMap()
                .AsBackFalling((13, 37).AsMap());

        Assert.Throws<ArgumentException>(() => map[666]);
    }
}
