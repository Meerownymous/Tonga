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
            new AsNumber(AsText._("4673.453")).ToDouble()
        );
    }

    [Fact]
    public void ParsesFloat()
    {
        Assert.True(
            new AsNumber(4673.453F).ToFloat() == 4673.453F
        );
    }

    [Fact]
    public void RejectsNoFloatText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").ToFloat()
        );
    }

    [Fact]
    public void ParsesInt()
    {
        Assert.True(
            new AsNumber(1337).ToInt() == 1337
        );
    }

    [Fact]
    public void RejectsNoIntText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").ToInt()
        );
    }

    [Fact]
    public void ParsesDouble()
    {
        Assert.True(
            new AsNumber(843.23969274001D).ToDouble() == 843.23969274001D
        );
    }

    [Fact]
    public void RejectsNoDoubleText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").ToDouble()
        );
    }

    [Fact]
    public void ParsesLong()
    {
        Assert.True(
            new AsNumber(139807814253711).ToLong() == 139807814253711L
        );
    }

    [Fact]
    public void RejectsNoLongText()
    {
        Assert.Throws<ArgumentException>(() =>
            new AsNumber("ghki").ToLong()
        );
    }

    [Fact]
    public void IntToDouble()
    {
        Assert.True(
            new AsNumber(
                5
            ).ToDouble() == 5d
        );
    }

    [Fact]
    public void DoubleToFloat()
    {
        Assert.True(
            new AsNumber(
                (551515155.451d)
            ).ToFloat() == 551515155.451f
        );
    }

    [Fact]
    public void FloatAsDouble()
    {
        Assert.True(
            new AsNumber(
                (5.243)
            ).ToDouble() == 5.243d
        );
    }

    [Fact]
    public void LongAsInt()
    {
        Assert.True(
            new AsNumber(
                (50L)
            ).ToInt() == 50
        );
    }

    [Fact]
    public void IntAsLong()
    {
        Assert.True(
            new AsNumber(
                (50)
            ).ToLong() == 50L
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
            ).ToDouble() == 10100.11
        );
    }

    [Fact]
    public void StringAsInt()
    {
        Assert.True(
            new AsNumber(
                "100"
            ).ToInt() == 100
        );
    }
}
