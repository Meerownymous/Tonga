

using System;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> whose contents have been replaced by another text.
/// </summary>
public sealed class Replaced(IText text, String find, String replace) : TextEnvelope(
    () => text.Str().Replace(find, replace)
);

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> whose contents have been replaced by another text.
    /// </summary>
    public static IText AsReplaced(this IText text, String find, String replace) =>
        new Replaced(text, find, replace);
}

