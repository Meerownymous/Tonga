

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{T}"/> that repeats one element infinitely.
    /// </summary>
    /// <typeparam name="T">type of the elements</typeparam>
    public sealed class Endless<T> : IEnumerable<T>
    {
        private readonly T elm;

        /// <summary>
        /// A <see cref="IEnumerable"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public Endless(T elm)
        {
            this.elm = elm;
        }

        public IEnumerator<T> GetEnumerator()
        {
            while (true)
            {
                yield return this.elm;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class Endless
    {
        /// <summary>
        /// A <see cref="IEnumerable"/> that repeats one element infinitely.
        /// </summary>
        /// <param name="elm">element to repeat</param>
        public static IEnumerable<T> New<T>(T elm) => new Endless<T>(elm);
    }
}
