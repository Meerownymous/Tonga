using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumeration of a single item.
    /// </summary>
    public class Single<T>(T item) : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public static class Single
    {
        public static Single<T> _<T>(T item) => new(item);
    }
}

