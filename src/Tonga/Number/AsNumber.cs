

using System;
using System.Globalization;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Number;

/// <summary>
/// A parsed number
/// </summary>
public sealed class AsNumber : NumberEnvelope
{
    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="text">text to parse</param>
    /// <param name="blockSeperator">seperator for blocks, for example 1.000</param>
    /// <param name="decimalSeperator">seperator for floating point numbers, for example 16,235 </param>
    public AsNumber(string text, string decimalSeperator, string blockSeperator) : this(
        () => Convert.ToInt64(
            text,
            new NumberFormatInfo()
            {
                NumberDecimalSeparator = decimalSeperator,
                NumberGroupSeparator = blockSeperator
            }
        ),
        () => Convert.ToInt32(
            text,
            new NumberFormatInfo()
            {
                NumberDecimalSeparator = decimalSeperator,
                NumberGroupSeparator = blockSeperator
            }),
        () => (float)Convert.ToDecimal(text, new NumberFormatInfo()
        {
            NumberDecimalSeparator = decimalSeperator,
            NumberGroupSeparator = blockSeperator
        }),
        () => Convert.ToDouble(
            text,
            new NumberFormatInfo
            {
                NumberDecimalSeparator = decimalSeperator,
                NumberGroupSeparator = blockSeperator
            })
    )
    { }

    /// <summary>
    /// A <see cref="int"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="str">The string</param>
    public AsNumber(string str) : this(str, CultureInfo.InvariantCulture)
    { }

    /// <summary>
    /// A <see cref="string"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="str">The string</param>
    /// <param name="provider">a number format provider</param>
    public AsNumber(string str, IScalar<IFormatProvider> provider) : this(str, provider.Value())
    { }

    /// <summary>
    /// A <see cref="string"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="str">The string</param>
    /// <param name="provider">a number format provider</param>
    public AsNumber(string str, IFormatProvider provider) : this(new AsText(str), provider)
    { }

    /// <summary>
    /// A <see cref="int"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="text">The text</param>
    public AsNumber(IText text) : this(text, CultureInfo.InvariantCulture)
    { }

    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="text">The text</param>
    /// <param name="provider">a number format provider</param>
    public AsNumber(IText text, IFormatProvider provider) : this(
            () =>
            {
                try
                {
                    return Convert.ToInt64(text.Str(), provider);
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", text).Str());
                }
            },
            () =>
            {
                try
                {
                    return Convert.ToInt32(text.Str(), provider);
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", text).Str());
                }
            },
            () =>
            {
                try
                {
                    return Convert.ToSingle(text.Str(), provider);
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", text).Str());
                }
            },
            () =>
            {
                try
                {
                    return Convert.ToDouble(text.Str(), provider);
                }
                catch (FormatException)
                {
                    throw new ArgumentException(new Formatted("'{0}' is not a number.", text).Str());
                }
            }
        )
    { }

    /// <summary>
    /// A <see cref="int"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="integer">The integer</param>
    public AsNumber(int integer) : this(
        () => Convert.ToInt64(integer),
        () => integer,
        () => Convert.ToSingle(integer),
        () => Convert.ToDouble(integer)
    )
    { }

    /// <summary>
    /// A <see cref="double"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="dbl">The double</param>
    public AsNumber(double dbl) : this(
        () => Convert.ToInt64(dbl),
        () => Convert.ToInt32(dbl),
        () => Convert.ToSingle(dbl),
        () => dbl
    )
    { }

    /// <summary>
    /// A <see cref="long"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="lng">The long</param>
    public AsNumber(long lng) : this(
        () => lng,
        () => Convert.ToInt32(lng),
        () => Convert.ToSingle(lng),
        () => Convert.ToDouble(lng)
    )
    { }

    /// <summary>
    /// A <see cref="float"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="flt">The float</param>
    public AsNumber(float flt) : this(
        () => Convert.ToInt64(flt),
        () => Convert.ToInt32(flt),
        () => Convert.ToSingle(flt),
        () => Convert.ToDouble(flt)
    )
    { }

    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="lng"></param>
    /// <param name="itg"></param>
    /// <param name="flt"></param>
    /// <param name="dbl"></param>
    public AsNumber(IScalar<long> lng, IScalar<int> itg, IScalar<float> flt, IScalar<double> dbl) : base(
        dbl.Value,
        itg.Value,
        lng.Value,
        flt.Value
    )
    { }

    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="lng"></param>
    /// <param name="itg"></param>
    /// <param name="flt"></param>
    /// <param name="dbl"></param>
    public AsNumber(Func<long> lng, Func<int> itg, Func<float> flt, Func<double> dbl) : base(dbl, itg, lng, flt)
    { }
}

public static partial class NumberSmarts
{
    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="text">text to parse</param>
    /// <param name="blockSeperator">seperator for blocks, for example 1.000</param>
    /// <param name="decimalSeperator">seperator for floating point numbers, for example 16,235 </param>
    public static INumber AsNumber(this string text, string decimalSeperator, string blockSeperator) =>
        new AsNumber(text, decimalSeperator, blockSeperator);

    /// <summary>
    /// A <see cref="int"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="str">The string</param>
    public static INumber AsNumber(this string str) => new AsNumber(str);

    /// <summary>
    /// A <see cref="string"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="str">The string</param>
    /// <param name="provider">a number format provider</param>
    public static INumber AsNumber(this string str, IScalar<IFormatProvider> provider) =>
        new AsNumber(str, provider);

    /// <summary>
    /// A <see cref="string"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="str">The string</param>
    /// <param name="provider">a number format provider</param>
    public static INumber AsNumber(this string str, IFormatProvider provider) =>
        new AsNumber(str, provider);

    /// <summary>
    /// A <see cref="int"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="text">The text</param>
    public static INumber AsNumber(this IText text) => new AsNumber(text);

    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="text">The text</param>
    /// <param name="provider">a number format provider</param>
    public static INumber AsNumber(this IText text, IFormatProvider provider) =>
        new AsNumber(text, provider);

    /// <summary>
    /// A <see cref="int"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="integer">The integer</param>
    public static INumber AsNumber(this int integer) => new AsNumber(integer);

    /// <summary>
    /// A <see cref="double"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="dbl">The double</param>
    public static INumber AsNumber(this double dbl) => new AsNumber(dbl);

    /// <summary>
    /// A <see cref="long"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="lng">The long</param>
    public static INumber AsNumber(long lng) => new AsNumber(lng);

    /// <summary>
    /// A <see cref="float"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="flt">The float</param>
    public static INumber AsNumber(float flt) => new AsNumber(flt);

    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="lng"></param>
    /// <param name="itg"></param>
    /// <param name="flt"></param>
    /// <param name="dbl"></param>
    public static INumber AsNumber(
        IScalar<long> lng,
        IScalar<int> itg,
        IScalar<float> flt,
        IScalar<double> dbl
    ) => new AsNumber(lng, itg, flt, dbl);

    /// <summary>
    /// A <see cref="IText"/> as a <see cref="INumber"/>
    /// </summary>
    /// <param name="lng"></param>
    /// <param name="itg"></param>
    /// <param name="flt"></param>
    /// <param name="dbl"></param>
    public static INumber AsNumber(
        Func<long> lng,
        Func<int> itg,
        Func<float> flt,
        Func<double> dbl
    ) => new AsNumber(lng, itg, flt, dbl);
}
