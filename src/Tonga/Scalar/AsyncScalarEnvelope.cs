using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Scalar;

/// <summary>
/// Envelope for async scalars.
/// </summary>
public abstract class AsyncScalarEnvelope<T>(Func<CancellationToken, ValueTask<T>> origin) : IAsyncScalar<T>
{
    /// <summary>
    /// Envelope for async scalars.
    /// </summary>
    public AsyncScalarEnvelope(Func<ValueTask<T>> origin) : this(_ => origin())
    { }

    /// <summary>
    /// Envelope for async scalars.
    /// </summary>
    public AsyncScalarEnvelope(IAsyncScalar<T> origin) : this(origin.Value)
    { }

    /// <summary>
    /// Get the result.
    /// </summary>
    public ValueTask<T> Value(CancellationToken cancellation = default) => origin(cancellation);
}
