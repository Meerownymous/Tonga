

using System;
using System.Collections.Generic;

using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Find the smallest item in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Min<T>(IEnumerable<Func<T>> items) : ScalarEnvelope<T>(() =>
    {
        var e = items.GetEnumerator();
        if (!e.MoveNext())
        {
            throw new ArgumentException("Can't find smaller element in an empty iterable");
        }

        T min = e.Current();
        while (e.MoveNext())
        {
            T next = e.Current();
            if (next.CompareTo(min) < 0)
            {
                min = next;
            }
        }
        return min;
    }) where T : IComparable<T>
    {
        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items"><see cref="Func{TResult}"/> functions which retrieve items to compare</param>
        public Min(params IScalar<T>[] items) : this(items.AsMapped(item => item.Value()))
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<T> items) : this(items.AsMapped<T,Func<T>>(item => () => item))
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<IScalar<T>> items) : this(items.AsMapped(item => item.Value()))
        { }

        /// <summary>
        /// Find the smallest item in the given items
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params T[] items) : this(items.AsMapped<T,Func<T>>(item => () => item))
        { }

        /// <summary>
        /// Find the smallest item in the given scalars.
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params Func<T>[] items) : this(items.AsEnumerable())
        { }
    }

    public static partial class EnumerableSmarts
    {
        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items"><see cref="Func{TResult}"/> functions which retrieve items to compare</param>
        public static IScalar<T> Min<T>(params Func<T>[] items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> Min<T>(this IEnumerable<T> items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in the given items
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> Min<T>(this T[] items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in the given scalars.
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> Min<T>(this IScalar<T>[] items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> Min<T>(this IEnumerable<Func<T>> items)
            where T : IComparable<T>
            => new Min<T>(items);
    }
}
