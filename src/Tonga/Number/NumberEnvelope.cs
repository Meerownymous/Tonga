

using System;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Number
{
    /// <summary>
    /// Wraps up Conversions to <see cref="INumber"/>
    /// </summary>
    public abstract class NumberEnvelope(
        IScalar<double> dbl, IScalar<int> itg, IScalar<long> lng, IScalar<float> flt) : INumber
    {
        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl">the double</param>
        public NumberEnvelope(double dbl) : this(dbl.AsScalar())
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(int itg) : this(itg.AsScalar())
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(long lng) : this(lng.AsScalar())
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(float flt) : this(flt.AsScalar())
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="float"/>
        /// </summary>
        /// <param name="flt">the float</param>
        public NumberEnvelope(IScalar<float> flt) : this(
            new AsScalar<double>(() => Convert.ToDouble(flt.Value())),
            new AsScalar<Int32>(() => Convert.ToInt32(flt.Value())),
            new AsScalar<Int64>(() => Convert.ToInt64(flt.Value())),
            flt
        )
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="int"/>
        /// </summary>
        /// <param name="itg">the int</param>
        public NumberEnvelope(IScalar<int> itg) : this(
            new AsScalar<double>(() => Convert.ToDouble(itg.Value())),
            itg,
            new AsScalar<Int64>(() => Convert.ToInt64(itg.Value())),
            new AsScalar<float>(() => Convert.ToSingle(itg.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="long"/>
        /// </summary>
        /// <param name="lng">the long</param>
        public NumberEnvelope(IScalar<long> lng) : this(
            new AsScalar<double>(() => Convert.ToDouble(lng.Value())),
            new AsScalar<Int32>(() => Convert.ToInt32(lng.Value())),
            lng,
            new AsScalar<float>(() => Convert.ToSingle(lng.Value()))
        )
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl"></param>
        public NumberEnvelope(IScalar<double> dbl) : this(
            dbl,
            new AsScalar<Int32>(() => Convert.ToInt32(dbl.Value())),
            new AsScalar<Int64>(() => Convert.ToInt64(dbl.Value())),
            new AsScalar<float>(() => Convert.ToSingle(dbl.Value())))
        { }

        /// <summary>
        /// A <see cref="INumber"/> from a <see cref="double"/>
        /// </summary>
        /// <param name="dbl"></param>
        public NumberEnvelope(
            Func<double> dbl,
            Func<Int32> itg,
            Func<Int64> lng,
            Func<float> flt
        ) : this(
            new AsScalar<double>(dbl),
            new AsScalar<Int32>(itg),
            new AsScalar<Int64>(lng),
            new AsScalar<float>(flt)
        )
        { }

        /// <summary>
        /// Number as double representation
        /// Precision: ±5.0e−324 to ±1.7e308	(15-16 digits)
        /// </summary>
        /// <returns></returns>
        public double ToDouble() => dbl.Value();

        /// <summary>
        /// Number as float representation
        /// Precision: 	±1.5e−45 to ±3.4e38	    (7 digits)
        /// </summary>
        /// <returns></returns>
        public float ToFloat() => flt.Value();

        /// <summary>
        /// Number as integer representation
        /// Range -2,147,483,648 to 2,147,483,647	(Signed 32-bit integer)
        /// </summary>
        /// <returns></returns>
        public int ToInt() => itg.Value();

        /// <summary>
        /// Number as long representation
        /// Range -9,223,372,036,854,775,808 to 9,223,372,036,854,775,807	(Signed 64-bit integer)
        /// </summary>
        /// <returns></returns>
        public long ToLong() => lng.Value();
    }
}
