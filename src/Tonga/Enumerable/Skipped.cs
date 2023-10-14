

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Skipped<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> enumerable;
        private readonly int skip;

        /// <summary>
        /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerable">enumerable to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public Skipped(IEnumerable<T> enumerable, int skip)
        {
            this.enumerable = enumerable;
            this.skip = skip;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var skipped = 0;
            foreach(var item in this.enumerable)
            {
                if (skipped < this.skip)
                {
                    skipped++;
                }
                else yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    /// <summary>
    /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
    /// </summary>
    public static class Skipped
    {
        /// <summary>
        /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerable">enumerable to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> enumerable, int skip) => new Skipped<T>(enumerable, skip);
    }
}
