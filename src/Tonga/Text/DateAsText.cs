using System;
using System.Globalization;
using Tonga.Scalar;

namespace Tonga.Text;

/// <summary>
/// A date formatted as a text
/// </summary>
public sealed class DateAsText(Func<DateTime> date, Func<string> format, IFormatProvider provider) : IText
{
    private readonly Func<string> formatted =
        () => date().ToString(format(), provider);

    /// <summary>
    /// Current Datetime as ISO
    /// </summary>
    public DateAsText() : this(() => DateTime.Now)
    { }

    /// <summary>
    /// A date formatted as ISO
    /// </summary>
    /// <param name="date"></param>
    public DateAsText(Func<DateTime> date) : this(date, () => "o", CultureInfo.CurrentCulture)
    { }

    /// <summary>
    /// A date formatted as ISO
    /// </summary>
    /// <param name="date"></param>
    public DateAsText(DateTime date) : this(() => date, () => "o", CultureInfo.CurrentCulture)
    { }

    /// <summary>
    /// A date formatted as ISO
    /// </summary>
    /// <param name="date"></param>
    public DateAsText(IScalar<DateTime> date) : this(date.Value, () => "o", CultureInfo.CurrentCulture)
    { }

    /// <summary>
    /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
    /// </summary>
    /// <param name="date">a date</param>
    /// <param name="format">a format pattern</param>
    public DateAsText(DateTime date, string format) : this(
        () => date,
        () => format,
        CultureInfo.CurrentCulture
    )
    { }

    /// <summary>
    /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
    /// </summary>
    /// <param name="date">a date</param>
    /// <param name="format">a format pattern</param>
    public DateAsText(IScalar<DateTime> date, string format) : this(
        date.Value,
        () => format,
        CultureInfo.CurrentCulture
    )
    { }

    /// <summary>
    /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
    /// </summary>
    /// <param name="date">a date</param>
    /// <param name="format">a format pattern</param>
    public DateAsText(DateTime date, IText format) : this(
        () => date,
        format.Str,
        CultureInfo.CurrentCulture
    )
    { }

    /// <summary>
    /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
    /// </summary>
    /// <param name="date">a date</param>
    /// <param name="format">a format pattern</param>
    public DateAsText(IScalar<DateTime> date, IText format) : this(date.Value, format.Str, CultureInfo.CurrentCulture)
    { }

    /// <summary>
    /// The formatted <see cref="DateTime"/>
    /// </summary>
    /// <returns></returns>
    public string Str() => formatted();
}

public static partial class TextSmarts
{
    public static IText AsText(DateTime date) => new DateAsText(date);
}
