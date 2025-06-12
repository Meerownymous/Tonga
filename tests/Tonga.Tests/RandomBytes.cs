using System;
using Tonga.List;

namespace Tonga.Tests;

/// <summary>
/// List of N random bytes.
/// </summary>
public sealed class RandomBytes : ListEnvelope<byte>
{
    /// <summary>
    /// List of N random bytes.
    /// </summary>
    /// <param name="size">size of N</param>
    public RandomBytes(int size) : base(
        new AsList<byte>(() =>
        {
            byte[] bytes = new byte[size];
            new Random().NextBytes(bytes);
            return bytes;
        })
    )
    { }
}
