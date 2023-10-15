

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
        private readonly IEnumerable<T> a;
        private readonly IEnumerable<T> b;
        private readonly Func<T, bool> match;
        private readonly Ternary<T> result;

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b, bool live = false) : this(
            a, b, item => true, live
        )
        { }

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public Divergency(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match, bool live = false)
        {
            this.a = new Filtered<T>(match, a, live: true);
            this.b = new Filtered<T>(match, b, live: true);
            this.match = match;
            this.result =
                Ternary.Pipe(
                    Sticky.New(() => this.Produced()),
                    EnumerableOf.Pipe(() => this.Produced()),
                    live
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

        private IEnumerator<T> Produced()
        {
            var all1 = new HashSet<T>(EqualityComparer<T>.Default);
            var all2 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin1 = new HashSet<T>(EqualityComparer<T>.Default);
            var notin2 = new HashSet<T>(EqualityComparer<T>.Default);

            foreach (var element in this.a)
            {
                if (this.match(element))
                    all1.Add(element);
            }

            foreach (var element in this.b)
            {
                if (this.match(element))
                    all2.Add(element);
            }

            foreach (var element in this.b)
            {
                if (this.match(element) && all1.Add(element))
                {
                    notin1.Add(element);
                }
            }

            foreach (var element in this.a)
            {
                if (this.match(element) && all2.Add(element))
                {
                    notin2.Add(element);
                }
            }

            foreach (var item in new Joined<T>(notin2, notin1))
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
        public static IEnumerable<T> New<T>(IEnumerable<T> a, IEnumerable<T> b, Func<T, bool> match) => new Divergency<T>(a, b, match);

        /// <summary>
        /// Items which do only exist in one enumerable.
        /// </summary>
        public static IEnumerable<T> New<T>(IEnumerable<T> a, IEnumerable<T> b) => new Divergency<T>(a, b);

    }
}
