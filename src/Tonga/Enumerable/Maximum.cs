using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// The greatest item in the given <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Maximum<T>(IEnumerable<Func<T>> items) : ScalarEnvelope<T>(
        () =>
        {
            var e = items.GetEnumerator();
            if (!e.MoveNext())
            {
                throw new ArgumentException("Can't find greater element in an empty iterable");
            }

            T max = e.Current();
            while (e.MoveNext())
            {
                T next = e.Current();
                if (next.CompareTo(max) > 0)
                {
                    max = next;
                }
            }
            return max;
        }
    ) where T : IComparable<T>
    {
        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Maximum(params Func<T>[] items) : this(
            items
                .AsEnumerable()
                .AsMapped(item => item.Invoke())
        )
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Maximum(IEnumerable<T> items) : this(
            items.AsMapped(item => new Func<T>(() => item))
        )
        { }

        /// <summary>
        /// The greatest item in the given items.
        /// </summary>
        /// <param name="items">list of items</param>
        public Maximum(params T[] items) : this(
            items.AsMapped(item => new Func<T>(() => item))
        )
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">scalars of items to compare</param>
        public Maximum(params IScalar<T>[] items) : this(items.AsMapped(item => item.Value()))
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">scalars of items to compare</param>
        public Maximum(IEnumerable<IScalar<T>> items) : this(items.AsMapped(item => item.Value()))
        { }
    }

    public static partial class EnumerableSmarts
    {
        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> Maximum<T>(this Func<T>[] items) where T : IComparable<T>
            => new Maximum<T>(items);

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> Maximum<T>(this IEnumerable<T> items)
            where T : IComparable<T>
            => new Maximum<T>(items);

        /// <summary>
        /// The greatest item in the given items.
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> Maximum<T>(this T[] items)
            where T : IComparable<T>
            => new Maximum<T>(items);

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> Maximum<T>(this IScalar<T>[] items)
            where T : IComparable<T>
            => new Maximum<T>(items);

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> Maximum<T>(this IEnumerable<Func<T>> items)
            where T : IComparable<T>
            => new Maximum<T>(items);
    }
}
