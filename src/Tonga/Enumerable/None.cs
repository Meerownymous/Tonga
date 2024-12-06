

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public sealed class None<T>() : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            yield break;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    /// <summary>
    /// Enumerable which is empty.
    /// </summary>
    public static class None
    {
        /// <summary>
        /// Enumerable which is empty.
        /// </summary>
        public static None<T> _<T>() => new();
    }
}
