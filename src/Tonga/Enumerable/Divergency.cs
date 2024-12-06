

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Divergency<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) : IEnumerable<T>
    {
        private readonly IEnumerable<T> result =
            new AsEnumerable<T>(() =>
                Produced(
                    Filtered._(match, a),
                    Filtered._(match, b)
                )
            );

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b) : this(
            a, b, _ => true
        )
        { }

        public IEnumerator<T> GetEnumerator() => result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<T> a, IEnumerable<T> b)
        {
            var all1 = new HashSet<T>(EqualityComparer<T>.Default);
            var all2 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin1 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin2 = new HashSet<T>(EqualityComparer<T>.Default);

            foreach (var element in a)
            {
                all1.Add(element);
                notin2.Add(element); // Add to `notin2` initially.
            }

            foreach (var element in b)
            {
                all2.Add(element);
                if (!all1.Contains(element))
                {
                    notin1.Add(element); // Only add to `notin1` if not in `a`.
                }
                else
                {
                    notin2.Remove(element); // Remove from `notin2` if found in `b`.
                }
            }

            foreach (var element in a)
            {
                if (all2.Contains(element))
                {
                    notin2.Remove(element); // Remove from `notin2` if found in `b`.
                }
            }

            foreach (var item in Joined._(notin1, notin2))
            {
                yield return item;
            }
        }
    }

    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    public static class Divergency
    {
        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) => new Divergency<T>(a, b, match);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> _<T>(IEnumerable<T> a, IEnumerable<T> b) => new Divergency<T>(a, b);
    }

    public static class DivergencySmarts
    {
        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<TItem> Divergency<TItem>(
            this TItem[] source,
            IEnumerable<TItem> other
        ) =>
            new Divergency<TItem>(source, other);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<TItem> Divergency<TItem>(
            this TItem[] source,
            IEnumerable<TItem> other,
            Func<TItem, bool> match
        ) =>
            new Divergency<TItem>(source, other, match);
    }
}
