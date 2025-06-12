using System;
using Tonga.Number;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Number;

public sealed class AsNumberTest
{
    [Fact]
    public void ParsesText()
    {
        Assert.Equal(
            4673.453,
            new AsNumber("4673.453").Double()
        );
    }

    [Fact]
    public void ParsesFloat()
    {
        Assert.Equal(
            4673.453F,
            new AsNumber(4673.453F).Float()
        );
    }

    [Fact]
    public void RejectsNoFloatText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").Float()
        );
    }

    [Fact]
    public void ParsesInt()
    {
        Assert.True(
            new AsNumber(1337).Int() == 1337
        );
    }

    [Fact]
    public void RejectsNoIntText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").Int()
        );
    }

    [Fact]
    public void ParsesDouble()
    {
        Assert.True(
            new AsNumber(843.23969274001D).Double() == 843.23969274001D
        );
    }

    [Fact]
    public void RejectsNoDoubleText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").Double()
        );
    }

    [Fact]
    public void ParsesLong()
    {
        Assert.True(
            new AsNumber(139807814253711).Long() == 139807814253711L
        );
    }

    [Fact]
    public void RejectsNoLongText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").Long()
        );
    }

    [Fact]
    public void IntToDouble()
    {
        Assert.True(
            new AsNumber(
                5
            ).Double() == 5d
        );
    }

    [Fact]
    public void DoubleToFloat()
    {
        Assert.True(
            new AsNumber(
                (551515155.451d)
            ).Float() == 551515155.451f
        );
    }

    [Fact]
    public void FloatAsDouble()
    {
        Assert.True(
            new AsNumber(
                (5.243)
            ).Double() == 5.243d
        );
    }

    [Fact]
    public void LongAsInt()
    {
        Assert.True(
            new AsNumber(
                (50L)
            ).Int() == 50
        );
    }

    [Fact]
    public void IntAsLong()
    {
        Assert.True(
            new AsNumber(
                (50)
            ).Long() == 50L
        );
    }

    [Fact]
    public void DoubleSeperator()
    {
        Assert.True(
            new AsNumber(
                "10.100,11",
                ",",
                "."
            ).Double() == 10100.11
        );
    }

    [Fact]
    public void StringAsInt()
    {
        Assert.True(
            new AsNumber(
                "100"
            ).Int() == 100
        );
    }
}
