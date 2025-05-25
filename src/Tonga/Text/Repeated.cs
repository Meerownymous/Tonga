

using System;
using System.Text;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> repeated multiple times.
/// </summary>
public sealed class Repeated(IText text, int count) : TextEnvelope(
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
    /// <summary>
    /// A <see cref="IText"/>  repeated multiple times.
    /// </summary>
    /// <param name="text">text to repeat</param>
    /// <param name="count">how often to repeat</param>
    public Repeated(String text, int count) : this(
        text.AsText(),
        count
    )
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
