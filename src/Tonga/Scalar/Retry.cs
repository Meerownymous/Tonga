

using System;
using Tonga.Func;

namespace Tonga.Scalar
{
    /// <summary>
    /// <see cref="ScalarEnvelope{T}"/> which will retry multiple times before throwing an exception.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Retry<T> : ScalarEnvelope<T>
    {
        /// <summary>
        /// <see cref="ScalarEnvelope{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">func to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public Retry(Func<T> scalar, int attempts = 3) : this(
            AsScalar._(scalar.Invoke),
            attempts
        )
        { }

        /// <summary>
        /// <see cref="ScalarEnvelope{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public Retry(IScalar<T> scalar, int attempts = 3) :
            this(scalar, new FuncOf<int, bool>(attempt => attempt >= attempts))
        { }

        /// <summary>
        /// <see cref="ScalarEnvelope{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public Retry(IScalar<T> scalar, IFunc<Int32, Boolean> exit) : base(() =>
            new RetryFunc<Boolean, T>(
                new FuncOf<Boolean, T>(input => scalar.Value()),
                exit
            ).Invoke(true)
        )
        { }
    }

    public static class Retry
    {
        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">func to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public static IScalar<T> _<T>(Func<T> scalar, int attempts = 3)
            => new Retry<T>(scalar, attempts);

        /// <summary>
        /// <see cref="IScalar{T}"/> which will retry multiple times before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="attempts">how often to retry</param>
        public static IScalar<T> _<T>(IScalar<T> scalar, int attempts = 3)
            => new Retry<T>(scalar, attempts);

        /// <summary>
        /// <see cref="ScalarEnvelope{T}"/> which will retry until the given condition <see cref="IFunc{In, Out}"/> matches before throwing an exception.
        /// </summary>
        /// <param name="scalar">scalar to retry when needed</param>
        /// <param name="exit"></param>
        public static IScalar<T> _<T>(IScalar<T> scalar, IFunc<Int32, Boolean> exit)
            => new Retry<T>(scalar, exit);
    }
}
