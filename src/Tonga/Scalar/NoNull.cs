

using System;
using System.IO;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary>
    /// Scalar that will raise error or return fallback if value is null.
    /// </summary>
    /// <typeparam name="T">type of return value</typeparam>
    public class NoNull<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        public NoNull(T origin) : this(
            origin,
            new IOException("got NULL instead of a valid value"))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="ex">error to raise if null</param>
        public NoNull(T origin, Exception ex) : this(
            AsScalar._(origin),
            new FuncOf<T>(() => throw ex))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public NoNull(T origin, T fallback) : this(
            AsScalar._(origin),
            fallback)
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback value</param>
        public NoNull(IScalar<T> origin, T fallback) : this(
            origin,
            new FuncOf<T>(() => fallback))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback</param>
        public NoNull(IScalar<T> origin, IFunc<T> fallback)
            : base(() =>
            {
                T ret = origin.Value();

                if (ret == null)
                {
                    ret = fallback.Invoke();
                }

                return ret;
            })
        { }
    }

    public static class NoNull
    {
        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        public static IScalar<T> _<T>(T origin)
            => new NoNull<T>(origin);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="ex">error to raise if null</param>
        public static IScalar<T> _<T>(T origin, Exception ex)
            => new NoNull<T>(origin, ex);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public static IScalar<T> _<T>(T origin, T fallback)
            => new NoNull<T>(origin, fallback);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback value</param>
        public static IScalar<T> _<T>(IScalar<T> origin, T fallback)
            => new NoNull<T>(origin, fallback);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback</param>
        public static IScalar<T> _<T>(IScalar<T> origin, IFunc<T> fallback)
            => new NoNull<T>(origin, fallback);
    }
}
