

using System;
using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> of texts joined together.
/// </summary>
public sealed class Joined(IText delimit, Func<IEnumerable<IText>> txts) : TextEnvelope(
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
    public Joined(String delimit, IEnumerable<string> strs) : this(
        delimit.AsText(),
        Mapped._(
            str => str.AsText(),
            strs
        )
    )
    { }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="strs">texts to join</param>
    public Joined(String delimit, params string[] strs) : this(
        delimit.AsText(),
        strs.AsMapped(str => str.AsText())
    )
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(IText delimit, params IText[] txts) : this(delimit, txts.AsEnumerable)
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(String delimit, params IText[] txts) : this(
        delimit.AsText(),
        txts.AsEnumerable
    )
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(String delimit, IEnumerable<IText> txts) : this(
        delimit.AsText(),
        () => txts
    )
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(String delimit, Func<IEnumerable<IText>> txts) : this(
        delimit.AsText(),
        txts
    )
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(IText delimit, IScalar<IEnumerable<IText>> txts) : this(
        delimit,
        txts.Value
    )
    {
    }

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    /// <param name="delimit">delimiter</param>
    /// <param name="txts">texts to join</param>
    public Joined(IText delimit, IEnumerable<IText> txts) : this(
        delimit,
        () => txts
    )
    {
    }
}

public static partial class TextSmarts
{
    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this IEnumerable<string> strs, string delimit) => new(delimit, strs);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this string[] strs, String delimit) => new(delimit, strs);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this IText[] txts, IText delimit) => new(delimit, txts);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this IText[] txts, String delimit) => new(delimit, txts);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this IEnumerable<IText> txts, String delimit) => new(delimit, txts);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this Func<IEnumerable<IText>> txts, String delimit) => new(delimit, txts);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this IScalar<IEnumerable<IText>> txts, IText delimit) => new(delimit, txts);

    /// <summary>
    /// Joins texts together with the delimiter between them.
    /// </summary>
    public static Joined AsJoined(this IEnumerable<IText> txts, IText delimit) => new(delimit, txts);
}
