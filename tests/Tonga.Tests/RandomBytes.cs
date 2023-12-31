


using System;
using System.Collections.Generic;
using Tonga.Enumerable;
using Tonga.List;
using Tonga.Scalar;

namespace Tonga.Tests
{
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
            AsList._(() =>
            {
                byte[] bytes = new byte[size];
                new Random().NextBytes(bytes);
                return bytes;
            })
        )
        { }
    }
}
