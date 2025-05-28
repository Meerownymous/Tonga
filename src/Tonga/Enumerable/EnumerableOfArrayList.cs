using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

    /// <summary>
    /// A <see cref="ArrayList"/> converted to IEnumerable&lt;object&gt;
    /// </summary>
    public sealed class EnumerableOfArrayList(ArrayList src) : IEnumerable<object>
    {
        public IEnumerator<object> GetEnumerator()
        {
            foreach(var item in src)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    /// <summary>
    /// A <see cref="ArrayList"/> converted to IEnumerable&lt;T&gt;
    /// </summary>
    public sealed class ArrayListAsEnumerable<T>(ArrayList src) : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in src)
                yield return (T)item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
