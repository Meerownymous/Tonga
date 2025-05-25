

using System;
using System.Collections.Generic;
using Tonga.Enumerable;


namespace Tonga.Scalar
{
    /// <summary>
    /// First element in <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class First<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public First(IEnumerable<T> source, Exception ex) : this(
            _ => true,
            source,
            _ => throw ex
        )
        { }

        public First(IEnumerable<T> source) : this(
            _ => true,
            source,
            new ArgumentException("Cannot get first element - no match.")
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public First(Func<T, bool> condition, IEnumerable<T> source) : this(
            condition,
            source,
            new ArgumentException("Cannot get first element - no match.")
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public First(Func<T, bool> condition, IEnumerable<T> source, Exception ex) : this(
            condition,
            source,
            _ => throw ex)
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public First(IEnumerable<T> source, T fallback) : this(
            enm => true,
            source,
            _ => fallback
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        /// <param name="condition">condition to match in order to find the desired item</param>
        public First(Func<T, bool> condition, IEnumerable<T> source, T fallback) : this(
            condition,
            source,
            _ => fallback
        )
        { }

        /// <summary>
        /// First Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public First(IEnumerable<T> source, IScalar<T> fallback) : this(
            _ => true,
            source,
            _ => fallback.Value()
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fallback">fallback if no match</param>
        public First(IEnumerable<T> src, Func<IEnumerable<T>, T> fallback) : this(item => true, src, fallback)
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        public First(Func<T, bool> condition, IEnumerable<T> src, Func<IEnumerable<T>, T> fallback) : base(
            () =>
            {
                using var filtered = Filtered._(condition, src).GetEnumerator();

                T result;
                if (filtered.MoveNext())
                {
                    result = filtered.Current;
                }
                else
                {
                    result = fallback(src);
                }
                return result;
            })
        { }
    }

    public static class First
    {
        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IScalar<T> _<T>(IEnumerable<T> source, Exception ex)
            => new First<T>(source, ex);

        public static IScalar<T> _<T>(IEnumerable<T> source)
            => new First<T>(source);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IScalar<T> _<T>(Func<T, bool> condition, IEnumerable<T> source)
            => new First<T>(condition, source);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IScalar<T> _<T>(Func<T, bool> condition, IEnumerable<T> source, Exception ex)
            => new First<T>(condition, source, ex);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        public static IScalar<T> _<T>(IEnumerable<T> source, T fallback)
            => new First<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        public static IScalar<T> _<T>(Func<T, bool> condition, IEnumerable<T> source, T fallback)
            => new First<T>(condition, source, fallback);

        /// <summary>
        /// First Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        public static IScalar<T> _<T>(IEnumerable<T> source, IScalar<T> fallback)
            => new First<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        public static IScalar<T> _<T>(IEnumerable<T> src, Func<IEnumerable<T>, T> fallback)
            => new First<T>(src, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        public static IScalar<T> _<T>(Func<T, bool> condition, IEnumerable<T> src, Func<IEnumerable<T>, T> fallback)
            => new First<T>(condition, src, fallback);

    }
}
