using System.Collections;
using System.Collections.ObjectModel;

namespace Tonga.Collection
{
    /// <summary>
    /// An ArrayList as a collection.
    /// </summary>
    public sealed class ArrayListAsCollection : CollectionEnvelope<object>
    {
        /// <summary>
        /// An ArrayList as a collection.
        /// </summary>
        /// <param name="src">source ArrayList</param>
        public ArrayListAsCollection(ArrayList src) : base(
            (() =>
            {
                var col = new Collection<object>();
                foreach (var item in src)
                {
                    col.Add(item);
                }
                return col;
            })
        )
        { }
    }
}
