

using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Enumerator;

/// <summary>
/// Length of an <see cref="IEnumerator"/>
/// </summary>
public sealed class StickyLength(IEnumerator items) : ScalarEnvelope<Int64>(
    new Lazy<long>(
        () =>
        {
            long size = 0;
            while (items.MoveNext())
                ++size;
            return size;
        }
    )
);
