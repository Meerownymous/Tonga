

using System;
using System.Collections.Generic;

using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Find the smallest item in a <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Min<T> : Scalar.ScalarEnvelope<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items"><see cref="System.Func{TResult}"/> functions which retrieve items to compare</param>
        public Min(params System.Func<T>[] items) : this(
            new Mapped<System.Func<T>, IScalar<T>>(
                item => AsScalar._(() => item.Invoke()),
                AsEnumerable._(items)
            )
        )
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<T> items) : this(
            Mapped._(
                item => AsScalar._(item),
                items
            )
        )
        { }

        /// <summary>
        /// Find the smallest item in the given items
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params T[] items) : this(
            Mapped._(
                item => AsScalar._(item),
                items
            )
        )
        { }

        /// <summary>
        /// Find the smallest item in the given scalars.
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(params IScalar<T>[] items) : this(AsEnumerable._(items))
        { }

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public Min(IEnumerable<IScalar<T>> items)
            : base(() =>
            {
                var e = items.GetEnumerator();
                if (!e.MoveNext())
                {
                    throw new ArgumentException("Can't find smaller element in an empty iterable");
                }

                T min = e.Current.Value();
                while (e.MoveNext())
                {
                    T next = e.Current.Value();
                    if (next.CompareTo(min) < 0)
                    {
                        min = next;
                    }
                }
                return min;
            })
        { }
    }

    public sealed class Min
    {
        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items"><see cref="System.Func{TResult}"/> functions which retrieve items to compare</param>
        public static IScalar<T> New<T>(params System.Func<T>[] items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> New<T>(IEnumerable<T> items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in the given items
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> New<T>(params T[] items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in the given scalars.
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> New<T>(params IScalar<T>[] items)
            where T : IComparable<T>
            => new Min<T>(items);

        /// <summary>
        /// Find the smallest item in a <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">items to compare</param>
        public static IScalar<T> New<T>(IEnumerable<IScalar<T>> items)
            where T : IComparable<T>
            => new Min<T>(items);
    }
}
