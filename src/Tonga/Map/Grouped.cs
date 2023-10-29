using System.Collections.Generic;
using Tonga.List;

namespace Tonga.Map
{
    /// <summary>
    /// Groups a list to Keys and Lists of Values according to the given Functions
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="Key"></typeparam>
    /// <typeparam name="Value"></typeparam>
    public sealed class Grouped<T, Key, Value> : MapEnvelope<Key, IList<Value>>
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">Source Enumerable</param>
        /// <param name="key">Function to convert Source Type to Key Type</param>
        /// <param name="value">Function to Convert Source Type to Key Týpe</param>
        public Grouped(IEnumerable<T> src, IFunc<T, Key> key, IFunc<T, Value> value) : base(
            () =>
            {
                IMap<Key, IList<Value>> temp = new Empty<Key, IList<Value>>();
                foreach (var entry in src)
                {
                    temp =
                        temp.With(
                            AsPair._(key.Invoke(entry), Mapped._(value, src))
                        );
                }
                return temp;
            }
        )
        { }
    }

    public static class Grouped
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">Source Enumerable</param>
        /// <param name="key">Function to convert Source Type to Key Type</param>
        /// <param name="value">Function to Convert Source Type to Key Týpe</param>
        public static IMap<Key, IList<Value>> _<T, Key, Value>(IEnumerable<T> src, IFunc<T, Key> key, IFunc<T, Value> value)
            => new Grouped<T, Key, Value>(src, key, value);
    }
}
