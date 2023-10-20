

using System;
using System.Collections;
using System.Collections.Generic;

using Tonga.Scalar;

namespace Tonga.List
{
    /// <summary>
    /// Ensures that <see cref="IList{T}" /> is not empty
    /// </summary>
    /// <typeparam name="T">Type of the list</typeparam>
    public sealed class NotEmpty<T> : ListEnvelope<T>
    {
        /// <summary>
        /// Ensures that <see cref="IList{T}" /> is not empty
        /// </summary>
        /// <param name="origin">List</param>
        public NotEmpty(IList<T> origin) : this(
            origin,
            new Exception("List is empty"))
        { }

        /// <summary>
        /// Ensures that <see cref="IList{T}" /> is not empty
        /// </summary>
        /// <param name="origin">List</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public NotEmpty(IList<T> origin, Exception ex) : base(() =>
            ListOf.Pipe(new Enumerable.NotEmpty<T>(origin, ex))
        )
        { }
    }

    public static class NotEmpty
    {
        /// <summary>
        /// Ensures that <see cref="IList{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">List</param>
        public static IList<T> Pipe<T>(IList<T> origin)
            => new NotEmpty<T>(origin);

        /// <summary>
        /// Ensures that <see cref="IList{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">List</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public static IList<T> Pipe<T>(IList<T> origin, Exception ex)
            => new NotEmpty<T>(origin, ex);
    }
}
