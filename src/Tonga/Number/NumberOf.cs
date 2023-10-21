

using System;
using System.Globalization;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Number
{
    /// <summary>
    /// A parsed number
    /// </summary>
    public sealed class NumberOf : NumberEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="blockSeperator">seperator for blocks, for example 1.000</param>
        /// <param name="decimalSeperator">seperator for floating point numbers, for example 16,235 </param>
        public NumberOf(string text, string decimalSeperator, string blockSeperator) : this(
            new AsScalar<long>(() => Convert.ToInt64(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new AsScalar<int>(() => Convert.ToInt32(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new AsScalar<float>(() => (float)Convert.ToDecimal(text, new NumberFormatInfo()
            {
                NumberDecimalSeparator = decimalSeperator,
                NumberGroupSeparator = blockSeperator
            })),
            new AsScalar<double>(() => Convert.ToDouble(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                }))
        )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        public NumberOf(string str) : this(str, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A <see cref="string"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string str, IScalar<IFormatProvider> provider) : this(str, provider.Value())
        { }

        /// <summary>
        /// A <see cref="string"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(string str, IFormatProvider provider) : this(new AsText(str), provider)
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">The text</param>
        public NumberOf(IText text) : this(text, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">The text</param>
        /// <param name="provider">a number format provider</param>
        public NumberOf(IText text, IFormatProvider provider) : this(
            new AsScalar<long>(
                () =>
                {
                    try
                    {
                        return Convert.ToInt64(text.AsString(), provider);
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException(new Formatted("'{0}' is not a number.", text).AsString());
                    }
                }),
            new AsScalar<int>(
                () =>
                {
                    try
                    {
                        return Convert.ToInt32(text.AsString(), provider);
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException(new Formatted("'{0}' is not a number.", text).AsString());
                    }
                }),
            new AsScalar<float>(
                () =>
                {
                    try
                    {
                        return Convert.ToSingle(text.AsString(), provider);
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException(new Formatted("'{0}' is not a number.", text).AsString());
                    }
                }),
            new AsScalar<double>(
                () =>
                {
                    try
                    {
                        return Convert.ToDouble(text.AsString(), provider);
                    }
                    catch (FormatException)
                    {
                        throw new ArgumentException(new Formatted("'{0}' is not a number.", text).AsString());
                    }
                })
            )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="integer">The integer</param>
        public NumberOf(int integer) : this(
            new AsScalar<long>(() => Convert.ToInt64(integer)),
            new AsScalar<int>(integer),
            new AsScalar<float>(() => Convert.ToSingle(integer)),
            new AsScalar<double>(() => Convert.ToDouble(integer))
        )
        { }

        /// <summary>
        /// A <see cref="double"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="dbl">The double</param>
        public NumberOf(double dbl) : this(
            new AsScalar<long>(() => Convert.ToInt64(dbl)),
            new AsScalar<int>(() => Convert.ToInt32(dbl)),
            new AsScalar<float>(() => Convert.ToSingle(dbl)),
            new AsScalar<double>(dbl)
            )
        { }

        /// <summary>
        /// A <see cref="long"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng">The long</param>
        public NumberOf(long lng) : this(
            new AsScalar<long>(() => lng),
            new AsScalar<int>(() => Convert.ToInt32(lng)),
            new AsScalar<float>(() => Convert.ToSingle(lng)),
            new AsScalar<double>(() => Convert.ToDouble(lng))
            )
        { }

        /// <summary>
        /// A <see cref="float"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="flt">The float</param>
        public NumberOf(float flt) : this(
            new AsScalar<long>(() => Convert.ToInt64(flt)),
            new AsScalar<int>(() => Convert.ToInt32(flt)),
            new AsScalar<float>(() => Convert.ToSingle(flt)),
            new AsScalar<double>(() => Convert.ToDouble(flt))
            )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="itg"></param>
        /// <param name="flt"></param>
        /// <param name="dbl"></param>
        public NumberOf(IScalar<long> lng, IScalar<int> itg, IScalar<float> flt, IScalar<double> dbl) : base(dbl, itg, lng, flt)
        {
        }
    }
}
