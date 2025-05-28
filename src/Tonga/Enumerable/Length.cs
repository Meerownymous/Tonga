using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Enumerable;

/// <summary>
/// Length of an <see cref="IEnumerable"/>.
/// </summary>
public sealed class Length : ScalarEnvelope<Int64>
{

    public Length(IEnumerable enumerable) : base(
        () => new Enumerator.LengthOf(enumerable.GetEnumerator()).Value()
    )
    { }
}


public static partial class EnumerableSmarts
{
    /// <summary>
    /// Length of an <see cref="IEnumerable"/>
    /// </summary>
    public static IScalar<Int64> Length(this IEnumerable items) => new Length(items);
}
