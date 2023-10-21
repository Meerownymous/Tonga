

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable partitioned by a given size.
    /// <para>Is a IEnumerable</para>
    /// </summary>
    public sealed class Partitioned<T> : IEnumerable<IEnumerable<T>>
    {
        private readonly int size;
        private readonly IEnumerable<T> items;

        /// <summary>
        /// Enumerable partitioned by a given size.
        /// </summary>
        public Partitioned(int size, IEnumerable<T> items)
        {
            this.size = size;
            this.items = items;
        }

        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            var source = this.items.GetEnumerator();
            while(source.MoveNext())
            {
                yield return Partition(source);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private IEnumerable<T> Partition(IEnumerator<T> source)
        {
            var taken = 1;
            yield return source.Current;
            while(taken < this.size && source.MoveNext())
            {
                taken++;
                yield return source.Current;
            }
        }
    }

    /// <summary>
    /// Enumerable partitioned by a given size.
    /// <para>Is a IEnumerable</para>
    /// </summary>
    public static class Partitioned
    {
        /// <summary>
        /// Enumerable partitioned by a given size.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> From<T>(int size, IEnumerable<T> items) =>
            new Partitioned<T>(size, items);

        /// <summary>
        /// Enumerable partitioned by a given size.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Sticky<T>(int size, IEnumerable<T> items) =>
            Enumerable.Sticky.From(new Partitioned<T>(size, items));
    }
}
