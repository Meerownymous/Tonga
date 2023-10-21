

using System;
using System.Text.RegularExpressions;

namespace Tonga.Text
{
    /// <summary>
    /// Normalized A <see cref="IText"/> (whitespaces replaced with one single space)
    /// </summary>
    public sealed class Normalized : TextEnvelope
    {
        /// <summary>
        /// Normalized A <see cref="IText"/>  (whitespaces replaced with one single space)
        /// </summary>
        /// <param name="text">text to normalize</param>
        public Normalized(String text) : this(AsText._(text))
        { }

        /// <summary>
        /// Normalized A <see cref="IText"/>  (whitespaces replaced with one single space)
        /// </summary>
        /// <param name="text">text to normalize</param>
        public Normalized(IText text) : base(() =>
            Regex.Replace(new Trimmed(text).AsString(), "\\s+", " ")
        )
        { }
    }
}
