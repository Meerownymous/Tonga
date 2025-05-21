

namespace Tonga;

/// <summary>
/// A key-value pair to add to a map.
/// </summary>
public interface IPair<out TKey, out TValue>
{
    TKey Key();
    TValue Value();
    bool IsLazy();
}
