using System.Text;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> repeated multiple times.
/// </summary>
public sealed class Repeated(TextMorph text, int count) : TextEnvelope(
    () =>
    {
        StringBuilder output = new StringBuilder();
        for (int cnt = 0; cnt < count; ++cnt)
        {
            output.Append(text.Str());
        }
        return output.ToString();
    }
)
{
    public Repeated(IText txt, int count) : this(new TextMorph(txt), count)
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> repeated multiple times.
    /// </summary>
    public static IText AsRepeated(this IText text, int count) => new Repeated(text, count);

    /// <summary>
    /// A <see cref="IText"/>  repeated multiple times.
    /// </summary>
    public static IText AsRepeated(this string text, int count) => new Repeated(text, count);
}
