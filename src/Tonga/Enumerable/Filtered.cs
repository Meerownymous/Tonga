

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
        private readonly IEnumerable<T> src;
        private readonly Func<T, bool> fnc;
        private readonly Ternary<T> result;

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="fnc">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public Filtered(Func<T, Boolean> fnc, T item1, T item2, params T[] items) : this(
            fnc,
            new Joined<T>(
                live: true,
                new Transit<T>(
                    item1,
                    item2
                ),
                new Params<T>(items)
            ),
            false
        )
        { }

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="fnc">filter function</param>
        /// <param name="item1">first item to filter</param>
        /// <param name="item2">secound item to filter</param>
        /// <param name="items">other items to filter</param>
        public Filtered(Func<T, Boolean> fnc, T item1, T item2, bool live, params T[] items) : this(
            fnc,
            new Joined<T>(
                live: true,
                new Transit<T>(
                    item1,
                    item2
                ),
                new Params<T>(items)
            ),
            live
        )
        { }

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        /// <param name="live">live or sticky</param>
        public Filtered(Func<T, Boolean> fnc, IEnumerable<T> src, bool live = false)
        {
            this.src = src;
            this.fnc = fnc;
            this.result =
                new Ternary<T>(
                    new Transit<T>(Produced),
                    new Sticky<T>(Produced),
                    live
                );

        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerator<T> Produced()
        {
            foreach (var item in this.src)
            {
                if(fnc.Invoke(item))
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
        public static IEnumerable<T> New<T>(Func<T, Boolean> fnc, T item1, T item2, params T[] items) =>
            new Filtered<T>(fnc, item1, item2, items);

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        public static IEnumerable<T> New<T>(Func<T, Boolean> fnc, IEnumerable<T> src) => new Filtered<T>(fnc, src);

        /// <summary>
        /// A filtered <see cref="IEnumerable{T}"/> which filters by the given condition <see cref="Func{In, Out}"/>.
        /// </summary>
        /// <param name="src">enumerable to filter</param>
        /// <param name="fnc">filter function</param>
        /// <param name="live">live or sticky</param>
        public static IEnumerable<T> New<T>(Func<T, Boolean> fnc, IEnumerable<T> src, bool live) => new Filtered<T>(fnc, src, live);
    }
}
