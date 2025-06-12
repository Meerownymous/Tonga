using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Enumerator;

/// <summary>
/// Length of an <see cref="IEnumerator"/>
/// </summary>
public sealed class Length(IEnumerator items) : ScalarEnvelope<Int32>(() =>
    {
        int size = 0;
        while (items.MoveNext())
        {
            ++size;
        }
        return size;
    }
);
