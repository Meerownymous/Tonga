

using System;
using Tonga.Scalar;

namespace Tonga.Number
{
    /// <summary>
    /// Wraps up Conversions to <see cref="INumber"/>
    /// </summary>
    public abstract class NumberEnvelope : INumber
    {
        private readonly IScalar<double> dbl;
        private readonly IScalar<int> itg;
        private readonly IScalar<long> lng;
        private readonly IScalar<float> flt;

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl">the double</param>
        public NumberEnvelope(double dbl) : this(AsScalar._(dbl))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(int itg) : this(AsScalar._(itg))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(long lng) : this(AsScalar._(lng))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(float flt) : this(AsScalar._(flt))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(IScalar<float> flt) : this(
            AsScalar._(() => Convert.ToDouble(flt.Value())),
            AsScalar._(() => Convert.ToInt32(flt.Value())),
            AsScalar._(() => Convert.ToInt64(flt.Value())),
            flt)
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(IScalar<int> itg) : this(
            AsScalar._(() => Convert.ToDouble(itg.Value())),
            itg,
            AsScalar._(() => Convert.ToInt64(itg.Value())),
            AsScalar._(() => Convert.ToSingle(itg.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(IScalar<long> lng) : this(
            AsScalar._(() => Convert.ToDouble(lng.Value())),
            AsScalar._(() => Convert.ToInt32(lng.Value())),
            lng,
            AsScalar._(() => Convert.ToSingle(lng.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl"></param>
        public NumberEnvelope(IScalar<double> dbl) : this(
            dbl,
            AsScalar._(() => Convert.ToInt32(dbl.Value())),
            AsScalar._(() => Convert.ToInt64(dbl.Value())),
            AsScalar._(() => Convert.ToSingle(dbl.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from the given inputs
        /// </summary>
        /// <param name="dbl"></param>
        /// <param name="itg"></param>
        /// <param name="lng"></param>
        /// <param name="flt"></param>
        public NumberEnvelope(IScalar<double> dbl, IScalar<int> itg, IScalar<long> lng, IScalar<float> flt)
        {
            this.dbl = dbl;
            this.flt = flt;
            this.lng = lng;
            this.itg = itg;
        }

        /// <summary>
        /// Number as double representation
        /// Precision: ±5.0e−324 to ±1.7e308	(15-16 digits)
        /// </summary>
        /// <returns></returns>
        public double AsDouble()
        {
            return dbl.Value();
        }

        /// <summary>
        /// Number as float representation
        /// Precision: 	±1.5e−45 to ±3.4e38	    (7 digits)
        /// </summary>
        /// <returns></returns>
        public float AsFloat()
        {
            return flt.Value();
        }

        /// <summary>
        /// Number as integer representation
        /// Range -2,147,483,648 to 2,147,483,647	(Signed 32-bit integer)
        /// </summary>
        /// <returns></returns>
        public int AsInt()
        {
            return itg.Value();
        }

        /// <summary>
        /// Number as long representation
        /// Range -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	(Signed 64-bit integer)
        /// </summary>
        /// <returns></returns>
        public long AsLong()
        {
            return lng.Value();
        }
    }
}
