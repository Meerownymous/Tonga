

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
            new ScalarOf<long>(() => Convert.ToInt64(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new ScalarOf<int>(() => Convert.ToInt32(
                text,
                new NumberFormatInfo()
                {
                    NumberDecimalSeparator = decimalSeperator,
                    NumberGroupSeparator = blockSeperator
                })),
            new ScalarOf<float>(() => (float)Convert.ToDecimal(text, new NumberFormatInfo()
            {
                NumberDecimalSeparator = decimalSeperator,
                NumberGroupSeparator = blockSeperator
            })),
            new ScalarOf<double>(() => Convert.ToDouble(
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
        public NumberOf(string str, IFormatProvider provider) : this(new TextOf(str), provider)
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
            new ScalarOf<long>(
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
            new ScalarOf<int>(
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
            new ScalarOf<float>(
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
            new ScalarOf<double>(
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
            new ScalarOf<long>(() => Convert.ToInt64(integer)),
            new ScalarOf<int>(integer),
            new ScalarOf<float>(() => Convert.ToSingle(integer)),
            new ScalarOf<double>(() => Convert.ToDouble(integer))
        )
        { }

        /// <summary>
        /// A <see cref="double"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="dbl">The double</param>
        public NumberOf(double dbl) : this(
            new ScalarOf<long>(() => Convert.ToInt64(dbl)),
            new ScalarOf<int>(() => Convert.ToInt32(dbl)),
            new ScalarOf<float>(() => Convert.ToSingle(dbl)),
            new ScalarOf<double>(dbl)
            )
        { }

        /// <summary>
        /// A <see cref="long"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng">The long</param>
        public NumberOf(long lng) : this(
            new ScalarOf<long>(() => lng),
            new ScalarOf<int>(() => Convert.ToInt32(lng)),
            new ScalarOf<float>(() => Convert.ToSingle(lng)),
            new ScalarOf<double>(() => Convert.ToDouble(lng))
            )
        { }

        /// <summary>
        /// A <see cref="float"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="flt">The float</param>
        public NumberOf(float flt) : this(
            new ScalarOf<long>(() => Convert.ToInt64(flt)),
            new ScalarOf<int>(() => Convert.ToInt32(flt)),
            new ScalarOf<float>(() => Convert.ToSingle(flt)),
            new ScalarOf<double>(() => Convert.ToDouble(flt))
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
