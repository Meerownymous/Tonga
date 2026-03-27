using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable;

public sealed class IsEmptyTests
{
    [Fact]
    public void KnowsTrue()
    {
        Assert.True(
            1.AsSingle()
                .IsEmpty()
                .IsTrue()
        );
    }

    [Fact]
    public void KnowsFalse()
    {
        Assert.False(
            new Empty<int>()
                .IsEmpty()
                .IsTrue()
        );
    }
}
