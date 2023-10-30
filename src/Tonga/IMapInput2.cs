using System.Collections.Generic;
using Tonga;
using Tonga.Map;

/// <summary>
/// Input to be attached to a dictionary.
/// </summary>
public interface IMapInput<Key, Value>
{
    /// <summary>
    /// Apply the input to a dictionary.
    /// </summary>
    /// <param name="dict"></param>
    /// <returns>Updated dictionary</returns>
    IMap<Key, Value> Merged(IMap<Key, Value> dict);

    /// <summary>
    /// Access the raw IMapInput.
    /// </summary>
    /// <returns>Itself as IDictInput.</returns>
    IMapInput<Key, Value> Self();
}
