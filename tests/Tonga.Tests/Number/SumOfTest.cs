using Tonga.Enumerable;
using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Number;

public sealed class SumOfTest
{
    [Fact]
    public void CalculatesSumOfNumbers()
    {
        Assert.True(
            new SumOf(
                1.5F, 2.5F, 3.5F
            ).ToFloat() == 7.5F);
    }

    [Fact]
    public void CalculatesSumOfEnumerables()
    {
        Assert.True(
            new SumOf(
                (1.5F, 2.5F, 3.5F).AsEnumerable()
            ).ToFloat() == 7.5F
        );
    }
}
