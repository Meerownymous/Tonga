using System;
using Tonga.Text;
using Xunit;

public static class AssertText
{
    public static void Equal(TextMorph expected, TextMorph actual) =>
        Assert.Equal(expected.Str(), actual.Str());

    public static void Equal(TextMorph expected, TextMorph actual, bool ignoreCase) =>
        Assert.Equal(expected.Str(), actual.Str(), ignoreCase);

    public static void Equal(TextMorph expected, TextMorph actual, StringComparer comparer) =>
        Assert.Equal(expected.Str(), actual.Str(), comparer);

    public static void NotEqual(TextMorph expected, TextMorph actual) =>
        Assert.NotEqual(expected.Str(), actual.Str());

    public static void NotEqual(TextMorph expected, TextMorph actual, StringComparer comparer) =>
        Assert.NotEqual(expected.Str(), actual.Str(), comparer);

    public static void Contains(TextMorph expectedSubstring, TextMorph actualString) =>
        Assert.Contains(expectedSubstring.Str(), actualString.Str());

    public static void Contains(TextMorph expectedSubstring, TextMorph actualString, StringComparison comparisonType) =>
        Assert.Contains(expectedSubstring.Str(), actualString.Str(), comparisonType);

    public static void DoesNotContain(TextMorph expectedSubstring, TextMorph actualString) =>
        Assert.DoesNotContain(expectedSubstring.Str(), actualString.Str());

    public static void DoesNotContain(TextMorph expectedSubstring, TextMorph actualString, StringComparison comparisonType) =>
        Assert.DoesNotContain(expectedSubstring.Str(), actualString.Str(), comparisonType);

    public static void StartsWith(TextMorph expectedStartString, TextMorph actualString) =>
        Assert.StartsWith(expectedStartString.Str(), actualString.Str());

    public static void StartsWith(TextMorph expectedStartString, TextMorph actualString, StringComparison comparisonType) =>
        Assert.StartsWith(expectedStartString.Str(), actualString.Str(), comparisonType);

    public static void EndsWith(TextMorph expectedEndString, TextMorph actualString) =>
        Assert.EndsWith(expectedEndString.Str(), actualString.Str());

    public static void EndsWith(TextMorph expectedEndString, TextMorph actualString, StringComparison comparisonType) =>
        Assert.EndsWith(expectedEndString.Str(), actualString.Str(), comparisonType);

    public static void Matches(TextMorph expectedRegexPattern, TextMorph actualString) =>
        Assert.Matches(expectedRegexPattern.Str(), actualString.Str());

    public static void DoesNotMatch(TextMorph expectedRegexPattern, TextMorph actualString) =>
        Assert.DoesNotMatch(expectedRegexPattern.Str(), actualString.Str());

    public static void Empty(TextMorph actual) =>
        Assert.Empty(actual.Str());

    public static void NotEmpty(TextMorph actual) =>
        Assert.NotEmpty(actual.Str());

    public static void Null(TextMorph actual) =>
        Assert.Null(actual.Str());

    public static void NotNull(TextMorph actual) =>
        Assert.NotNull(actual.Str());
}
