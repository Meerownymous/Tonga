using Tonga.Enumerable;
using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact;

public sealed class HasAnyTests
{
    [Fact]
    public void KnowsTrue()
    {
        Assert.True(
            new HasAny<int>(
                new AsEnumerable<int>(1)
            ).IsTrue()
        );
    }

    [Fact]
    public void KnowsFalse()
    {
        Assert.False(
            new HasAny<int>(
                new None<int>()
            ).IsTrue()
        );
    }
}
