

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public sealed class None : IEnumerable<string>
    {
        /// <summary>
        /// Enumerable which is empty.
        /// </summary>
        public None()
        { }

        public IEnumerator<string> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public sealed class None<T> : IEnumerable<T>
    {
        /// <summary>
        /// Enumerable which is empty.
        /// </summary>
        public None()
        { }

        public IEnumerator<T> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
