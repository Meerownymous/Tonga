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
            ).Long() == 0L);
    }

    [Fact]
    public void CalculatesAvgIntOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).Int() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).Long() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).Double() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfInts()
    {
        Assert.True(
            new Average(1, 2, 3, 4).Float() == 2.5F);
    }

    [Fact]
    public void CalculatesAvgIntOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).Int() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).Long() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).Double() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfDoubles()
    {
        Assert.True(
            new Average(1D, 2D, 3D, 4D).Float() == 2.5F);
    }

    [Fact]
    public void CalculatesAvgIntOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).Int() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).Long() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).Double() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfLongs()
    {
        Assert.True(
            new Average(1L, 2L, 3L, 4L).Float() == 2.5F);
    }

    [Fact]
    public void CalculatesAvgIntOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).Int() == 2);
    }

    [Fact]
    public void CalculatesAvgLongOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).Long() == 2L);
    }

    [Fact]
    public void CalculatesAvgDoubleOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).Double() == 2.5D);
    }

    [Fact]
    public void CalculatesAvgFloatOfFloats()
    {
        Assert.True(
            new Average(1F, 2F, 3F, 4F).Float() == 2.5F);
    }
}
