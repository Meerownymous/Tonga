

using System;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Number
{
    /// <summary>
    /// Wraps up Conversions to <see cref="INumber"/>
    /// </summary>
    public abstract class NumberEnvelope(
        Func<double> dbl, Func<int> itg, Func<long> lng, Func<float> flt) : INumber
    {
        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl">the double</param>
        public NumberEnvelope(double dbl) : this(() => dbl)
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(int itg) : this(() => itg)
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(long lng) : this(() => lng)
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(float flt) : this(() => flt)
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(Func<float> flt) : this(
            () => Convert.ToDouble(flt()),
            () => Convert.ToInt32(flt()),
            () => Convert.ToInt64(flt()),
            flt
        )
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(Func<int> itg) : this(
            () => Convert.ToDouble(itg()),
            itg,
            () => Convert.ToInt64(itg()),
            () => Convert.ToSingle(itg())
        )
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(Func<long> lng) : this(
            () => Convert.ToDouble(lng()),
            () => Convert.ToInt32(lng()),
            lng,
            () => Convert.ToSingle(lng())
        )
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl"></param>
        public NumberEnvelope(Func<double> dbl) : this(
            dbl,
            () => Convert.ToInt32(dbl()),
            () => Convert.ToInt64(dbl()),
            () => Convert.ToSingle(dbl())
        )
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        public NumberEnvelope(
            IScalar<double> dbl,
            IScalar<Int32> itg,
            IScalar<Int64> lng,
            IScalar<float> flt
        ) : this(
            dbl.Value,
            itg.Value,
            lng.Value,
            flt.Value

        )
        { }

        /// <summary>
        /// Number as double representation
        /// Precision: ±5.0e−324 to ±1.7e308	(15-16 digits)
        /// </summary>
        /// <returns></returns>
        public double Double() => dbl();

        /// <summary>
        /// Number as float representation
        /// Precision: 	±1.5e−45 to ±3.4e38	    (7 digits)
        /// </summary>
        /// <returns></returns>
        public float Float() => flt();

        /// <summary>
        /// Number as integer representation
        /// Range -2,147,483,648 to 2,147,483,647	(Signed 32-bit integer)
        /// </summary>
        /// <returns></returns>
        public int Int() => itg();

        /// <summary>
        /// Number as long representation
        /// Range -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	(Signed 64-bit integer)
        /// </summary>
        /// <returns></returns>
        public long Long() => lng();
    }
}
