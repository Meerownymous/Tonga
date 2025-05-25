

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
            ).ToInt() == 1);
    }

    [Fact]
    public void FindsMinOfIntsAsFloat()
    {

        Assert.True(
            new MinOf(
                1, 2, 3, 4
            ).ToFloat() == 1F);
    }

    [Fact]
    public void FindsMinOfIntsAsLong()
    {

        Assert.True(
            new MinOf(
                1, 2, 3, 4
            ).ToLong() == 1L);
    }

    [Fact]
    public void FindsMinOfIntsAsDouble()
    {

        Assert.True(
            new MinOf(
                1, 2, 3, 4
            ).ToInt() == 1D);
    }

    [Fact]
    public void FindsMinOfFloatsAsInt()
    {

        Assert.True(
            new MinOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).ToInt() == 1);
    }

    [Fact]
    public void FindsMinOfFloatsAsFloat()
    {

        Assert.True(
            new MinOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).ToFloat() == 1.2F);
    }

    [Fact]
    public void FindsMinOfFloatsAsLong()
    {

        Assert.True(
            new MinOf(
                1.2F, 2.1F, 3.6F, 4.9F
            ).ToLong() == 1L);
    }

    [Fact]
    public void FindsMinOfFloatsAsDouble()
    {

        Assert.True(
            new MinOf(
                1.0D, 2.0D, 3.0D, 4.0D
            ).ToDouble() == 1.0D);
    }

    [Fact]
    public void FindsMinOfDoublesAsInt()
    {

        Assert.True(
            new MinOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).ToInt() == 1);
    }

    [Fact]
    public void FindsMinOfDoublesAsFloat()
    {

        Assert.True(
            new MinOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).ToFloat() == 1.2F);
    }

    [Fact]
    public void FindsMinOfDoublesAsLong()
    {

        Assert.True(
            new MinOf(
                1.2D, 2.1D, 3.6D, 4.9D
            ).ToLong() == 1L);
    }

    [Fact]
    public void FindsMinOfDoublesAsDouble()
    {

        Assert.True(
            new MinOf(
                1.0D, 2.0D, 3.0D, 4.0D
            ).ToDouble() == 1.0D);
    }

    [Fact]
    public void FindsMinOfLongsAsInt()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).ToInt() == 1);
    }

    [Fact]
    public void FindsMinOfLongsAsFloat()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).ToFloat() == 1.0F);
    }

    [Fact]
    public void FindsMinOfLongsAsLong()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).ToLong() == 1L);
    }

    [Fact]
    public void FindsMinOfLongsAsDouble()
    {

        Assert.True(
            new MinOf(
                1L, 2L, 3L, 4L
            ).ToDouble() == 1.0D);
    }
}
