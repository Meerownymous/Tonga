

using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Enumerator;

/// <summary>
/// Length of an <see cref="IEnumerator"/>
/// </summary>
public sealed class LengthOf : IScalar<Int64>
{
    private readonly Lazy<long> length;

    /// <summary>
    /// Length of an <see cref="IEnumerator"/>
    /// </summary>
    /// <param name="items">enumerator to count</param>
    public LengthOf(IEnumerator items)
    {
        this.length = new(() =>
        {
            long size = 0;
            while (items.MoveNext())
            {
                ++size;
            }
            return size;
        });
    }

    /// <summary>
    /// Get the length.
    /// </summary>
    /// <returns>the length</returns>
    public Int64 Value() => length.Value;
}
