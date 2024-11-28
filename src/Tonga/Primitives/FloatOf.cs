using System;
using System.Globalization;
using Tonga.Scalar;
using Tonga.Text;

#pragma warning disable CS1591

namespace Tonga.Primitives
{
    /// <summary>
    /// A float out of text.
    /// </summary>
    public sealed class FloatOf : ScalarEnvelope<float>
    {
        private readonly AsScalar<float> val;

        /// <summary>
        /// A float out of a <see cref="string"/> using invariant culture.
        /// </summary>
        /// <param name="str">a float as a string</param>
        public FloatOf(String str) : this(new AsText(str))
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using invariant culture.
        /// </summary>
        /// <param name="text">a float as a text</param>
        public FloatOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A float out of a <see cref="string"/>.
        /// </summary>
        /// <param name="str">a float as a string</param>
        /// <param name="culture">culture of the string</param>
        public FloatOf(String str, CultureInfo culture) : this(new AsText(str), culture)
        { }

        /// <summary>
        /// A float out of a <see cref="IText"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="text">a float as a text</param>
        /// <param name="culture">a culture of the string</param>
        public FloatOf(IText text, CultureInfo culture) : this(
            () => float.Parse(text.AsString(), culture.NumberFormat)
        )
        { }

        public FloatOf(Func<float> value) : base(value)
        { }
    }
}
