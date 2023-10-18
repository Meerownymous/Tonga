

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A reversed <see cref="IEnumerable{T}"/>
    /// </summary>
    /// <typeparam name="X">type of items in enumerable</typeparam>
    public sealed class Reversed<X> : IEnumerable<X>
    {
        private readonly IEnumerable<X> result;

        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public Reversed(IEnumerable<X> src)
        {
            this.result = EnumerableOf.Pipe(() => Produced(src));
        }

        public IEnumerator<X> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

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
        public static IEnumerable<T> Pipe<T>(IEnumerable<T> src) =>
            new Reversed<T>(src);

        /// <summary>
        /// A reversed <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <param name="src">enumerable to reverse</param>
        public static IEnumerable<T> Sticky<T>(IEnumerable<T> src) =>
            Enumerable.Sticky.New(new Reversed<T>(src));
    }
}
