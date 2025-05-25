using System;
using System.Collections.Generic;
using Tonga.Number;
using Xunit;

namespace Tonga.Tests.Number;

public sealed class LiveNumberTests
{
    [Fact]
    public void ConversionIsLive()
    {
        var text = "gkb";
        var number = new LiveNumber(() => text);
        text = "42";
        Assert.Equal(42, number.ToInt());
    }

    [Fact]
    public void ParsesFloat()
    {
        Assert.True(
            new LiveNumber(() => 4673.453F).ToFloat() == 4673.453F
        );
    }

    [Fact]
    public void RejectsNoFloatText()
    {
        Assert.Throws<ArgumentException>(() =>
            new LiveNumber(() => "ghki").ToFloat()
        );
    }

    [Fact]
    public void ParsesInt()
    {
        Assert.True(
            new LiveNumber(() => 1337).ToInt() == 1337
        );
    }

    [Fact]
    public void RejectsNoIntText()
    {
        Assert.Throws<ArgumentException>(() =>
            new LiveNumber(() => "ghki").ToInt()
        );
    }

    [Fact]
    public void ParsesDouble()
    {
        Assert.True(
            new LiveNumber(() => 843.23969274001D).ToDouble() == 843.23969274001D
        );
    }

    [Fact]
    public void RejectsNoDoubleText()
    {
        Assert.Throws<ArgumentException>(() =>
            new LiveNumber(() => "ghki").ToDouble()
        );
    }

    [Fact]
    public void ParsesLong()
    {
        Assert.True(
            new LiveNumber(() => 139807814253711).ToLong() == 139807814253711L
        );
    }

    [Fact]
    public void RejectsNoLongText()
    {
        Assert.Throws<ArgumentException>(() =>
            new LiveNumber(() => "ghki").ToLong()
        );
    }

    [Fact]
    public void IntToDouble()
    {
        Assert.True(
            new LiveNumber(
                () => 5
            ).ToDouble() == 5d
        );
    }

    [Fact]
    public void DoubleToFloat()
    {
        Assert.True(
            new LiveNumber(
                () => (551515155.451d)
            ).ToFloat() == 551515155.451f
        );
    }

    [Fact]
    public void FloatAsDouble()
    {
        Assert.True(
            new LiveNumber(
                () => (5.243)
            ).ToDouble() == 5.243d
        );
    }

    [Fact]
    public void AveragesLiveNumbers()
    {
        var list = new List<int>() { 1, 2, 3 };
        var liveAverage = new LiveNumber(() => new Average(list));
        var first = liveAverage.ToDouble();
        list.Add(4);
        Assert.NotEqual(first, liveAverage.ToDouble());
    }
}
