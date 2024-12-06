using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A given enumerator as enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class EnumeratorAsEnumerable<T>(Func<IEnumerator<T>> enumerator) : IEnumerable<T>
    {
        /// <summary>
        /// A given enumerator as enumerable.
        /// </summary>
        public EnumeratorAsEnumerable(IEnumerator<T> enumerator) : this(() => enumerator)
        { }

        public IEnumerator<T> GetEnumerator()
        {
            var enm = enumerator();

            while (enm.MoveNext())
            {
                yield return enm.Current;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}

