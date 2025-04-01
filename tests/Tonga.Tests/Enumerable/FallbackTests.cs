using System.Linq;
using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable;

public sealed class FallbackTests
{
    [Fact]
    public void DeliversFallbackIfSourceEmpty()
    {
        Assert.Equal(
            1137,
            new Fallback<int>(
                    new None<int>(), 1137
            )
            .First()
        );
    }
}
