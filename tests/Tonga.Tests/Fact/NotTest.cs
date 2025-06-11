using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact;

public sealed class NotTest
{
    [Fact]
    public void NegatesTrue()
    {
        Assert.False(
            new Not(
                new True()
            ).IsTrue()
        );
    }

    [Fact]
    public void NegatesFalse()
    {
        Assert.True(
            new Not(
                new False()
            ).IsTrue()
        );
    }
}
