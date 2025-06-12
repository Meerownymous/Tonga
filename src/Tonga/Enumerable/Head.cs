using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
    /// </summary>
    /// <typeparam name="T">type of elements</typeparam>
    public sealed class Head<T>(IEnumerable<T> source, Func<int> limit) : IEnumerable<T>
    {
        private readonly IEnumerable<T> result =
            new AsEnumerable<T>(() => Produced(source, limit));

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        public Head(IEnumerable<T> enumerable) : this(enumerable, 1)
        { }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public Head(IEnumerable<T> enumerable, int limit) : this(
            enumerable,
            () => limit
        )
        { }

        public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<T> source, Func<int> limit)
        {
            var max = limit();
            var taken = 0;
            var enumerator = source.GetEnumerator();
            while (enumerator.MoveNext() && taken < max)
            {
                taken++;
                yield return enumerator.Current;
            }
        }
    }

    public static partial class EnumerableSmarts
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        public static IEnumerable<T> AsHead<T>(this IEnumerable<T> enumerable) => new Head<T>(enumerable);

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public static IEnumerable<T> AsHead<T>(this IEnumerable<T> enumerable, int limit) => new Head<T>(enumerable, limit);

        /// <summary>
        /// A <see cref="IEnumerable{T}"/> limited to an item maximum.
        /// </summary>
        /// <param name="enumerable">enumerable to limit</param>
        /// <param name="limit">maximum item count</param>
        public static IEnumerable<T> AsHead<T>(this IEnumerable<T> enumerable, Func<int> limit) => new Head<T>(enumerable, limit);
    }
}
