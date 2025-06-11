using Tonga.Enumerable;
using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Number;

public sealed class AverageTest
{
    [Fact]
    public void IsZeroForEmptyCollection()
    {
        Assert.True(
            new Average(
                new None<long>()
            ).ToLong() == 0L);
    }

    [Fact]
    public void CalculatesAvgIntOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).ToInt() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).ToLong() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).ToDouble() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).ToFloat() == 2.5F);
    }

    [Fact]
    public void CalculatesAvgIntOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).ToInt() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).ToLong() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).ToDouble() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).ToFloat() == 2.5F);
    }

    [Fact]
    public void CalculatesAvgIntOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).ToInt() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).ToLong() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).ToDouble() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).ToFloat() == 2.5F);
    }

    [Fact]
    public void CalculatesAvgIntOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).ToInt() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).ToLong() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).ToDouble() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).ToFloat() == 2.5F);
    }
}
