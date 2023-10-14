

using System.Collections;
using System.Collections.Concurrent;
using Tonga.Func;

namespace Tonga.Collection
{
    /// <summary>
    /// An ArrayList converted to a IList&lt;object&gt;
    /// </summary>
    public sealed class ArrayListAsCollection : CollectionEnvelope<object>
    {
        /// <summary>
        /// A ArrayList converted to IList&lt;object&gt;
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ArrayListAsCollection(ArrayList src) : base(() =>
            {
                var blocking = new BlockingCollection<object>();
                foreach (var lst in src)
                {
                    new Each<object>(item => blocking.Add(item), lst).Invoke();
                }
                return blocking.GetConsumingEnumerable().GetEnumerator();
            },
            false
        )
        { }
    }
}
