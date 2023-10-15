

using System.Collections;
using System.Collections.Generic;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Tonga.Enumerable
{
    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T> : IEnumerable<T>
    {
        private readonly IEnumerable<IEnumerable<T>> enumerables;
        private readonly Ternary<T> result;

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, T second, IEnumerable<T> lst, bool live = false, params T[] items) : this(
            live,
            EnumerableOf.Pipe(first, second),
            lst,
            new EnumerableOf<T>(items)
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, IEnumerable<T> lst, bool live = false, params T[] items) : this(
            new EnumerableOf<IEnumerable<T>>(
                new Single<T>(first),
                lst,
                new EnumerableOf<T>(items)
            ),
            live
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(bool live, IEnumerable<T> lst, params T[] items) : this(
            lst, live, items
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(IEnumerable<T> lst, params T[] items) : this(
            lst, false, items
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(IEnumerable<T> lst, bool live = false, params T[] items) : this(
            new EnumerableOf<IEnumerable<T>>(lst,
                new EnumerableOf<T>(items)
            ),
            live
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(params IEnumerable<T>[] items) : this(
            false, items
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(bool live = false, params IEnumerable<T>[] items) : this(
            new EnumerableOf<IEnumerable<T>>(items), live
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(IEnumerable<IEnumerable<T>> items, bool live = false)
        {
            this.enumerables = items;
            this.result =
                Ternary.Pipe(
                    EnumerableOf.Pipe(Produced),
                    Sticky.New(Produced),
                    live
                );

        }

        public IEnumerator<T> GetEnumerator()
        {
            return result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerator<T> Produced()
        {
            foreach (var enumerable in this.enumerables)
            {
                foreach (var item in enumerable)
                {
                    yield return item;
                }
            }
        }
    }

    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
    /// </summary>
    public static class Joined
    {
        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> lst, params T[] items) => new Joined<T>(lst, items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> New<T>(params IEnumerable<T>[] items) => new Joined<T>(items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> New<T>(IEnumerable<IEnumerable<T>> items) => new Joined<T>(items);

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public static IEnumerable<T> Pipe<T>(IEnumerable<T> lst, params T[] items) => new Joined<T>(live: true, lst, items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> Pipe<T>(params IEnumerable<T>[] items) => new Joined<T>(items, live: true);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> Pipe<T>(IEnumerable<IEnumerable<T>> items) => new Joined<T>(items, live: true);
    }
}
