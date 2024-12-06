

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable partitioned by a given size.
    /// <para>Is a IEnumerable</para>
    /// </summary>
    public sealed class Partitioned<T>(int size, IEnumerable<T> items) : IEnumerable<IEnumerable<T>>
    {
        public IEnumerator<IEnumerable<T>> GetEnumerator()
        {
            var source = items.GetEnumerator();
            while(source.MoveNext())
            {
                yield return Partition(source);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private IEnumerable<T> Partition(IEnumerator<T> source)
        {
            var taken = 1;
            yield return source.Current;
            while(taken < size && source.MoveNext())
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
        public static IEnumerable<IEnumerable<T>> _<T>(int size, IEnumerable<T> items) =>
            new Partitioned<T>(size, items);
    }

    public static class PartitionedSmarts
    {
        /// <summary>
        /// Enumerable partitioned by a given size.
        /// </summary>
        public static IEnumerable<IEnumerable<T>> Partitioned<T>(this IEnumerable<T> items, int size) =>
            new Partitioned<T>(size, items);
    }
}
