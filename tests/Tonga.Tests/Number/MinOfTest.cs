

using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Number;

public sealed class MinOfTest
{
    [Fact]
    public void FindsMinOfIntsAsInt()
    {

        Assert.True(
            new MinOf(
                1, 2, 3, 4
            ).Int() == 1);
    }

    [Fact]
    public void FindsMinOfIntsAsFloat()
    {

        Assert.True(
            new MinOf(
                1, 2, 3, 4
            ).Float() == 1F);
    }

    [Fact]
    public void FindsMinOfIntsAsLong()
    {

        Assert.True(
            new MinOf(
                1, 2, 3, 4
            ).Long() == 1L);
    }

    [Fact]
    public void FindsMinOfIntsAsDouble()
    {

        Assert.True(
            new MinOf(
                1, 2, 3, 4
            ).Int() == 1D);
    }

    [Fact]
    public void FindsMinOfFloatsAsInt()
    {

        Assert.True(
            new MinOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).Int() == 1);
    }

    [Fact]
    public void FindsMinOfFloatsAsFloat()
    {

        Assert.True(
            new MinOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).Float() == 1.2F);
    }

    [Fact]
    public void FindsMinOfFloatsAsLong()
    {

        Assert.True(
            new MinOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).Long() == 1L);
    }

    [Fact]
    public void FindsMinOfFloatsAsDouble()
    {

        Assert.True(
            new MinOf(
                1.0D, 2.0D, 3.0D, 4.0D
            ).Double() == 1.0D);
    }

    [Fact]
    public void FindsMinOfDoublesAsInt()
    {

        Assert.True(
            new MinOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).Int() == 1);
    }

    [Fact]
    public void FindsMinOfDoublesAsFloat()
    {

        Assert.True(
            new MinOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).Float() == 1.2F);
    }

    [Fact]
    public void FindsMinOfDoublesAsLong()
    {

        Assert.True(
            new MinOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).Long() == 1L);
    }

    [Fact]
    public void FindsMinOfDoublesAsDouble()
    {

        Assert.True(
            new MinOf(
                1.0D, 2.0D, 3.0D, 4.0D
            ).Double() == 1.0D);
    }

    [Fact]
    public void FindsMinOfLongsAsInt()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).Int() == 1);
    }

    [Fact]
    public void FindsMinOfLongsAsFloat()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).Float() == 1.0F);
    }

    [Fact]
    public void FindsMinOfLongsAsLong()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).Long() == 1L);
    }

    [Fact]
    public void FindsMinOfLongsAsDouble()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).Double() == 1.0D);
    }
}
