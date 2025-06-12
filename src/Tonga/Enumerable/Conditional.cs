using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable sourced depending on a given condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Conditional<T>(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, Func<bool> condition) : IEnumerable<T>
    {
        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        public Conditional(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, bool condition) : this(
            whenMatching,
            whenNotMatching,
            () => condition
        )
        { }

        public IEnumerator<T> GetEnumerator()
        {
            if(condition())
                foreach(var item in whenMatching)
                    yield return item;
            else
                foreach (var item in whenNotMatching)
                    yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }

    public static partial class EnumerableSmarts
    {
        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable<T> AsConditional<T>(
            this IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, bool condition
        ) =>
            new Conditional<T>(whenMatching, whenNotMatching, condition);

        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static IEnumerable AsConditional<T>(
            this IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, Func<bool> condition
        ) =>
            new Conditional<T>(whenMatching, whenNotMatching, condition);
    }
}

