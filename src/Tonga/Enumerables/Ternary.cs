using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Enumerable sourced depending on a given condition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Ternary<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> whenMatching;
        private readonly IEnumerable<T> whenNotMatching;
        private readonly Func<bool> condition;

        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Ternary(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, bool condition) : this(
            whenMatching,
            whenNotMatching,
            () => condition
        )
        { }

        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public Ternary(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, Func<bool> condition)
        {
            this.whenMatching = whenMatching;
            this.whenNotMatching = whenNotMatching;
            this.condition = condition;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if(this.condition())
                foreach(var item in this.whenMatching)
                    yield return item;
            else
                foreach (var item in this.whenNotMatching)
                    yield return item;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }

    public static class Ternary
    {
        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static Ternary<T> From<T>(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, bool condition) =>
            new Ternary<T>(whenMatching, whenNotMatching, condition);

        /// <summary>
        /// Enumerable sourced depending on a given condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public static Ternary<T> From<T>(IEnumerable<T> whenMatching, IEnumerable<T> whenNotMatching, Func<bool> condition) =>
            new Ternary<T>(whenMatching, whenNotMatching, condition);
    }
}

