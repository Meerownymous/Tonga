

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that starts from the beginning when ended.
    /// </summary>
    /// <typeparam name="T">type of the contents</typeparam>
    public sealed class Cycled<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> enumerable;

        /// <summary>
        /// A <see cref="IEnumerator{T}"/> that starts from the beginning when ended.
        /// </summary>
        /// <param name="enumerable">an enum to cycle</param>
        public Cycled(IEnumerable<T> enumerable)
        {
            this.enumerable = enumerable;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var copies = new List<T>();
            foreach(var item in this.enumerable)
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
    /// <typeparam name="T">type of the contents</typeparam>
    public static class Cycled
    {
        public static IEnumerable<T> New<T>(IEnumerable<T> enumerable) => new Cycled<T>(enumerable);
    }
}
