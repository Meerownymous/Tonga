

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> as lowercase.
/// </summary>
public sealed class Lower(IText text) : TextEnvelope(
    () => text.Str().ToLower()
);

public static partial class TextSmarts
{
    public static IText AsLower(this IText text) => new Lower(text);
}
