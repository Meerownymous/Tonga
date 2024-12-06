

using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Skipped<T>(IEnumerable<T> enumerable, int skip) : IEnumerable<T>
    {
        public IEnumerator<T> GetEnumerator()
        {
            var skipped = 0;
            foreach(var item in enumerable)
            {
                if (skipped < skip)
                {
                    skipped++;
                }
                else yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
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
        public static IEnumerable<T> _<T>(IEnumerable<T> enumerable, int skip) => new Skipped<T>(enumerable, skip);
    }

    /// <summary>
    /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
    /// </summary>
    public static class SkippedSmarts
    {
        /// <summary>
        /// A <see cref="IEnumerable{Tests}"/> which skips a given count of items.
        /// </summary>
        /// <param name="enumerable">enumerable to skip items in</param>
        /// <param name="skip">how many to skip</param>
        public static IEnumerable<T> Skipped<T>(this IEnumerable<T> enumerable, int skip) => new Skipped<T>(enumerable, skip);
    }
}
