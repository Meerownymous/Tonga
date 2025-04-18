using System;
using System.Collections.Generic;

namespace Tonga;

public interface IMap<Key, Value>
{
    Value this[Key key] { get; }
    Func<Value> Lazy(Key key);
    ICollection<Key> Keys();
    IEnumerable<IPair<Key, Value>> Pairs();
    IMap<Key, Value> With(IPair<Key, Value> pair);
}
