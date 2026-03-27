

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Tonga.Enumerable;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591
namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> which has been split at the given string.
/// </summary>
public sealed class Split(TextMorph text, TextMorph rgx, bool remBlank = true) : EnumerableEnvelope<string>(
    () =>
    {
        IEnumerable<string> split =
                new Regex(rgx.Str())
                    .Split(text.Str())
                    .AsEnumerable();

        return
            remBlank
                ? split.AsFiltered(str => !String.IsNullOrWhiteSpace(str))
                : split;
    }
)
{
    /// <summary>
    /// A <see cref="IText"/> which has been split at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitespace strings from result or not</param>
    public Split(IText text, IText rgx, bool remBlank = true) : this(
        new TextMorph(text),
        new TextMorph(rgx),
        remBlank
    )
    { }
}

public static partial class TextSmarts
{
    public static IEnumerable<string> SplitBy(this IText text, TextMorph rgx, bool remBlank = true) =>
        new Split(text, rgx, remBlank);

    /// <summary>
    /// A <see cref="IText"/> which has been split at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitespace strings from result or not</param>
    public static IEnumerable<string> SplitBy(this string text, string rgx, bool remBlank = true) =>
        new Split(text, rgx, remBlank);
}
