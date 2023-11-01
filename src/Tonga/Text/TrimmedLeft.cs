

using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// An <see cref="IText"/> without whitespaces / control characters or defined letters or a defined text on the left side.
    /// </summary>
    public sealed class TrimmedLeft : TextEnvelope
    {
        /// <summary>
        /// A <see cref="string"/> trimmed (removed whitespaces) on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedLeft(string text) : this(AsText._(text))
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed (removed whitespaces) on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        public TrimmedLeft(IText text) : this(
            text,
            AsScalar._(() => new char[] { '\b', '\f', '\n', '\r', '\t', '\v', ' ' })
        )
        { }

        /// <summary>
        /// A <see cref="string"/> trimmed with a <see cref="char"/>[] on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedLeft(string text, char[] trimText) : this(AsText._(text), trimText)
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed with a <see cref="char"/>[] on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedLeft(IText text, char[] trimText) : this(text, AsScalar._(trimText))
        { }

        /// <summary>
        /// An <see cref="IText"/> trimmed with an IScalar&lt;<see cref="char"/>[]&gt; on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="trimText">text that trims the text</param>
        public TrimmedLeft(IText text, IScalar<char[]> trimText) : base(
            AsText._(() => text.AsString().TrimStart(trimText.Value()))
        )
        { }

        /// <summary>
        /// A <see cref="string"/> from which a <see cref="string"/> is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedLeft(string text, string removeText) : this(AsText._(text), AsText._(removeText))
        { }

        /// <summary>
        /// A <see cref="string"/> from which an <see cref="IText"/> is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedLeft(string text, IText removeText) : this(AsText._(text), removeText)
        { }

        /// <summary>
        /// An <see cref="IText"/> from which a <see cref="string"/> is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedLeft(IText text, string removeText) : this(text, AsText._(removeText))
        { }

        /// <summary>
        /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        public TrimmedLeft(IText text, IText removeText) : this(
            text,
            removeText,
            false
        )
        { }

        /// <summary>
        /// An <see cref="IText"/> from which an IScalar&lt;<see cref="IText"/>&gt; is removed on the left side.
        /// </summary>
        /// <param name="text">text to trim</param>
        /// <param name="removeText">text that is removed from the text</param>
        /// <param name="ignoreCase">Trim by disregarding case.</param>
        public TrimmedLeft(IText text, IText removeText, bool ignoreCase) : base(
            AsText._(() =>
            {
                text = new AsSticky(text);
                removeText = new AsSticky(removeText);

                string str = text.AsString();
                string remove = removeText.AsString();

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
            })
        )
        { }
    }
}
