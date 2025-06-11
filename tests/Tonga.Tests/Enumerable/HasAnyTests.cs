using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable;

public sealed class HasAnyTests
{
    [Fact]
    public void KnowsTrue()
    {
        Assert.True(
            1.AsSingle()
                .HasAny()
                .IsTrue()
        );
    }

    [Fact]
    public void KnowsFalse()
    {
        Assert.False(
            new None<int>()
                .HasAny()
                .IsTrue()
        );
    }
}
