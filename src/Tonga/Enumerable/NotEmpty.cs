

using System;
using System.Collections;
using System.Collections.Generic;

using Tonga.Scalar;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
    /// </summary>
    /// <typeparam name="T">Type of the enumerable</typeparam>
    public sealed class NotEmpty<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> origin;
        private readonly Exception ex;
        private readonly Ternary<T> result;

        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        public NotEmpty(IEnumerable<T> origin, bool live = false) : this(
            origin,
            new Exception("Enumerable is empty"),
            live
        )
        { }

        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public NotEmpty(IEnumerable<T> origin, Exception ex, bool live = false)
        {
            this.origin = origin;
            this.ex = ex;
            this.result =
                Ternary.New(
                    Transit.Of(Produced),
                    Sticky.New(Produced),
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
            bool empty = true;
            foreach (var item in this.origin)
            {
                empty = false;
                yield return item;
            }
            if (empty)
            {
                throw this.ex;
            }
        }
    }

    /// <summary>
    /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
    /// </summary>
    public static class NotEmpty
    {
        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, bool live = false) => new NotEmpty<T>(origin, false);

        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public static IEnumerable<T> New<T>(IEnumerable<T> origin, Exception ex, bool live = false) => new NotEmpty<T>(origin, ex, false);
    }
}
