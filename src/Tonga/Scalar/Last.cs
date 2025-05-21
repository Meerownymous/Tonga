using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.Text;

namespace Tonga.Scalar
{
    /// <summary>
    /// Last element in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Last<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// Last element in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public Last(IEnumerable<T> source, Exception ex) : this(
            source,
            new AsFunc<IEnumerable<T>, T>(_ => throw ex)
        )
        { }

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public Last(IEnumerable<T> source) : this(
                source,
                new AsFunc<Exception, IEnumerable<T>, T>((ex, itr) =>
                {
                    throw
                        new ArgumentException(
                            new Formatted(
                                "Cannot get last element: {0}",
                                ex.Message
                            ).AsString()
                    );
                }))
        { }

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public Last(IEnumerable<T> source, T fallback) : this(
            source,
            new AsFunc<IEnumerable<T>, T>(b => fallback)
        )
        { }

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public Last(IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback) : this(
            source,
            (_, enumerable) => fallback.Invoke(enumerable)
        )
        { }

        /// <summary>
        /// Last Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public Last(IEnumerable<T> source, Func<Exception, IEnumerable<T>, T> fallback) : this(
            source,
            new AsFunc<Exception, IEnumerable<T>, T>((ex, enumerable) => fallback.Invoke(ex, enumerable)
            )
        )
        { }

        /// <summary>
        /// Last Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public Last(IEnumerable<T> source, IFunc<Exception, IEnumerable<T>, T> fallback)
            : base(
                  new ItemAt<T>(
                    new Reversed<T>(source),
                    fallback
                )
            )
        { }
    }

    public static class Last
    {
        /// <summary>
        /// Last element in <see cref="IEnumerable{T}"/> with given Exception thrown on fallback
        /// </summary>
        /// <param name="source"></param>
        /// <param name="ex"></param>
        public static IScalar<T> _<T>(IEnumerable<T> source, Exception ex)
            => new Last<T>(source, ex);

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        public static IScalar<T> _<T>(IEnumerable<T> source)
            => new Last<T>(source);

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/> with a fallback value.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, T fallback)
            => new Last<T>(source, fallback);

        /// <summary>
        /// Last element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, IFunc<IEnumerable<T>, T> fallback)
            => new Last<T>(source, fallback);

        /// <summary>
        /// Last Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, Func<Exception, IEnumerable<T>, T> fallback)
            => new Last<T>(source, fallback);

        /// <summary>
        /// Last Element in a <see cref="IEnumerable{T}"/> fallback function <see cref="IFunc{In, Out}"/>.
        /// </summary>
        /// <param name="source">source enum</param>
        /// <param name="fallback">fallback func</param>
        public static IScalar<T> _<T>(IEnumerable<T> source, IFunc<Exception, IEnumerable<T>, T> fallback)
            => new Last<T>(source, fallback);
    }
}
