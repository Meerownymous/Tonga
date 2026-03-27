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
                new Empty<int>(),
                1137
            )
            .FirstOne()
            .Value()
        );
    }
}
