using System.Collections.Generic;
using System.Linq;
using Tonga.Enumerable;
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
        public Grouped(IEnumerable<T> src, System.Func<T, Key> key, System.Func<T, Value> value) : base(
            new AsMap<Key, IList<Value>>(() =>
            {
                IMap<Key, IList<Value>> temp = new Empty<Key, IList<Value>>();
                var fix = src.ToArray();
                foreach (var entry in fix)
                {
                    temp =
                        temp.With(
                            (key.Invoke(entry),
                                new AsList<Value>(
                                    fix.AsMapped(value)
                                )
                            ).AsPair()
                        );
                }
                return temp.Pairs();
            })
        )
        { }
    }

    public static partial class MapSmarts
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="src">Source Enumerable</param>
        /// <param name="key">Function to convert Source Type to Key Type</param>
        /// <param name="value">Function to Convert Source Type to Key Týpe</param>
        public static IMap<Key, IList<Value>> AsGrouped<T, Key, Value>(
            this IEnumerable<T> src,
            System.Func<T, Key> key,
            System.Func<T, Value> value
        )
            => new Grouped<T, Key, Value>(src, key, value);
    }
}
