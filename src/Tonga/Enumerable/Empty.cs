

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable;

/// <summary>
/// Enumerable which is empty.
/// </summary>
public sealed class Empty<T> : IEnumerable<T>
{
    public IEnumerator<T> GetEnumerator()
    {
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}
