

using System;
using System.Collections;
using Tonga.Scalar;

namespace Tonga.Enumerator
{
    /// <summary>
    /// Length of an <see cref="IEnumerator"/>
    /// </summary>
    public sealed class LengthOf : IScalar<Int32>
    {
        private readonly IScalar<int> length;

        /// <summary>
        /// Length of an <see cref="IEnumerator"/>
        /// </summary>
        /// <param name="items">enumerator to count</param>
        public LengthOf(IEnumerator items)
        {
            this.length = new ScalarOf<int>(() =>
            {
                int size = 0;
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
        public Int32 Value()
        {
            return this.length.Value();
        }
    }
}
