

using System;
using Tonga.Scalar;

namespace Tonga.Number
{
    /// <summary>
    /// A parsed number
    /// </summary>
    public sealed class LiveNumber : INumber
    {
        private readonly IScalar<INumber> number;

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="text">text to parse</param>
        /// <param name="blockSeparator">separator for blocks, for example 1.000</param>
        /// <param name="decimalSeparator">separator for floating point numbers, for example 16,235 </param>
        public LiveNumber(Func<string> text, string decimalSeparator, string blockSeparator) : this(
            () => new NumberOf(text(), decimalSeparator, blockSeparator)
        )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        public LiveNumber(Func<string> str) : this(
            () => new NumberOf(str())
        )
        { }

        /// <summary>
        /// A <see cref="string"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public LiveNumber(Func<string> str, IScalar<IFormatProvider> provider) : this(
           () => new NumberOf(str(), provider)
        )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="str">The string</param>
        /// <param name="provider">a number format provider</param>
        public LiveNumber(Func<string> str, IFormatProvider provider) : this(
            () => new NumberOf(str(), provider)
        )
        { }

        /// <summary>
        /// A <see cref="int"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="integer">The integer</param>
        public LiveNumber(Func<int> integer) : this(
            () => new NumberOf(integer())
        )
        { }

        /// <summary>
        /// A <see cref="double"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="dbl">The double</param>
        public LiveNumber(Func<double> dbl) : this(
            () => new NumberOf(dbl())
        )
        { }

        /// <summary>
        /// A <see cref="long"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng">The long</param>
        public LiveNumber(Func<long> lng) : this(
            () => new NumberOf(lng())
        )
        { }

        /// <summary>
        /// A <see cref="float"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="flt">The float</param>
        public LiveNumber(Func<float> flt) : this(
            () => new NumberOf(flt())
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> as a <see cref="INumber"/>
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="itg"></param>
        /// <param name="flt"></param>
        /// <param name="dbl"></param>
        public LiveNumber(IScalar<long> lng, IScalar<int> itg, IScalar<float> flt, IScalar<double> dbl) : this(
            () => new NumberOf(lng, itg, flt, dbl)
        )
        { }

        public LiveNumber(Func<INumber> number)
        {
            this.number = AsScalar._(number);
        }

        /// <summary>
        /// Number as double representation
        /// Precision: ±5.0e−324 to ±1.7e308	(15-16 digits)
        /// </summary>
        /// <returns></returns>
        public double AsDouble()
        {
            return this.number.Value().AsDouble();
        }

        /// <summary>
        /// Number as float representation
        /// Precision: 	±1.5e−45 to ±3.4e38	    (7 digits)
        /// </summary>
        /// <returns></returns>
        public float AsFloat()
        {
            return this.number.Value().AsFloat();
        }

        /// <summary>
        /// Number as integer representation
        /// Range -2,147,483,648 to 2,147,483,647	(Signed 32-bit integer)
        /// </summary>
        /// <returns></returns>
        public int AsInt()
        {
            return this.number.Value().AsInt();
        }

        /// <summary>
        /// Number as long representation
        /// Range -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	(Signed 64-bit integer)
        /// </summary>
        /// <returns></returns>
        public long AsLong()
        {
            return this.number.Value().AsLong();
        }
    }
}
