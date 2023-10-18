


using System;
using System.Collections.Generic;
using Tonga.List;
using Tonga.Scalar;

namespace Tonga.Tests
{
    /// <summary>
    /// List of N random bytes.
    /// </summary>
    public sealed class RandomBytes : ListEnvelopeOriginal<byte>
    {
        /// <summary>
        /// List of N random bytes.
        /// </summary>
        /// <param name="size">size of N</param>
        public RandomBytes(int size) : this(
            new ScalarOf<IList<byte>>(() =>
            {
                byte[] bytes = new byte[size];
                new Random().NextBytes(bytes);
                return new ListOf<byte>(bytes);
            }),
            false
            )
        { }

        /// <summary>
        /// List of N random bytes.
        /// </summary>
        /// <param name="lst">List</param>
        /// <param name="live"></param>
        public RandomBytes(IScalar<IList<byte>> lst, bool live) : base(() => lst.Value().GetEnumerator(), live)
        {
        }
    }
}
