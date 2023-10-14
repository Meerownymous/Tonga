

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Func;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
    /// </summary>
    /// <typeparam name="T">type of items in enumerable</typeparam>
    public sealed class Replaced<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> origin;
        private readonly IFunc<T, bool> condition;
        private readonly T replacement;
        private readonly Ternary<T> result;

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, Func<T, bool> condition, T replacement, bool live = false) : this(
            origin,
            new FuncOf<T, bool>(condition),
            replacement,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable"/> where an item at a given index is replaced.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="index">index at which to replace the item</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, int index, T replacement, bool live = false) : this(
            new Mapped<T, T>(
                (item, itemIndex) => itemIndex == index ? replacement : item,
                origin,
                live: true
            ),
            item => false,
            replacement,
            live
        )
        { }

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, IFunc<T, bool> condition, T replacement, bool live = false)
        {
            this.origin = origin;
            this.condition = condition;
            this.replacement = replacement;
            this.result =
                Ternary.New(
                    Transit.Of(Produced),
                    Sticky.New(Produced),
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
            var e = this.origin.GetEnumerator();

            while (e.MoveNext())
            {
                if (condition.Invoke(e.Current))
                {
                    yield return replacement;
                }
                else
                {
                    yield return e.Current;
                }
            }
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
    /// </summary>
    public static class Replaced
    {
        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, Func<T, bool> condition, T replacement, bool live = false) =>
            new Replaced<T>(origin, condition, replacement, live);

        /// <summary>
        /// A <see cref="IEnumerable"/> where an item at a given index is replaced.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="index">index at which to replace the item</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, int index, T replacement, bool live = false) =>
            new Replaced<T>(origin, index, replacement, live);

        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, IFunc<T, bool> condition, T replacement, bool live = false) =>
            new Replaced<T>(origin, condition, replacement, live);
    }
}
