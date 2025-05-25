

using System;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> which has been reversed.
/// </summary>
public sealed class Reversed(IText text) : TextEnvelope(() =>
    {
        char[] chararray = text.Str().ToCharArray();
        Array.Reverse(chararray);
        string reverseTxt = "";
        for (int i = 0; i <= chararray.Length - 1; i++)
        {
            reverseTxt += chararray.GetValue(i);
        }

        return reverseTxt;
    }
);

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> which has been reversed.
    /// </summary>
    public static IText AsReversed(this IText text) => new Reversed(text);
}

