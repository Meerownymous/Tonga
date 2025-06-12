using Tonga.Bytes;
using Xunit;

namespace Tonga.Tests.Bytes;

public sealed class IsEqualTest
{
    [Fact]
    public void IsTrueOnEqualBytes()
    {
        Assert.True(
            new IsEqual(
                3.2d.AsBytes(),
                3.2d.AsBytes()
            ).IsTrue()
        );
    }

    [Fact]
    public void IsFalseOnDifferentLength()
    {
        Assert.False(
            new IsEqual(
                new AsBytes(1),
                new AsBytes(3.2d)
            ).IsTrue()
        );
    }

    [Fact]
    public void IsFalseOnDifferentBytes()
    {
        Assert.False(
            new IsEqual(
                1.AsBytes(),
                3.2d.AsBytes()
            ).IsTrue()
        );
    }
}
