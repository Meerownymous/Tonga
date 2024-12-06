

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that starts from the beginning when ended.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class Cycled<T>(IEnumerable<T> enumerable) : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            var copies = new List<T>();
            foreach(var item in enumerable)
            {
                copies.Add(item);
                yield return item;
            }

            var current = -1;
            while(true)
            {
                current++;
                if(current >= copies.Count)
                {
                    current = 0;
                }
                yield return copies[current];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that starts from the beginning when ended.
    /// </summary>
    public static class Cycled
    {
        public static IEnumerable<T> _<T>(IEnumerable<T> enumerable) => new Cycled<T>(enumerable);
    }

    public static class CycledSmarts
    {
        public static IEnumerable<TItem> Cycled<TItem>(this TItem[] source) =>
            new Cycled<TItem>(source);
    }
}
