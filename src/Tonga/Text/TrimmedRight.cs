

using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// An <see cref="IText"/> without whitespaces / control characters or defined letters or a defined text on the left side.
/// </summary>
public sealed class TrimmedRight : TextEnvelope
{
    /// <summary>
    /// A <see cref="string"/> trimmed (removed whitespaces) on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    public TrimmedRight(string text) : this(text.AsText())
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed (removed whitespaces) on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    public TrimmedRight(IText text) : this(
        text,
        new AsScalar<char[]>(() => ['\b', '\f', '\n', '\r', '\t', '\v', ' '])
    )
    { }

    /// <summary>
    /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public TrimmedRight(string text, char[] trimText) : this(text.AsText(), trimText)
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public TrimmedRight(IText text, char[] trimText) : this(text, trimText.AsScalar())
    { }

    /// <summary>
    /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="trimText">text that trims the text</param>
    public TrimmedRight(IText text, IScalar<char[]> trimText) : base(
        new AsText(() => text.Str().TrimEnd(trimText.Value()))
    )
    { }

    /// <summary>
    /// A <see cref="string"/> from which a <see cref="string"/> is removed on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public TrimmedRight(string text, string removeText) : this(text.AsText(), removeText.AsText())
    { }

    /// <summary>
    /// A <see cref="string"/> from which an <see cref="IText"/> is removed on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public TrimmedRight(string text, IText removeText) : this(text.AsText(), removeText)
    { }

    /// <summary>
    /// An <see cref="IText"/> from which a <see cref="string"/> is removed on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    public TrimmedRight(IText text, string removeText) : this(text, removeText.AsText())
    { }

    /// <summary>
    /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on the right side.
    /// </summary>
    /// <param name="text">text to trim</param>
    /// <param name="removeText">text that is removed from the text</param>
    /// <param name="ignoreCase">Trim by disregarding case.</param>
    public TrimmedRight(IText text, IText removeText, bool ignoreCase = false) : base(
        () =>
        {
            string str = text.Str();
            string remove = removeText.Str();

            if (ignoreCase)
            {
                var remLower = remove.ToLower();
                if (str.ToLower().EndsWith(remLower))
                {
                    int startIndex = str.Length - remove.Length;
                    str = str.Remove(startIndex, remove.Length);
                }
            }
            else
            {
                if (str.EndsWith(remove))
                {
                    int startIndex = str.Length - remove.Length;
                    str = str.Remove(startIndex, remove.Length);
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
        /// A <see cref="string"/> trimmed (removed whitespaces) on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public static IText AsTrimmedRight(this string text) => new TrimmedRight(text);

        /// <summary>
        /// An <see cref="IText"/> trimmed (removed whitespaces) on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public static IText AsTrimmedRight(this IText text) =>
            new TrimmedRight(text);

        /// <summary>
        /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public static IText AsTrimmedRight(this string text, char[] trimText) =>
            new TrimmedRight(text, trimText);

        /// <summary>
        /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public static IText AsTrimmedRight(this IText text, char[] trimText) =>
            new TrimmedRight(text, trimText);

        /// <summary>
        /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public static IText AsTrimmedRight(this IText text, IScalar<char[]> trimText) =>
            new TrimmedRight(text, trimText);

        /// <summary>
        /// A <see cref="string"/> from which a <see cref="string"/> is removed on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public static IText AsTrimmedRight(this string text, string removeText) =>
            new TrimmedRight(text, removeText);

        /// <summary>
        /// A <see cref="string"/> from which an <see cref="IText"/> is removed on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public static IText AsTrimmedRight(this string text, IText removeText) =>
            new TrimmedRight(text, removeText);

        /// <summary>
        /// An <see cref="IText"/> from which a <see cref="string"/> is removed on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public static IText AsTrimmedRight(this IText text, string removeText) =>
            new TrimmedRight(text, removeText);

        /// <summary>
        /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public static IText AsTrimmedRight(this IText text, IText removeText) =>
            new TrimmedRight(text, removeText);

        /// <summary>
        /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on the right side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        /// <param name="ignoreCase">Trim by disregarding case.</param>
        public static IText AsTrimmedRight(this IText text, IText removeText, bool ignoreCase) =>
            new TrimmedRight(text, removeText, ignoreCase);
}
