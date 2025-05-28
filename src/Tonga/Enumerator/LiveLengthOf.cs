using System;
using System.Collections;

namespace Tonga.Enumerator
{
    /// <summary>
    /// Length of an <see cref="IEnumerator"/>
    /// </summary>
    public sealed class LiveLengthOf : IScalar<Int32>
    {
        private readonly IEnumerator items;

        /// <summary>
        /// Length of an <see cref="IEnumerator"/>
        /// </summary>
        /// <param name="items">enumerator to count</param>
        public LiveLengthOf(IEnumerator items)
        {
            this.items = items;
        }

        /// <summary>
        /// Get the length.
        /// </summary>
        /// <returns>the length</returns>
        public Int32 Value()
        {
            int size = 0;
            while (this.items.MoveNext())
            {
                ++size;
            }
            return size;
        }
    }
}
