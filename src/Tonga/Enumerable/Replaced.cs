

using System;
using System.Collections;
using System.Collections.Generic;
using Tonga.Func;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="EnumerableEnvelope"/> whose items are replaced if they match a condition.
    /// </summary>
    /// <typeparam name="T">type of items in enumerable</typeparam>
    public sealed class Replaced<T>(IEnumerable<T> origin, Func<T, bool> condition, T replacement) : IEnumerable<T>
    {
        private readonly IEnumerable<T> result =
            new AsEnumerable<T>(() => Produced(origin, condition, replacement));

        /// <summary>
        /// A <see cref="EnumerableEnvelope"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, IFunc<T, bool> condition, T replacement) : this(
            origin,
            condition.Invoke,
            replacement
        )
        { }

        /// <summary>
        /// A <see cref="EnumerableEnvelope"/> where an item at a given index is replaced.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="index">index at which to replace the item</param>
        /// <param name="replacement">item to insert instead</param>
        public Replaced(IEnumerable<T> origin, int index, T replacement) : this(
            Mapped._(
                (item, itemIndex) => itemIndex == index ? replacement : item,
                origin
            ),
            _ => false,
            replacement
        )
        { }

        public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<T> origin, Func<T, bool> condition, T replacement)
        {
            var e = origin.GetEnumerator();
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
    /// A <see cref="EnumerableEnvelope"/> whose items are replaced if they match a condition.
    /// </summary>
    public static class Replaced
    {
        /// <summary>
        /// A <see cref="EnumerableEnvelope"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> origin, Func<T, bool> condition, T replacement) =>
            new Replaced<T>(origin, condition, replacement);

        /// <summary>
        /// A <see cref="EnumerableEnvelope"/> where an item at a given index is replaced.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="index">index at which to replace the item</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> origin, int index, T replacement) =>
            new Replaced<T>(origin, index, replacement);

        /// <summary>
        /// A <see cref="EnumerableEnvelope"/> whose items are replaced if they match a condition.
        /// </summary>
        /// <param name="origin">enumerable</param>
        /// <param name="condition">matching condition</param>
        /// <param name="replacement">item to insert instead</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> origin, IFunc<T, bool> condition, T replacement) =>
            new Replaced<T>(origin, condition, replacement);
    }

    /// <summary>
    /// A <see cref="EnumerableEnvelope"/> whose items are replaced if they match a condition.
    /// </summary>
    public static class ReplacedSmarts
    {
        /// <summary>
        /// A <see cref="IEnumerable"/> whose items are replaced if they match a condition.
        /// </summary>
        public static IEnumerable<T> Replaced<T>(this IEnumerable<T> origin, Func<T, bool> condition, T replacement) =>
            new Replaced<T>(origin, condition, replacement);

        /// <summary>
        /// A <see cref="EnumerableEnvelope"/> where an item at a given index is replaced.
        /// </summary>
        public static IEnumerable<T> Replaced<T>(this IEnumerable<T> origin, int index, T replacement) =>
            new Replaced<T>(origin, index, replacement);

        /// <summary>
        /// A <see cref="EnumerableEnvelope"/> whose items are replaced if they match a condition.
        /// </summary>
        public static IEnumerable<T> Replaced<T>(this IEnumerable<T> origin, IFunc<T, bool> condition, T replacement) =>
            new Replaced<T>(origin, condition, replacement);
    }
}
