using System.Collections.Generic;
using Tonga;

/// <summary>
/// Input to be attached to a dictionary.
/// </summary>
public interface ILookupInput<Key, Value>
{
    /// <summary>
    /// Apply the input to a dictionary.
    /// </summary>
    /// <param name="dict"></param>
    /// <returns>Updated dictionary</returns>
    IMap<Key, Value> Merged(IMap<Key, Value> dict);
}
