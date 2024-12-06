

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
    public sealed class Joined<T>(IEnumerable<IEnumerable<T>> items) : IEnumerable<T>
    {
        private readonly IEnumerable<T> result =
            new AsEnumerable<T>(() => Produced(items));

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, T second, IEnumerable<T> lst, params T[] items) : this(
            AsEnumerable._(first, second),
            lst,
            AsEnumerable._(items)
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(T first, IEnumerable<T> lst, params T[] items) : this(
            AsEnumerable._(
                Single._(first),
                lst,
                AsEnumerable._(items)
            )
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public Joined(IEnumerable<T> lst, params T[] items) : this(
            AsEnumerable._(lst,
                AsEnumerable._(items)
            )
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public Joined(params IEnumerable<T>[] items) : this(
            AsEnumerable._(items)
        )
        { }

        public IEnumerator<T> GetEnumerator() => result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<IEnumerable<T>> items)
        {
            foreach (var enumerable in items)
            {
                foreach (var item in enumerable)
                {
                    yield return item;
                }
            }
        }
    }

    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> Joined together.
    /// </summary>
    public static class Joined
    {
        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        /// <param name="lst">enumerable of items to join</param>
        /// <param name="items">array of items to join</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> lst, params T[] items) => new Joined<T>(lst, items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> _<T>(params IEnumerable<T>[] items) => new Joined<T>(items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> _<T>(IEnumerable<IEnumerable<T>> items) => new Joined<T>(items);
    }

    public static class JoinedSmarts
    {
        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public static IEnumerable<T> Joined<T>(this IEnumerable<T> lst, params T[] items) => new Joined<T>(lst, items);

        /// <summary>
        /// Join a single Element with an enumerable.
        /// </summary>
        public static IEnumerable<T> Joined<T>(this T firstItem, IEnumerable<T> items) => new Joined<T>(new Single<T>(firstItem), items);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> Joined<T>(this IEnumerable<T>[] items) => new Joined<T>(items);


        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> Joined<T>(this IEnumerable<IEnumerable<T>> items) => new Joined<T>(items);
    }
}
