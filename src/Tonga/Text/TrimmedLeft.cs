

using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// An <see cref="IText"/> without whitespaces / control characters or defined letters or a defined text on the left side.
/// </summary>
public sealed class TrimmedLeft : TextEnvelope
{
    /// <summary>
    /// A <see cref="string"/> trimmed (removed whitespaces) on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    public TrimmedLeft(string str) : this(str.AsText())
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed (removed whitespaces) on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    public TrimmedLeft(IText text) : this(
        text,
        new AsScalar<char[]>(() => ['\b', '\f', '\n', '\r', '\t', '\v', ' '])
    )
    { }

    /// <summary>
    /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public TrimmedLeft(string str, char[] trimText) : this(str.AsText(), trimText)
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public TrimmedLeft(IText text, char[] trimText) : this(text, trimText.AsScalar())
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public TrimmedLeft(IText text, IScalar<char[]> trimText) : base(
        new AsText(() => text.Str().TrimStart(trimText.Value()))
    )
    { }

    /// <summary>
    /// A <see cref="string"/> from which a <see cref="string"/> is removed on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="stringToRemove">text that is removed from the text</param>
    public TrimmedLeft(string str, string stringToRemove) : this(str.AsText(), stringToRemove.AsText())
    { }

    /// <summary>
    /// A <see cref="string"/> from which an <see cref="IText"/> is removed on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="textToRemove">text that is removed from the text</param>
    public TrimmedLeft(string str, IText textToRemove) : this(str.AsText(), textToRemove)
    { }

    /// <summary>
    /// An <see cref="IText"/> from which a <see cref="string"/> is removed on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="stringToRemove">text that is removed from the text</param>
    public TrimmedLeft(IText text, string stringToRemove) : this(text, stringToRemove.AsText())
    { }

    /// <summary>
    /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="textToRemove">text that is removed from the text</param>
    /// <param name="ignoreCase">Trim by disregarding case.</param>
    public TrimmedLeft(IText text, IText textToRemove, bool ignoreCase = false) : base(
        () =>
        {
            string str = text.Str();
            string remove = textToRemove.Str();

            if (ignoreCase)
            {
                var lower = str.ToLower();
                var remLower = remove.ToLower();
                if (lower.StartsWith(remLower))
                {
                    str = str.Remove(0, remove.Length);
                }
            }
            else
            {
                if (str.StartsWith(remove))
                {
                    str = str.Remove(0, remove.Length);
                }
            }
            return str;
        }
    )
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="string"/> trimmed (removed whitespaces) on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    public static IText AsTrimmedLeft(this string str) =>
        new TrimmedLeft(str);

    /// <summary>
    /// An <see cref="IText"/> trimmed (removed whitespaces) on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    public static IText AsTrimmedLeft(this IText text) =>
        new TrimmedLeft(text);

    /// <summary>
    /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public static IText AsTrimmedLeft(this string str, char[] trimText) =>
        new TrimmedLeft(str, trimText);

    /// <summary>
    /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public static IText AsTrimmedLeft(this IText text, char[] trimText) =>
        new TrimmedLeft(text, trimText);

    /// <summary>
    /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public static IText AsTrimmedLeft(this IText text, IScalar<char[]> trimText) =>
        new TrimmedLeft(text, trimText);

    /// <summary>
    /// A <see cref="string"/> from which a <see cref="string"/> is removed on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="stringToRemove">text that is removed from the text</param>
    public static IText AsTrimmedLeft(this string str, string stringToRemove) =>
        new TrimmedLeft(str, stringToRemove);

    /// <summary>
    /// A <see cref="string"/> from which an <see cref="IText"/> is removed on the left side.
    /// </summary>
    /// <param name="str">text to trim</param>
    /// <param name="textToRemove">text that is removed from the text</param>
    public static IText AsTrimmedLeft(this string str, IText textToRemove) =>
        new TrimmedLeft(str, textToRemove);

    /// <summary>
    /// An <see cref="IText"/> from which a <see cref="string"/> is removed on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="stringToRemove">text that is removed from the text</param>
    public static IText AsTrimmedLeft(this IText text, string stringToRemove) =>
        new TrimmedLeft(text, stringToRemove);

    /// <summary>
    /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on the left side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="textToRemove">text that is removed from the text</param>
    /// <param name="ignoreCase">Trim by disregarding case.</param>
    public static IText AsTrimmedLeft(this IText text, IText textToRemove, bool ignoreCase = false) =>
        new TrimmedLeft(text, textToRemove, ignoreCase);
}
