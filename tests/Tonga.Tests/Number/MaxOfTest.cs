

using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Number;

public sealed class MaxOfTest
{
    [Fact]
    public void FindsMaxOfIntsAsInt()
    {

        Assert.True(
            new MaxOf(
                1, 2, 3, 4
            ).Int() == 4);
    }

    [Fact]
    public void FindsMaxOfIntsAsFloat()
    {

        Assert.True(
            new MaxOf(
                1, 2, 3, 4
            ).Float() == 4F);
    }

    [Fact]
    public void FindsMaxOfIntsAsLong()
    {

        Assert.True(
            new MaxOf(
                1, 2, 3, 4
            ).Long() == 4L);
    }

    [Fact]
    public void FindsMaxOfIntsAsDouble()
    {

        Assert.True(
            new MaxOf(
                1, 2, 3, 4
            ).Int() == 4D);
    }

    [Fact]
    public void FindsMaxOfFloatsAsInt()
    {

        Assert.True(
            new MaxOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).Int() == 4);
    }

    [Fact]
    public void FindsMaxOfFloatsAsFloat()
    {

        Assert.True(
            new MaxOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).Float() == 4.9F);
    }

    [Fact]
    public void FindsMaxOfFloatsAsLong()
    {

        Assert.True(
            new MaxOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).Long() == 4L);
    }

    [Fact]
    public void FindsMaxOfFloatsAsDouble()
    {

        Assert.True(
            new MaxOf(
                1.0D, 2.0D, 3.0D, 4.0D
            ).Double() == 4.0D);
    }

    [Fact]
    public void FindsMaxOfDoublesAsInt()
    {

        Assert.True(
            new MaxOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).Int() == 4);
    }

    [Fact]
    public void FindsMaxOfDoublesAsFloat()
    {

        Assert.True(
            new MaxOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).Float() == 4.9F);
    }

    [Fact]
    public void FindsMaxOfDoublesAsLong()
    {

        Assert.True(
            new MaxOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).Long() == 4L);
    }

    [Fact]
    public void FindsMaxOfDoublesAsDouble()
    {

        Assert.True(
            new MaxOf(
                1.0D, 2.0D, 3.0D, 4.0D
            ).Double() == 4.0D);
    }

    [Fact]
    public void FindsMaxOfLongsAsInt()
    {

        Assert.True(
            new MaxOf(
                1L, 2L, 3L, 4L
            ).Int() == 4);
    }

    [Fact]
    public void FindsMaxOfLongsAsFloat()
    {

        Assert.True(
            new MaxOf(
                1L, 2L, 3L, 4L
            ).Float() == 4.0F);
    }

    [Fact]
    public void FindsMaxOfLongsAsLong()
    {

        Assert.True(
            new MaxOf(
                1L, 2L, 3L, 4L
            ).Long() == 4L);
    }

    [Fact]
    public void FindsMaxOfLongsAsDouble()
    {

        Assert.True(
            new MaxOf(
                1L, 2L, 3L, 4L
            ).Double() == 4.0D);
    }
}
