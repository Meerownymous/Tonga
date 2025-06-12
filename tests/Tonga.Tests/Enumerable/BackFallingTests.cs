using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable;

public sealed class BackFallingTests
{
    [Fact]
    public void DeliversFallbackIfSourceEmpty()
    {
        Assert.Equal(
            1137,
            new BackFalling<int>(
                new None<int>(),
                1137
            )
            .First()
            .Value()
        );
    }
}
