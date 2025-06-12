using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Number;

public sealed class IsSimilarTest
{
    [Fact]
    public void IsSimilarSame()
    {
        INumber first =
            new AsNumber(13.37);
        INumber second =
            new AsNumber(13.37);

        Assert.True(
            new IsSimilar(first, second).IsTrue()
        );
    }

    [Fact]
    public void IsSimilarNoDecimal()
    {
        INumber first =
            new AsNumber(13);
        INumber second =
            new AsNumber(13);

        Assert.True(
            new IsSimilar(first, second, 10).IsTrue()
        );
    }

    [Fact]
    public void IsSimilarOneDecimal()
    {
        INumber first =
            new AsNumber(13.37);
        INumber second =
            new AsNumber(13.3);

        Assert.True(
            new IsSimilar(first, second, 1).IsTrue()
        );
    }

    [Fact]
    public void IsSimilarOnlyOneDecimal()
    {
        INumber first =
            new AsNumber(13.37777);
        INumber second =
            new AsNumber(13.33333);

        Assert.True(
            new IsSimilar(first, second, 1).IsTrue()
        );
    }

    [Fact]
    public void IsSimilarFiveDecimal()
    {
        INumber first =
            new AsNumber(13.333337);
        INumber second =
            new AsNumber(13.333339);

        Assert.True(
            new IsSimilar(first, second, 5).IsTrue()
        );
    }

    [Fact]
    public void IsSimilar10Decimal()
    {
        INumber first =
            new AsNumber(13.33333333337);
        INumber second =
            new AsNumber(13.33333333339);

        Assert.True(
            new IsSimilar(first, second, 10).IsTrue()
        );
    }

    [Fact]
    public void IsNotSimilar()
    {
        INumber first =
            new AsNumber(13.37);
        INumber second =
            new AsNumber(13.39);

        Assert.False(
            new IsSimilar(first, second, 2).IsTrue()
        );
    }
}
