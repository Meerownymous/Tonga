

using System;
using System.Globalization;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Time
{
    /// <summary>
    /// A date formatted as a text
    /// </summary>
    public sealed class DateAsText : IText
    {
        private readonly Func<string> formatted;

        /// <summary>
        /// Current Datetime as ISO
        /// </summary>
        public DateAsText() : this(AsScalar._(() => DateTime.Now))
        { }

        /// <summary>
        /// A date formatted as ISO
        /// </summary>
        /// <param name="date"></param>
        public DateAsText(DateTime date) : this(AsScalar._(date), "o")
        { }

        /// <summary>
        /// A date formatted as ISO
        /// </summary>
        /// <param name="date"></param>
        public DateAsText(IScalar<DateTime> date) : this(date, "o")
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(DateTime date, string format) : this(AsScalar._(date), new AsText(format), CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(IScalar<DateTime> date, string format) : this(date,
            AsText._(format),
            CultureInfo.CurrentCulture
        )
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(DateTime date, IText format) : this(
            AsScalar._(date),
            format,
            CultureInfo.CurrentCulture
        )
        { }

        /// <summary>
        /// A date formatted by using a format-string and <see cref="CultureInfo.CurrentCulture"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        public DateAsText(IScalar<DateTime> date, IText format) : this(date, format, CultureInfo.CurrentCulture)
        { }

        /// <summary>
        /// A date formatted as <see cref="IText"/> by using a <see cref="IFormatProvider"/>
        /// </summary>
        /// <param name="date">a date</param>
        /// <param name="format">a format pattern</param>
        /// <param name="provider">a format provider</param>
        public DateAsText(IScalar<DateTime> date, IText format, IFormatProvider provider)
        {
            this.formatted = () => date.Value().ToString(format.AsString(), provider);
        }

        /// <summary>
        /// The formatted <see cref="DateTime"/>
        /// </summary>
        /// <returns></returns>
        public string AsString()
        {
            return formatted();
        }
    }
}
