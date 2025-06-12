using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Multiple <see cref="IEnumerable{T}"/> joined together.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Joined<T>(IEnumerable<IEnumerable<T>> items) : IEnumerable<T>
    {
        private readonly IEnumerable<T> result =
            new AsEnumerable<T>(() => Produced(items));

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public Joined(T first, T second, IEnumerable<T> lst, params T[] items) : this(
            new AsEnumerable<T>(first, second),
            lst,
            new AsEnumerable<T>(items)
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public Joined(T first, IEnumerable<T> lst, params T[] items) : this(
            new AsEnumerable<IEnumerable<T>>(
                first.AsSingle(),
                lst,
                items.AsEnumerable()
            )
        )
        { }

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public Joined(IEnumerable<T> lst, params T[] items) : this(
            lst,
            items.AsEnumerable()
        )
        { }

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        public Joined(params IEnumerable<T>[] items) : this(
            items.AsEnumerable()
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

    public static partial class EnumerableSmarts
    {
        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public static IEnumerable<T> AsJoined<T>(this IEnumerable<T> lst, IEnumerable<T> items) =>
            new Joined<T>(lst, items);

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public static IEnumerable<T> AsJoined<T>(this IEnumerable<T> lst, T item) =>
            new Joined<T>(lst, item);

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public static IEnumerable<T> AsJoined<T>(this IEnumerable<T> lst, params T[] item) =>
            new Joined<T>(lst, item);

        /// <summary>
        /// Join a <see cref="IEnumerable{T}"/> with (multiple) single Elements.
        /// </summary>
        public static IEnumerable<string> AsJoined<T>(this IEnumerable<string> lst, string item) =>
            new Joined<string>(lst, item);

        /// <summary>
        /// Multiple <see cref="IEnumerable{T}"/> Joined2 together.
        /// </summary>
        /// <param name="items">enumerables to join</param>
        public static IEnumerable<T> AsJoined<T>(this IEnumerable<IEnumerable<T>> items) =>
            new Joined<T>(items);
    }
}
