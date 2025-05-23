

using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.Scalar;

namespace Tonga.Collection
{
    /// <summary>
    /// A filtered <see cref="ICollection{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Filtered<T>(Func<T, Boolean> func, IEnumerable<T> src) : CollectionEnvelope<T>(
        AsCollection._(
            Enumerable.Filtered._(
                func, src
            )
        )
    )
    {
        /// <summary>
        /// A filtered <see cref="ICollection{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="func">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public Filtered(Func<T, Boolean> func, T item1, T item2, params T[] items) : this(
                func,
                new AsEnumerable<T>(() =>
                    new Enumerable.Joined<T>(
                        new AsEnumerable<T>(item1, item2),
                        items
                    ).GetEnumerator()
                )
            )
        { }

        /// <summary>
        /// A <see cref="ICollection{T}"/> filtered by the given <see cref="Func{T, TResult}"/>
        /// </summary>
        /// <param name="func">filter func</param>
        /// <param name="src">items to filter</param>
        public Filtered(Func<T, Boolean> func, IEnumerator<T> src) : this(func, new AsEnumerable<T>(src))
        { }
    }

    /// <summary>
    /// A <see cref="ICollection{T}"/> filtered by the given <see cref="Func{T, TResult}"/>
    /// </summary>
    public static class Filtered
    {
        /// <summary>
        /// A <see cref="ICollection{T}"/> filtered by the given <see cref="Func{T, TResult}"/>
        /// </summary>
        public static ICollection<T> _<T>(Func<T, Boolean> func, IEnumerable<T> src) => new Filtered<T>(func, src);

        /// <summary>
        /// A <see cref="ICollection{T}"/> filtered by the given <see cref="Func{T, TResult}"/>
        /// </summary>
        public static ICollection<T> _<T>(Func<T, Boolean> func, IEnumerator<T> src) => new Filtered<T>(func, src);

        /// <summary>
        /// A <see cref="ICollection{T}"/> filtered by the given <see cref="Func{T, TResult}"/>
        /// </summary>
        public static ICollection<T> _<T>(Func<T, Boolean> func, T item1, T item2, params T[] items) => new Filtered<T>(func, item1, item2, items);
    }
}
