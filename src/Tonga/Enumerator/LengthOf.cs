

using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Enumerator
{
    /// <summary>
    /// Length of an <see cref="IEnumerator"/>
    /// </summary>
    public sealed class LengthOf : IScalar<Int64>
    {
        private readonly IScalar<long> length;

        /// <summary>
        /// Length of an <see cref="IEnumerator"/>
        /// </summary>
        /// <param name="items">enumerator to count</param>
        public LengthOf(IEnumerator items)
        {
            this.length = new AsScalar<long>(() =>
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
        public Int64 Value()
        {
            return this.length.Value();
        }
    }
}
