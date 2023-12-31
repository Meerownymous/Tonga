

using System;
using System.Collections.Generic;
using Tonga.Scalar;

namespace Tonga.Collection
{
    /// <summary>
    /// Ensures that <see cref="ICollection{T}" /> is not empty/>
    /// </summary>
    /// <typeparam name="T">Type of the collection</typeparam>
    public sealed class NotEmpty<T> : CollectionEnvelope<T>
    {
        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        public NotEmpty(ICollection<T> origin) : this(
            origin,
            new Exception("Collection is empty"))
        { }
        public static NotEmpty<T> New(ICollection<T> origin) => new NotEmpty<T>(origin);

        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        /// <param name="origin">Collection</param>
        /// <param name="ex">Execption to be thrown if empty</param>
        public NotEmpty(ICollection<T> origin, Exception ex) : this(
            origin, () => throw ex
        )
        { }

        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        public NotEmpty(ICollection<T> origin, Func<ICollection<T>> fallback) : base(
            AsCollection._(() =>
            {
                if (!origin.GetEnumerator().MoveNext())
                {
                    origin = fallback();
                }
                return origin;
            })
        )
        { }
    }

    /// <summary>
    /// Ensures that <see cref="ICollection{T}" /> is not empty/>
    /// </summary>
    public static class NotEmpty
    {
        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        public static ICollection<T> _<T>(ICollection<T> origin) => new NotEmpty<T>(origin);

        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        public static ICollection<T> _<T>(ICollection<T> origin, Exception ex) => new NotEmpty<T>(origin, ex);

        /// <summary>
        /// Ensures that <see cref="ICollection{T}" /> is not empty/>
        /// </summary>
        public static ICollection<T> _<T>(ICollection<T> origin, Func<ICollection<T>> fallback) => new NotEmpty<T>(origin, fallback);
    }
}
