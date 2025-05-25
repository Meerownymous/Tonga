

using System;

namespace Tonga.Text;

/// <summary>
/// An <see cref="IText"/> without whitespaces / control characters or defined letters or a defined text on both sides.
/// </summary>
public sealed class Trimmed : TextEnvelope
{
    /// <summary>
    /// A <see cref="string"/> trimmed (removed whitespaces) on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    public Trimmed(string text) : this(text.AsText())
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed (removed whitespaces) on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    public Trimmed(IText text) : this(
        text,
        () => ['\b', '\f', '\n', '\r', '\t', '\v', ' ']
    )
    { }

    /// <summary>
    /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public Trimmed(string text, char[] trimText) : this(text.AsText(), trimText)
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public Trimmed(IText text, char[] trimText) : this(text, () => trimText)
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public Trimmed(IText text, Func<char[]> trimText) : base(
        new AsText(() => text.Str().Trim(trimText()))
    )
    { }

    /// <summary>
    /// A <see cref="string"/> from which a <see cref="string"/> is removed on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public Trimmed(string text, string removeText) : this(text.AsText(), removeText.AsText())
    { }

    /// <summary>
    /// A <see cref="string"/> from which an <see cref="IText"/> is removed on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public Trimmed(string text, IText removeText) : this(text.AsText(), removeText)
    { }

    /// <summary>
    /// An <see cref="IText"/> from which a <see cref="string"/> is removed on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public Trimmed(IText text, string removeText) : this(text, removeText.AsText())
    { }

    /// <summary>
    /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="ignoreCase">Trim by disregarding case.</param>
    /// <param name="removeText">text that is removed from the text</param>
    public Trimmed(IText text, IText removeText, bool ignoreCase = false) : base(
        () =>
        {
            string str = text.Str();
            string remove = removeText.Str();

            if (ignoreCase)
            {
                var lower = str.ToLower();
                var remLower = remove.ToLower();

                while (str.StartsWith(remove) || str.EndsWith(remove))
                {
                    if (lower.StartsWith(remLower))
                    {
                        str = str.Remove(0, remove.Length);
                    }
                    if (str.ToLower().EndsWith(remLower))
                    {
                        int startIndex = str.Length - remove.Length;
                        str = str.Remove(startIndex, remove.Length);
                    }
                }
            }
            else
            {
                while (str.StartsWith(remove) || str.EndsWith(remove))
                {
                    if (str.StartsWith(remove))
                    {
                        str = str.Remove(0, remove.Length);
                    }
                    if (str.EndsWith(remove))
                    {
                        int startIndex = str.Length - remove.Length;
                        str = str.Remove(startIndex, remove.Length);
                    }
                }
            }
            return str;
        }
    ){ }
}

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="string"/> trimmed (removed whitespaces) on both sides.
    /// </summary>
    /// <param name="str">text to trim</param>
    public static IText AsTrimmed(this string str) => new Trimmed(str);

    /// <summary>
    /// An <see cref="IText"/> trimmed (removed whitespaces) on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    public static IText AsTrimmed(this IText text) => new Trimmed(text);

    /// <summary>
    /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on both sides.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public static IText AsTrimmed(this string str, char[] trimText) => new Trimmed(str, trimText);

    /// <summary>
    /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public static IText AsTrimmed(this IText text, char[] trimText) => new Trimmed(text, trimText);

    /// <summary>
    /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public static IText AsTrimmed(this IText text, Func<char[]> trimText) => new Trimmed(text, trimText);

    /// <summary>
    /// A <see cref="string"/> from which a <see cref="string"/> is removed on both sides.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public static IText AsTrimmed(this string str, string removeText) => new Trimmed(str, removeText);

    /// <summary>
    /// A <see cref="string"/> from which an <see cref="IText"/> is removed on both sides.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public static IText AsTrimmed(this string str, IText removeText) => new Trimmed(str, removeText);

    /// <summary>
    /// An <see cref="IText"/> from which a <see cref="string"/> is removed on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public static IText AsTrimmed(this IText text, string removeText) => new Trimmed(text, removeText.AsText());

    /// <summary>
    /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on both sides.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="ignoreCase">Trim by disregarding case.</param>
    /// <param name="removeText">text that is removed from the text</param>
    public static IText AsTrimmed(this IText text, IText removeText, bool ignoreCase = false) =>
        new Trimmed(text, removeText, ignoreCase);
}
