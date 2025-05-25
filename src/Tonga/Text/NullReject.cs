

using System.IO;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> that can't accept null.
/// </summary>
public sealed class NullReject(IText text) : TextEnvelope(
    () =>
    {
        if (text == null) throw new IOException("invalid text (null)");
        return text.Str();
    }
);

public static partial class TextSmarts
{
    public static IText AsNullReject(this IText text) => new NullReject(text);
}
