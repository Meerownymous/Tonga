using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact;

public sealed class TrueTest
{
    [Fact]
    public void AsValue()
    {
        Assert.True(
            new True().IsTrue()
        );

    }
}
