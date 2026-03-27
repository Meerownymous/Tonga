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
                3.2d,
                3.2d
            ).IsTrue()
        );
    }

    [Fact]
    public void IsFalseOnDifferentLength()
    {
        Assert.False(
            new IsEqual(
                1,
                3.2d
            ).IsTrue()
        );
    }

    [Fact]
    public void IsFalseOnDifferentBytes()
    {
        Assert.False(
            new IsEqual(
                1,
                3.2d
            ).IsTrue()
        );
    }
}
