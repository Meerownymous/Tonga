using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A filtered <see cref="IEnumerable{T}"/>.
    /// Pass a filter function which will applied to all items, similar to List{T}.Where(...) in LinQ
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Filtered<T>(Func<T, Boolean> pass, IEnumerable<T> src) : IEnumerable<T>
    {
        private readonly IEnumerable<T> result =
            new AsEnumerable<T>(() => Produced(src, pass));

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="fnc">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public Filtered(Func<T, Boolean> fnc, T item1, T item2, params T[] items) : this(
            fnc,
            Joined._(
                new AsEnumerable<T>(item1, item2),
                new AsEnumerable<T>(items)
            )
        )
        { }

        public IEnumerator<T> GetEnumerator() =>
            this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() =>
            this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<T> source, Func<T, Boolean> pass)
        {
            foreach (var item in source)
            {
                if(pass.Invoke(item))
                {
                    yield return item;
                }

            }
        }
    }

    /// <summary>
    /// A filtered <see cref="IEnumerable{T}"/>.
    /// Pass a filter function which will applied to all items, similar to List{T}.Where(...) in LinQ
    /// </summary>
    public static class Filtered
    {
        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="fnc">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public static IEnumerable<T> _<T>(Func<T, Boolean> fnc, T item1, T item2, params T[] items) =>
            new Filtered<T>(fnc, item1, item2, items);

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public static IEnumerable<T> _<T>(Func<T, Boolean> fnc, IEnumerable<T> src) => new Filtered<T>(fnc, src);

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public static IEnumerable<T> _<T>(this IEnumerable<T> src, Func<T, Boolean> fnc) => new Filtered<T>(fnc, src);
    }

    public static class FilteredSmarts
    {
        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        public static IEnumerable<T> Filtered<T>(this T[] items, Func<T, Boolean> fnc) =>
            new Filtered<T>(fnc, items);

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        public static IEnumerable<T> Filtered<T>(this IEnumerable<T> src, Func<T, Boolean> fnc) => new Filtered<T>(fnc, src);
    }
}
