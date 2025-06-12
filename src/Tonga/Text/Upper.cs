namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> as uppercase.
/// </summary>
public sealed class Upper(IText text) : TextEnvelope(() => text.Str().ToUpper());

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> as uppercase.
    /// </summary>
    public static IText AsUpper(this IText text) => new Upper(text);
}
