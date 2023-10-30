using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A given enumerator as enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EnumeratorAsEnumerable<T> : IEnumerable<T>
    {
        private readonly Func<IEnumerator<T>> enumerator;

        /// <summary>
        /// A given enumerator as enumerable.
        /// </summary>
        public EnumeratorAsEnumerable(IEnumerator<T> enumerator) : this(() => enumerator)
        { }

        /// <summary>
        /// A given enumerator as enumerable.
        /// </summary>
        public EnumeratorAsEnumerable(Func<IEnumerator<T>> enumerator)
        {
            this.enumerator = enumerator;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = this.enumerator();

            while (enumerator.MoveNext())
            {
                yield return enumerator.Current;
            }
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}

