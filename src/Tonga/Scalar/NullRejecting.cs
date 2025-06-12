using System;
using System.IO;

namespace Tonga.Scalar
{
    /// <summary>
    /// Scalar that will raise error or return fallback if value is null.
    /// </summary>
    /// <typeparam name="T">type of return value</typeparam>
    public class NullRejecting<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        public NullRejecting(T origin) : this(
            origin,
            new IOException("got NULL instead of a valid value"))
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="ex">error to raise if null</param>
        public NullRejecting(T origin, Exception ex) : this(
            origin.AsScalar(),
            () => throw ex
        )
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public NullRejecting(T origin, T fallback) : this(
            origin.AsScalar(),
            fallback
        )
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback value</param>
        public NullRejecting(IScalar<T> origin, T fallback) : this(
            origin,
            () => fallback
        )
        { }

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback</param>
        public NullRejecting(IScalar<T> origin, Func<T> fallback) : base(
            () => origin.Value() ?? fallback.Invoke()
        )
        { }
    }

    public static partial class ScalarSmarts
    {
        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        public static IScalar<T> AsNullRejecting<T>(this T origin)
            => new NullRejecting<T>(origin);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="ex">error to raise if null</param>
        public static IScalar<T> AsNullRejecting<T>(this T origin, Exception ex)
            => new NullRejecting<T>(origin, ex);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original</param>
        /// <param name="fallback">the fallback value</param>
        public static IScalar<T> AsNullRejecting<T>(this T origin, T fallback)
            => new NullRejecting<T>(origin, fallback);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback value</param>
        public static IScalar<T> AsNullRejecting<T>(this IScalar<T> origin, T fallback)
            => new NullRejecting<T>(origin, fallback);

        /// <summary>
        /// A scalar with a fallback if value is null.
        /// </summary>
        /// <param name="origin">the original scalar</param>
        /// <param name="fallback">the fallback</param>
        public static IScalar<T> AsNullRejecting<T>(this IScalar<T> origin, Func<T> fallback)
            => new NullRejecting<T>(origin, fallback);
    }
}
