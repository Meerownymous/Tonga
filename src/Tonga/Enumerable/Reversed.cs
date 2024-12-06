

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="X">type of items in enumerable</typeparam>
    public sealed class Reversed<X>(IEnumerable<X> src) : IEnumerable<X>
    {
        private readonly IEnumerable<X> result =
            new AsEnumerable<X>(() => Produced(src));

        public IEnumerator<X> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<X> Produced(IEnumerable<X> source)
        {
            var items = new List<X>(source);
            items.Reverse();
            foreach(var item in items)
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class Reversed
    {
        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> src) =>
            new Reversed<T>(src);
    }

    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class ReversedSmarts
    {
        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public static IEnumerable<T> Reversed<T>(this IEnumerable<T> src) =>
            new Reversed<T>(src);
    }
}
