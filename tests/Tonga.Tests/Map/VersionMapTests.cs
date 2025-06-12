using System;
using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class VersionMapTests
{
    [Fact]
    public void MatchesLowerEnd()
    {
        Assert.Equal(
            "ainz",
            (
                (new Version(1, 0, 0, 0), "ainz").AsPair(),
                (new Version(2, 0, 0, 0), "zway").AsPair()
            ).AsVersionMap()[new Version(1, 0, 0, 0)]
        );
    }

    [Fact]
    public void MatchesKeyBetween()
    {
        Assert.Equal(
            "ainz",
            (
                (new Version(1, 0, 0, 0), "ainz"),
                (new Version(5, 0, 0, 0), "zway")
            ).AsVersionMap()[new Version(2, 0, 0, 0)]
        );
    }

    [Fact]
    public void MatchesOpenEnd()
    {
        Assert.Equal(
            "zway",
            (
                (new Version(1, 0, 0, 0), "ainz"),
                (new Version(5, 0, 0, 0), "zway")
            ).AsVersionMap()[new Version(10, 0, 0, 0)]
        );
    }

    [Fact]
    public void RejectsOverClosedEnd()
    {
        Assert.Throws<ArgumentException>(() =>
            (
                (new Version(1, 0, 0, 0), "ainz"),
                (new Version(5, 0, 0, 0), "zway")
            ).AsVersionMap(false)[new Version(10, 0, 0, 0)]
        );
    }

    [Fact]
    public void MatchesWithinClosedEnd()
    {
        Assert.Equal(
            "ainz",
            (
                (new Version(1, 0, 0, 0), "ainz"),
                (new Version(5, 0, 0, 0), "zway")
            ).AsVersionMap(false)[new Version(2, 0, 0, 0)]
        );
    }

    [Fact]
    public void MatchesEndingZero()
    {
        Assert.Equal(
            "ainz",
            (
                (new Version(1, 0, 0, 0), "ainz"),
                (new Version(5, 0, 0, 0), "zway")
            ).AsVersionMap(false)[new Version(1, 0)]
        );
    }

    [Fact]
    public void MatchesMajorMinorVersion()
    {
        Assert.Equal(
            "zway",
            (
                (new Version(1, 0, 0, 0), "ainz"),
                (new Version(5, 0, 0, 0), "zway")
            ).AsVersionMap()[new Version(5, 0)]
        );
    }
}
