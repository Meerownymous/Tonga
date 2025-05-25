

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Tonga.Enumerable;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591
namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> which has been splitted at the given string.
/// </summary>
public sealed class Split(IText text, IText rgx, bool remBlank = true) : EnumerableEnvelope<string>(
    () =>
    {
        IEnumerable<string> split =
            AsEnumerable._(
                new Regex(rgx.Str())
                    .Split(text.Str())
            );

        return
            remBlank ?
                Filtered._(
                    (str) => !String.IsNullOrWhiteSpace(str),
                    split
                )
                :
                split;
    }
)
{
    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
    public Split(String text, String rgx, bool remBlank = true) : this(
        text.AsText(),
        rgx.AsText(),
        remBlank
    )
    { }

    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
    public Split(String text, IText rgx, bool remBlank = true) : this(
        text.AsText(),
        rgx,
        remBlank)
    { }

    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
    public Split(IText text, String rgx, bool remBlank = true) : this(
        text,
        rgx.AsText(),
        remBlank
    )
    { }
}

public static partial class TextSmarts
{
    public static IEnumerable<string> AsSplit(this IText text, IText rgx, bool remBlank = true) =>
        new Split(text, rgx, remBlank);

    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
    public static IEnumerable<string> AsSplit(this String text, String rgx, bool remBlank = true) =>
        new Split(text, rgx, remBlank);

    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
    public static IEnumerable<string> AsSplit(this String text, IText rgx, bool remBlank = true) =>
        new Split(text, rgx, remBlank);

    /// <summary>
    /// A <see cref="IText"/> which has been splitted at the given string.
    /// </summary>
    /// <param name="text">text to split</param>
    /// <param name="rgx">regex to use for splitting</param>
    /// <param name="remBlank">switch to remove empty or whitspace stirngs from result or not</param>
    public static IEnumerable<string> AsSplit(this IText text, String rgx, bool remBlank = true)  =>
    new Split(text, rgx, remBlank);
}
