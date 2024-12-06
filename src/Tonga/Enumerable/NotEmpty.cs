

using System;
using System.Collections;
using System.Collections.Generic;

namespace Tonga.Enumerable
{
    /// <summary>
    /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
    /// </summary>
    /// <typeparam name="T">Type of the enumerable</typeparam>
    public sealed class NotEmpty<T>(IEnumerable<T> origin, Exception ex) : IEnumerable<T>
    {
        private readonly IEnumerable<T> result =
            new AsEnumerable<T>(() => Produced(origin, ex));

        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        public NotEmpty(IEnumerable<T> origin) : this(
            origin,
            new Exception("Enumerable is empty")
        )
        { }

        public IEnumerator<T> GetEnumerator() => this.result.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        private static IEnumerator<T> Produced(IEnumerable<T> origin, Exception ex)
        {
            bool empty = true;
            foreach (var item in origin)
            {
                empty = false;
                yield return item;
            }
            if (empty)
            {
                throw ex;
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
        public static IEnumerable<T> _<T>(IEnumerable<T> origin) => new NotEmpty<T>(origin);

        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public static IEnumerable<T> _<T>(IEnumerable<T> origin, Exception ex) => new NotEmpty<T>(origin, ex);
    }

    /// <summary>
    /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
    /// </summary>
    public static class NotEmptySmarts
    {
        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        public static IEnumerable<T> NotEmpty<T>(this IEnumerable<T> origin) => new NotEmpty<T>(origin);

        /// <summary>
        /// Ensures that <see cref="IEnumerable{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Enumerable</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public static IEnumerable<T> NotEmpty<T>(IEnumerable<T> origin, Exception ex) => new NotEmpty<T>(origin, ex);
    }
}
