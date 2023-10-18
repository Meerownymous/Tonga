

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
    public sealed class Filtered<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> result;

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="fnc">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public Filtered(Func<T, Boolean> fnc, T item1, T item2, params T[] items) : this(
            fnc,
            Joined.Pipe(
                new EnumerableOf<T>(item1, item2),
                new EnumerableOf<T>(items)
            )
        )
        { }

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="pass">filter function</param>
        /// <param name="live">live or sticky</param>
        public Filtered(Func<T, Boolean> pass, IEnumerable<T> src)
        {
            this.result = EnumerableOf.Pipe(() => Produced(src, pass));
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

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
        public static IEnumerable<T> Pipe<T>(Func<T, Boolean> fnc, T item1, T item2, params T[] items) =>
            new Filtered<T>(fnc, item1, item2, items);

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public static IEnumerable<T> Pipe<T>(Func<T, Boolean> fnc, IEnumerable<T> src) => new Filtered<T>(fnc, src);

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="fnc">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public static IEnumerable<T> Sticky<T>(Func<T, Boolean> fnc, T item1, T item2, params T[] items) =>
            Enumerable.Sticky.New(new Filtered<T>(fnc, item1, item2, items));

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public static IEnumerable<T> Sticky<T>(Func<T, Boolean> fnc, IEnumerable<T> src) =>
            Enumerable.Sticky.New(new Filtered<T>(fnc, src));
    }
}
