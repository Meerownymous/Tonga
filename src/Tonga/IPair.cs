

namespace Tonga;

/// <summary>
/// A key-value pair to add to a map.
/// </summary>
public interface IPair<TKey, TValue>
{
    TKey Key();
    TValue Value();
    bool IsLazy();
}
