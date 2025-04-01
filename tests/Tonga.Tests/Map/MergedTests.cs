

using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map;

public sealed class JoinedTests
{
    [Fact]
    public void JoinsInputs()
    {
        var joined =
            Merged._(
                AsMap._(AsPair._("A", "I am")),
                AsMap._(AsPair._("B", "trapped in")),
                AsMap._(AsPair._("C", "a dictionary"))
            );

        Assert.Equal(
            "I am trapped in a dictionary",
            $"{joined["A"]} {joined["B"]} {joined["C"]}"
        );
    }

    [Fact]
    public void ReplacesExisting()
    {
        var map =
            Merged._(
                AsMap._(AsPair._("A", "Hakuna")),
                AsMap._(AsPair._("B", "Matata")),
                AsMap._(AsPair._("B", "Banana"))
            );

        Assert.Equal(
            "Banana",
            map["B"]
        );
    }

    [Fact]
    public void JoinsInputsTypedValue()
    {
        var dict =
            Merged._(
                AsMap._(AsPair._("A", 89)),
                AsMap._(AsPair._("B", 17)),
                AsMap._(AsPair._("C", 8))
            );

        Assert.Equal(
            "89 17 8",
            $"{dict["A"]} {dict["B"]} {dict["C"]}"
        );
    }

    [Fact]
    public void ReplacesExistingTypedValue()
    {
        var dict =
            Merged._(
                AsMap._(AsPair._("A", 1)),
                AsMap._(AsPair._("B", 4)),
                AsMap._(AsPair._("B", 19))
            );

        Assert.Equal(
            19,
            dict["B"]
        );
    }

    [Fact]
    public void JoinsInputsTypedKeyValue()
    {
        var dict =
            Merged._(
                AsMap._(AsPair._(0, 1)),
                AsMap._(AsPair._(1, 3)),
                AsMap._(AsPair._(2, 37))
            );

        Assert.Equal(
            "1337",
            $"{dict[0]}{dict[1]}{dict[2]}"
        );
    }

    [Fact]
    public void ReplacesExistingTypedKeyValue()
    {
        var dict =
            Merged._(
                AsMap._(AsPair._(0, 1)),
                AsMap._(AsPair._(0, 4)),
                AsMap._(AsPair._(0, 19))
            );

        Assert.Equal(
            19,
            dict[0]
        );
    }

    [Fact]
    public void JoinsLazy()
    {
        var map =
            Merged._(
                AsMap._(AsPair._("A", () => "I am")),
                AsMap._(AsPair._("B", () => "trapped in")),
                AsMap._(AsPair._("C", "a dictionary"))
            );

        Assert.Equal(
            "I am trapped in a dictionary",
            $"{map["A"]} {map["B"]} {map["C"]}"
        );
    }
}
