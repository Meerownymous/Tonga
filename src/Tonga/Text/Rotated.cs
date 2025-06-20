

using System.Text;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> whose characters have been rotated.
/// </summary>
public sealed class Rotated(IText text, int shift) : TextEnvelope(() =>
    {
        var str = text.Str();
        int length = str.Length;
        if (length != 0 && shift != 0 && shift % length != 0)
        {
            var builder = new StringBuilder(length);
            int offset = -(shift % length);
            if (offset < 0)
            {
                offset = str.Length + offset;
            }

            str = builder.Append(
                str.Substring(offset)
            ).Append(
                str.Substring(0, offset)
            ).ToString();
        }

        return str;
    });

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> whose characters have been rotated.
    /// </summary>
    public static IText AsRotated(this IText text, int shift) => new Rotated(text, shift);
}
