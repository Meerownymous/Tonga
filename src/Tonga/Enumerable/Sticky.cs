using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable which memoizes already visited items.
    /// </summary>
    public class Sticky<T>(Func<IEnumerator<T>> source) : IEnumerable<T>
    {
        private readonly Lock exclusive = new();
        private readonly Lazy<IEnumerator<T>> source = new(source);
        private bool ended;
        private readonly List<T> copy = [];

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(IEnumerable<T> source) : this(() => source)
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(Func<IEnumerable<T>> source) : this(() => source().GetEnumerator())
        { }

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public Sticky(IEnumerator<T> source) : this(() => source)
        { }

        public IEnumerator<T> GetEnumerator()
        {
            if (!ended)
            {
                foreach (var item in Partial())
                {
                    yield return item;
                }
            }
            else
            {
                foreach (var item in Full())
                {
                    yield return item;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private IEnumerable<T> Partial()
        {
            var i = 0;
            var enumerator = this.source.Value;
            while (true)
            {
                bool hasValue;
                lock (exclusive)
                {
                    if (i >= copy.Count)
                    {
                        hasValue = enumerator.MoveNext();
                        if (hasValue)
                            copy.Add(enumerator.Current);
                    }
                    else
                    {
                        hasValue = true;
                    }
                }

                if (hasValue)
                    yield return copy[i];
                else
                {
                    this.ended = true;
                    break;
                }
                i++;
            }
        }

        private IEnumerable<T> Full()
        {
            foreach(var item in this.copy)
            {
                yield return item;
            }
        }
    }

    public static class Sticky
    {
        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> _<T>(IEnumerable<T> source) => new(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> _<T>(Func<IEnumerable<T>> source) => new(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> _<T>(IEnumerator<T> source) => new(source);


        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static Sticky<T> _<T>(Func<IEnumerator<T>> source) => new(source);
    }

    public static class StickySmarts
    {
        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static IEnumerable<T> Sticky<T>(this IEnumerable<T> source) => new Sticky<T>(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static IEnumerable<T> Sticky<T>(this Func<IEnumerable<T>> source) => new Sticky<T>(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static IEnumerable<T> Sticky<T>(this IEnumerator<T> source) => new Sticky<T>(source);

        /// <summary>
        /// Enumerable which memoizes already visited items.
        /// </summary>
        public static IEnumerable<T> Sticky<T>(this Func<IEnumerator<T>> source) => new Sticky<T>(source);
    }
}

