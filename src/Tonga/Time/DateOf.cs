

using System;
using System.Globalization;
using Tonga.Scalar;

namespace Tonga.Time
{
    /// <summary>
    /// A date out of a text
    /// </summary>
    public sealed class DateOf : IScalar<DateTime>
    {
        private readonly IScalar<DateTime> date;

        /// <summary>
        /// A date parsed using a using <see cref="CultureInfo.InvariantCulture"/>
        /// </summary>
        /// <param name="date">the date as text</param>
        public DateOf(string date) : this(date, CultureInfo.InvariantCulture, "yyyy-MM-ddTHH:mm:ss.fffffffZ", "yyyy-MM-ddTHH:mm:ss.fffffffzzz", "ddd, dd MMM yyyy HH:mm:ss Z")
        { }

        /// <summary>
        /// A date parsed using a pattern and a formatprovider
        /// </summary>
        /// <param name="date">the date as text</param>
        /// <param name="patterns"></param>
        public DateOf(string date, params string[] patterns) : this(date, CultureInfo.InvariantCulture, patterns)
        { }

        /// <summary>
        /// A date parsed using a pattern and a formatprovider
        /// </summary>
        /// <param name="date">the date as text</param>
        /// <param name="patterns"></param>
        /// <param name="provider"></param>
        public DateOf(string date, IFormatProvider provider, params string[] patterns) : this(
            new Live<DateTime>(() =>
            {
                return DateTime.ParseExact(date, patterns, provider, DateTimeStyles.AssumeUniversal);
            }))
        { }

        /// <summary>
        /// A parsed <see cref="DateTime"/>
        /// </summary>
        /// <param name="date">the date as text</param>
        public DateOf(IText date) : this(date, CultureInfo.InvariantCulture)
        { }

        /// <summary>
        /// A date parsed using a formatprovider
        /// </summary>
        /// <param name="date">the date as text</param>
        /// <param name="dateFormat">format provider</param>
        public DateOf(IText date, IFormatProvider dateFormat) : this(
            new Live<DateTime>(
                () =>
                    DateTime.Parse(
                        date.AsString(),
                        dateFormat
                    )
                )
            )
        { }

        /// <summary>
        /// A date
        /// </summary>
        /// <param name="date"></param>
        public DateOf(IScalar<DateTime> date)
        {
            this.date = new ScalarOf<DateTime>(date);
        }

        /// <summary>
        /// The parsed Date
        /// </summary>
        /// <returns></returns>
        public DateTime Value()
        {
            return this.date.Value();
        }
    }
}
