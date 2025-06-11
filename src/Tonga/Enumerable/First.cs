using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// First element in <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class First<T>(
        Func<T, bool> condition,
        IEnumerable<T> src,
        Func<Exception, IEnumerable<T>, T> fallback
    ) :
        ScalarEnvelope<T>(
        () =>
        {
            T result;
            try
            {
                using var filtered = src.AsFiltered(condition).GetEnumerator();
                result =
                    filtered.MoveNext()
                        ? filtered.Current
                        : fallback(new ArgumentException("Source is empty"), src);
            }
            catch (Exception e)
            {
                result = fallback(e, src);
            }
            return result;
        })
    {
        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public First(IEnumerable<T> source, Exception ex) : this(
            _ => true,
            source,
            (_,_) => throw ex
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
            (_,_) => throw ex
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public First(IEnumerable<T> source, T fallback) : this(
            _ => true,
            source,
            (_,_) => fallback
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
            (_,_) => fallback
        )
        { }

        /// <summary>
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public First(IEnumerable<T> source, IScalar<T> fallback) : this(
            _ => true,
            source,
            (_,_) => fallback.Value()
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fallback">fallback if no match</param>
        public First(IEnumerable<T> src, Func<IEnumerable<T>, T> fallback) : this(
            _ => true, src, (_, e) => fallback(e)
        )
        { }

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        /// <param name="src">source enumerable</param>
        /// <param name="fallback">fallback if no match</param>
        public First(IEnumerable<T> src, Func<Exception, IEnumerable<T>, T> fallback) : this(
            _ => true, src, fallback
        )
        { }
    }

    public static partial class EnumerableSmarts
    {
        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> source, Exception ex)
            => new First<T>(source, ex);

        public static IScalar<T> First<T>(this IEnumerable<T> source)
            => new First<T>(source);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> source, Func<T, bool> condition)
            => new First<T>(condition, source);

        /// <summary>
        /// Element from position in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> source, Func<T, bool> condition, Exception ex)
            => new First<T>(condition, source, ex);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> source, T fallback)
            => new First<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> source, Func<T, bool> condition, T fallback)
            => new First<T>(condition, source, fallback);

        /// <summary>
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> source, IScalar<T> fallback)
            => new First<T>(source, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> src, Func<IEnumerable<T>, T> fallback)
            => new First<T>(src, fallback);

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> src, Func<T, bool> condition, Func<IEnumerable<T>, T> fallback)
            => new First<T>(condition, src, (_, e) => fallback(e));

        /// <summary>
        /// First element in a <see cref="IEnumerable{T}"/> fallback function <see>
        ///     <cref>IBiFunc{X, Y, Z}</cref>
        /// </see>
        /// </summary>
        public static IScalar<T> First<T>(this IEnumerable<T> src, Func<T, bool> condition, Func<Exception, IEnumerable<T>, T> fallback)
            => new First<T>(condition, src, fallback);

    }
}
