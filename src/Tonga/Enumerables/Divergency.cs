

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Items which do only exist in one enumerable.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Divergency<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> result;

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b) : this(
            a, b, item => true
        )
        { }

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match)
        {
            this.result =
                AsEnumerable._(() =>
                    Produced(
                        Filtered.From(match, a),
                        Filtered.From(match, b)
                    )
                );
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.result.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        private static IEnumerator<T> Produced(IEnumerable<T> a, IEnumerable<T> b)
        {
            var all1 = new HashSet<T>(EqualityComparer<T>.Default);
            var all2 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin1 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin2 = new HashSet<T>(EqualityComparer<T>.Default);

            foreach (var element in a)
            {
                all1.Add(element);
            }

            foreach (var element in b)
            {
                all2.Add(element);
            }

            foreach (var element in b)
            {
                if (all1.Add(element))
                {
                    notin1.Add(element);
                }
            }

            foreach (var element in a)
            {
                if (all2.Add(element))
                {
                    notin2.Add(element);
                }
            }

            foreach (var item in Joined._(notin2, notin1))
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
        public static IEnumerable<T> From<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) => new Divergency<T>(a, b, match);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> From<T>(IEnumerable<T> a, IEnumerable<T> b) => new Divergency<T>(a, b);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> Sticky<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) =>
            new Divergency<T>(a, b, match);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> Sticky<T>(IEnumerable<T> a, IEnumerable<T> b) =>
            new Divergency<T>(a, b);

    }
}
