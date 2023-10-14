

using System;
using System.Globalization;
using Tonga.Scalar;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="int"/> of a text.
    /// </summary>
    public sealed class IntOf : IScalar<Int32>
    {
        private readonly ScalarOf<int> val;

        /// <summary>
        /// A int out of a <see cref="string"/> using invariant culture.
        /// </summary>
        /// <param name="str">a int as a string</param>
        public IntOf(String str) : this(new TextOf(str))
        { }

        /// <summary>
        /// A int out of a <see cref="IText"/> using invariant culture.
        /// </summary>
        /// <param name="text">a int as a text</param>
        public IntOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A int out of a <see cref="string"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="str">a int as a string</param>
        /// <param name="culture">culture of the string</param>
        public IntOf(String str, CultureInfo culture) : this(new TextOf(str), culture)
        { }

        /// <summary>
        /// A int out of a <see cref="IText"/> using the given <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="text">a int as a string</param>
        /// <param name="culture">culture of the string</param>
        public IntOf(IText text, CultureInfo culture) : this(new ScalarOf<int>(() => Convert.ToInt32(text.AsString(), culture.NumberFormat)))
        { }

        /// <summary>
        /// A int out of a scalar.
        /// </summary>
        /// <param name="value">the scalar returning the float</param>
        private IntOf(IScalar<int> value)
        {
            val = new ScalarOf<int>(value);
        }

        /// <summary>
        /// Get the int.
        /// </summary>
        /// <returns>the int</returns>
        public Int32 Value()
        {
            return val.Value();
        }
    }
}
