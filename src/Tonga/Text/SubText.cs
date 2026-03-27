

using System;
using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// Extracted subtext from a <see cref="IText"/>.
/// </summary>
public sealed class SubText(TextMorph text, Func<int> start, Func<int> length) : TextEnvelope(
    () =>
        text.Str()
            .Substring(
                start(),
                length()
            )
)
{
    /// <summary>
    /// Extracted subtext from a <see cref="string"/>.
    /// </summary>
    public SubText(String str, int start) : this(new TextMorph(str), start)
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="string"/>.
    /// </summary>
    public SubText(String str, int start, int length) : this(
        new TextMorph(str),
        start,
        length
    )
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public SubText(IText text, int start) : this(
        new TextMorph(text),
        () => start,
        () => text.Str().Length - start
    )
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public SubText(IText text, int start, int length) : this(
        new TextMorph(text),
        () => start,
        () => length
    )
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public SubText(IText text, IScalar<Int32> start, IScalar<Int32> length) : this(
        new TextMorph(text),
        start.Value,
        length.Value
    )
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// Extracted subtext from a <see cref="string"/>.
    /// </summary>
    public static IText AsSubText(this String str, int start) =>
        new SubText(str, start);

    /// <summary>
    /// Extracted subtext from a <see cref="string"/>.
    /// </summary>
    public static IText AsSubText(this String str, int start, int length) =>
        new SubText(str, start, length);

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public static IText AsSubText(this IText text, int start) =>
        new SubText(text, start);

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public static IText AsSubText(this IText text, int start, int length) =>
        new SubText(text, start, length);

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public static IText AsSubText(this IText text, Func<Int32> start, Func<Int32> length) =>
        new SubText(new TextMorph(text), start, length);
}
