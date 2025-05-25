

using System;
using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// Extracted subtext from a <see cref="IText"/>.
/// </summary>
public sealed class SubText(IText text, Func<int> start, Func<int> length) : TextEnvelope(
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
    public SubText(String str, int start) : this(str.AsText(), start)
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="string"/>.
    /// </summary>
    public SubText(String str, int start, int length) : this(
        str.AsText(),
        start,
        length
    )
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public SubText(IText text, int start) : this(
        text,
        () => start,
        () => text.Str().Length - start
    )
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public SubText(IText text, int start, int length) : this(
        text,
        () => start,
        () => length
    )
    { }

    /// <summary>
    /// Extracted subtext from a <see cref="IText"/>.
    /// </summary>
    public SubText(IText text, IScalar<Int32> start, IScalar<Int32> length) : this(
        text,
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
        new SubText(text, start, length);
}
