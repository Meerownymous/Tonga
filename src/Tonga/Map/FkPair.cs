using System;

namespace Tonga.Map;

/// <summary>
/// Fake Pair
/// </summary>
public sealed class FkPair<TKey, TValue>(Func<TKey> key, Func<TValue> value, Func<bool> isLazy) : IPair<TKey, TValue>
{
    public TValue Value() => value();
    public TKey Key() => key();
    public bool IsLazy() => isLazy();
}
