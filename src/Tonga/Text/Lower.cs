

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> as lowercase.
/// </summary>
public sealed class Lower(TextMorph text) : TextEnvelope(() => text.Str().ToLower()
)
{
    public Lower(IText text) : this(new TextMorph(text))
    { }
}

public static partial class TextSmarts
{
    public static IText AsLower(this IText text) => new Lower(text);
}
