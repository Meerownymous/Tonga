using System.IO;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> as uppercase.
/// </summary>
public sealed class Upper(TextMorph text) : TextEnvelope(() => text.Str().ToUpper())
{
    public Upper(IText text) : this(new TextMorph(text))
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> as uppercase.
    /// </summary>
    public static TextMorph AsUpper(this TextEnvelope text) => new Upper(text);

    /// <summary>
    /// A <see cref="IText"/> as uppercase.
    /// </summary>
    public static TextMorph AsUpper(this string text) => new Upper(text);
}
