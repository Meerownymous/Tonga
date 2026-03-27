

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> of texts joined together.
/// </summary>
public sealed class Joined(TextMorph delimit, Func<IEnumerable<TextMorph>> txts) : TextEnvelope(
    () =>
        String.Join(
            delimit.Str(),
            txts().AsMapped(text => text.Str())
        )
)
{
    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="strs">texts to join</param>
    public Joined(IText delimit, IEnumerable<string> strs) : this(
        new TextMorph(delimit),
        () => strs.AsMapped(str => new TextMorph(str))
    )
    { }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="strs">texts to join</param>
    public Joined(TextMorph delimit, IEnumerable<IText> strs) : this(
        delimit,
        () => strs.AsMapped(str => new TextMorph(str))
    )
    { }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="strs">texts to join</param>
    public Joined(TextMorph delimit, IEnumerable<string> strs) : this(
        delimit,
        () => strs.AsMapped(str => new TextMorph(str))
    )
    { }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(TextMorph delimit, params TextMorph[] txts) : this(delimit, txts.AsMapped(t => t.Str()))
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(TextMorph delimit, params IText[] txts) : this(delimit, txts.AsMapped(t => t.Str()))
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(IText delimit, params IText[] txts) : this(delimit, txts.AsEnumerable().AsMapped(t => t.Str()))
    {
    }
}

public static partial class TextSmarts
{
    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static IText AsJoined(this IText[] txts, IText delimit) => new Joined(delimit, txts);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static IText AsJoined(this IEnumerable<string> txts, TextMorph delimit) => new Joined(delimit, txts);
}
