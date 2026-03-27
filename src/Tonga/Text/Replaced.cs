

using System;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> whose contents have been replaced by another text.
/// </summary>
public sealed class Replaced(TextMorph text, TextMorph find, TextMorph replace)
    : TextEnvelope(() => text.Str().Replace(find.Str(), replace.Str())
    )
{
    public Replaced(IText text, IText find, IText replace) : this(new TextMorph(text), new TextMorph(find), new TextMorph(replace))
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> whose contents have been replaced by another text.
    /// </summary>
    public static IText AsReplaced(this IText text, String find, String replace) =>
        new Replaced(new TextMorph(text), find, replace);
}

