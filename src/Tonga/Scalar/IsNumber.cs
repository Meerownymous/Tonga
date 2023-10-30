

using System;
using System.Globalization;
using Tonga.Text;

namespace Tonga.Scalar
{
    /// <summary>
    /// Checks whether a given text is a number
    /// </summary>
    public sealed class IsNumber : ScalarEnvelope<bool>
    {
        /// <summary>
        /// Checks whether the given text is a number
        /// </summary>
        /// <param name="text">the text</param>
        public IsNumber(string text) : this(
            new AsText(text),
            NumberFormatInfo.InvariantInfo
        )
        { }

        /// <summary>
        /// Checks whether the given text is a number
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="provider">number format provider</param>
        public IsNumber(string text, IFormatProvider provider) : this(
            new AsText(text),
            provider
        )
        { }

        /// <summary>
        /// Checks whether the given text is a number
        /// </summary>
        /// <param name="text">the text</param>
        public IsNumber(IText text) : this(
            text,
            NumberFormatInfo.InvariantInfo
        )
        { }

        /// <summary>
        /// Checks whether the given text is a number
        /// </summary>
        /// <param name="text">the text</param>
        /// <param name="provider">number format provider</param>
        public IsNumber(IText text, IFormatProvider provider) : base(() =>
                double.TryParse(
                    text.AsString(),
                    NumberStyles.Any,
                    provider,
                    out var unused
                )
            )
        { }
    }
}
