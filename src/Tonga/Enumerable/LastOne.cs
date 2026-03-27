using System;
using System.Collections.Generic;
using Tonga.Scalar;
using Tonga.Text;

namespace Tonga.Enumerable;

/// <summary>
/// Last element in a <see cref="IEnumerable{T}"/>
/// </summary>
/// <typeparam name="T"></typeparam>
public sealed class LastOne<T> : ScalarEnvelope<T>
{
    /// <summary>
    /// Last element in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ex"></param>
    public LastOne(IEnumerable<T> source, Exception ex) : this(
        source,
        (_,_) => throw ex
    )
    { }

    /// <summary>
    /// Last element in a <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <param name="source">source enum</param>
    public LastOne(IEnumerable<T> source) : this(
            source,
            (ex, _) => throw new ArgumentException(
                new Formatted(
                    "Cannot get last element: {0}",
                    ex.Message
                ).Str()
            )
        )
    { }

    /// <summary>
    /// Last element in a <see cref="IEnumerable{T}"/> with a fallback value.
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="fallback">fallback func</param>
    public LastOne(IEnumerable<T> source, T fallback) : this(
        source,
        (_,_) => fallback
    )
    { }

    /// <summary>
    /// </summary>
    /// <param name="source">source enum</param>
    /// <param name="fallback">fallback func</param>
    public LastOne(IEnumerable<T> source, Func<Exception, IEnumerable<T>, T> fallback) : base(
            new FirstOne<T>(
                new Reversed<T>(source),
                fallback
            )
        )
    { }
}

    public static partial class EnumerableSmarts
    {
        /// <summary>
        /// Last element in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public static IScalar<T> LastOne<T>(this IEnumerable<T> source, Exception ex)
            => new LastOne<T>(source, ex);

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public static IScalar<T> LastOne<T>(this IEnumerable<T> source)
            => new LastOne<T>(source);

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> LastOne<T>(this IEnumerable<T> source, T fallback)
            => new LastOne<T>(source, fallback);

        /// <summary>
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> LastOne<T>(this IEnumerable<T> source, Func<Exception, IEnumerable<T>, T> fallback)
            => new LastOne<T>(source, fallback);
    }
