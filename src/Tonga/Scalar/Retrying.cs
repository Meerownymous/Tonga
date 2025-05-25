

using System;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary>
    /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Retrying<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="origin">func to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public Retrying(Func<T> origin, int attempts = 3) : this(
            new AsScalar<T>(origin.Invoke),
            attempts
        )
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public Retrying(IScalar<T> scalar, int attempts = 3) :
            this(scalar, new AsFunc<int, bool>(attempt => attempt >= attempts))
        { }

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public Retrying(IScalar<T> scalar, IFunc<Int32, Boolean> exit) : base(() =>
            new RetryFunc<Boolean, T>(
                new AsFunc<Boolean, T>(_ => scalar.Value()),
                exit
            ).Invoke(true)
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
            => new Retrying<T>(scalar, attempts);

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public static IScalar<T> AsRetrying<T>(this IScalar<T> scalar, int attempts = 3)
            => new Retrying<T>(scalar, attempts);

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public static IScalar<T> AsRetrying<T>(IScalar<T> scalar, IFunc<Int32, Boolean> exit)
            => new Retrying<T>(scalar, exit);
    }
}
