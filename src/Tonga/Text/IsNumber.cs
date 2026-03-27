using System;
using System.Globalization;
using Tonga.Fact;

namespace Tonga.Text
{
    /// <summary>
    /// Checks whether a given text is a number
    /// </summary>
    public sealed class IsNumber(TextMorph text, IFormatProvider format) : FactEnvelope(() =>

        double.TryParse(
            text.Str(),
            NumberStyles.Any,
            format,
            out var unused
        )
    )
    {
        /// <summary>
        /// Checks whether the given text is a number
        /// </summary>
        /// <param name="text">the text</param>
        public IsNumber(TextMorph text) : this(
            text, NumberFormatInfo.InvariantInfo
        )
        { }

        /// <summary>
        /// Checks whether the given text is a number
        /// </summary>
        /// <param name="text">the text</param>
        public IsNumber(IText text) : this(
            new TextMorph(text), NumberFormatInfo.InvariantInfo
        )
        { }

        /// <summary>
        /// Checks whether the given text is a number
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="provider">number format provider</param>
        public IsNumber(IText text, IFormatProvider provider) : this(
            new TextMorph(text), provider
        )
        { }
    }
}
