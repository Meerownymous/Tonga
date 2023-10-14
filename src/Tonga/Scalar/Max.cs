

using System;
using System.Collections.Generic;

using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// The greatest item in the given <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Max<T> : ScalarEnvelope<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(params Func<T>[] items) : this(
            new Enumerable.Mapped<Func<T>, IScalar<T>>(
                item => new Live<T>(() => item.Invoke()),
                new ManyOf<Func<T>>(items)
            )
        )
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(IEnumerable<T> items) : this(
            new Enumerable.Mapped<T, IScalar<T>>(item => new Live<T>(item), items))
        { }

        /// <summary>
        /// The greatest item in the given items.
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(params T[] items) : this(
            new Enumerable.Mapped<T, IScalar<T>>(item => new Live<T>(item), items))
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(params IScalar<T>[] items) : this(new ManyOf<IScalar<T>>(items))
        { }

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public Max(IEnumerable<IScalar<T>> items)
            : base(() =>
            {
                var e = items.GetEnumerator();
                if (!e.MoveNext())
                {
                    throw new ArgumentException("Can't find greater element in an empty iterable");
                }

                T max = e.Current.Value();
                while (e.MoveNext())
                {
                    T next = e.Current.Value();
                    if (next.CompareTo(max) > 0)
                    {
                        max = next;
                    }
                }
                return max;
            })
        { }
    }

    public static class Max
    {
        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> New<T>(params Func<T>[] items)
            where T : IComparable<T>
            => new Max<T>(items);

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> New<T>(IEnumerable<T> items)
            where T : IComparable<T>
            => new Max<T>(items);

        /// <summary>
        /// The greatest item in the given items.
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> New<T>(params T[] items)
            where T : IComparable<T>
            => new Max<T>(items);

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> New<T>(params IScalar<T>[] items)
            where T : IComparable<T>
            => new Max<T>(items);

        /// <summary>
        /// The greatest item in the given <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="items">list of items</param>
        public static IScalar<T> New<T>(IEnumerable<IScalar<T>> items)
            where T : IComparable<T>
            => new Max<T>(items);
    }
}
