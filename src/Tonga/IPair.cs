

namespace Tonga
{
    /// <summary>
    /// A key-value pair string to string to add to a map.
    /// </summary>
    public interface IPair
    {
        string Key();
        string Value();
        bool IsLazy();
    }

    /// <summary>
    /// A key-value pair to add to a map.
    /// </summary>
    public interface IPair<TValue>
    {
        string Key();
        TValue Value();
        bool IsLazy();
    }

    /// <summary>
    /// A key-value pair to add to a map.
    /// </summary>
    public interface IPair<TKey, TValue>
    {
        TKey Key();
        TValue Value();
        bool IsLazy();
    }
}
