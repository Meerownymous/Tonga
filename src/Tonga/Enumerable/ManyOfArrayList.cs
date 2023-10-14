

using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

#pragma warning disable NoGetOrSet // No Statics
#pragma warning disable CS1591

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="ArrayList"/> converted to IEnumerable&lt;object&gt;
    /// </summary>
    public sealed class ManyOfArrayList : IEnumerable<object>
    {
        private readonly ArrayList src;

        /// <summary>
        /// A ArrayList converted to IEnumerable&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ManyOfArrayList(ArrayList src)
        {
            this.src = src;
        }

        public IEnumerator<object> GetEnumerator()
        {
            foreach(var item in this.src)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// A <see cref="ArrayList"/> converted to IEnumerable&lt;T&gt;
    /// </summary>
    public sealed class ManyOfArrayList<T> : IEnumerable<T>
    {
        private readonly ArrayList src;

        /// <summary>
        /// A ArrayList converted to IEnumerable&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ManyOfArrayList(ArrayList src)
        {
            this.src = src;
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.src)
                yield return (T)item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
