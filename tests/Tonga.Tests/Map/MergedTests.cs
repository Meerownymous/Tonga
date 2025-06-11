

using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class JoinedTests
{
    [Fact]
    public void JoinsInputs()
    {
        var merged =
        (
            ("A", "I am").AsMap(),
            ("B", "trapped in").AsMap(),
            ("C", "a dictionary").AsMap()
        ).AsMerged();

        Assert.Equal(
            "I am trapped in a dictionary",
            $"{merged["A"]} {merged["B"]} {merged["C"]}"
        );
    }

    [Fact]
    public void ReplacesExisting()
    {
        var merged =
        (
            ("A", "Hakuna").AsMap(),
            ("B", "Matata").AsMap(),
            ("B", "Banana").AsMap()
        ).AsMerged();

        Assert.Equal(
            "Banana",
            merged["B"]
        );
    }

    [Fact]
    public void JoinsInputsTypedValue()
    {
        var merged =
        (
            ("A", 89).AsMap(),
            ("B", 17).AsMap(),
            ("C", 8).AsMap()
        ).AsMerged();

        Assert.Equal(
            "89 17 8",
            $"{merged["A"]} {merged["B"]} {merged["C"]}"
        );
    }

    [Fact]
    public void ReplacesExistingTypedValue()
    {
        Assert.Equal(
            19,
            (
                ("A", 1).AsMap(),
                ("B", 4).AsMap(),
                ("B", 19).AsMap()
            ).AsMerged()["B"]
        );
    }

    [Fact]
    public void JoinsInputsTypedKeyValue()
    {
        var merged =
        (
            (0, 1).AsMap(),
            (1, 3).AsMap(),
            (2, 37).AsMap()
        ).AsMerged();

        Assert.Equal(
            "1337",
            $"{merged[0]}{merged[1]}{merged[2]}"
        );
    }

    [Fact]
    public void ReplacesExistingTypedKeyValue()
    {
        var dict =
        (
            (0, 1).AsMap(),
            (0, 4).AsMap(),
            (0, 19).AsMap()
        ).AsMerged();

        Assert.Equal(
            19,
            dict[0]
        );
    }

    [Fact]
    public void JoinsLazy()
    {
        var map =
        (
            "A".AsPair(() => "I am").AsMap(),
            "B".AsPair(() => "trapped in").AsMap(),
            ("C", "a dictionary").AsMap()
        ).AsMerged();

        Assert.Equal(
            "I am trapped in a dictionary",
            $"{map["A"]} {map["B"]} {map["C"]}"
        );
    }
}
