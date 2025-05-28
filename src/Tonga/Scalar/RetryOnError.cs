using System;
using Tonga.Pipe;

namespace Tonga.Scalar
{
    /// <summary>
    /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class RetryOnError<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="origin">func to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public RetryOnError(Func<T> origin, int attempts = 3) : this(
            new AsScalar<T>(origin.Invoke),
            attempts
        )
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public RetryOnError(IScalar<T> scalar, int attempts = 3) : this(
            scalar, attempt => attempt >= attempts
        )
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public RetryOnError(IScalar<T> scalar, Func<Int32, Boolean> exit) : base(() =>
            new RetryOnError<Boolean, T>(
                _ => scalar.Value(),
                exit
            ).Yield(true)
        )
        { }
    }

    public static partial class ScalarSmarts
    {
        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">func to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public static IScalar<T> AsRetrying<T>(this Func<T> scalar, int attempts = 3)
            => new RetryOnError<T>(scalar, attempts);

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public static IScalar<T> AsRetrying<T>(this IScalar<T> scalar, int attempts = 3)
            => new RetryOnError<T>(scalar, attempts);

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry until the given condition <see cref="Func{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public static IScalar<T> AsRetrying<T>(IScalar<T> scalar, Func<Int32, Boolean> exit)
            => new RetryOnError<T>(scalar, exit);
    }
}
