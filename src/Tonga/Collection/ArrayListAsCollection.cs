using System.Collections;
using System.Collections.ObjectModel;
using Tonga.Func;

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
            AsCollection._(() =>
            {
                var col = new Collection<object>();
                foreach (var lst in src)
                {
                    new Each<object>(item => col.Add(item), lst).Invoke();
                }
                return col;
            })
        )
        { }
    }
}
