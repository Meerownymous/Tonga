using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Optional;

/// <summary>
/// Envelope for async optionals.
/// </summary>
public abstract class AsyncOptEnvelope<TValue>(IAsyncOptional<TValue> origin) : IAsyncOptional<TValue>
{
    public ValueTask<bool> Has(CancellationToken cancellation = default) => origin.Has(cancellation);

    public IAsyncOptional<TValue> IfHas(Func<TValue, CancellationToken, ValueTask> then) => origin.IfHas(then);

    public IAsyncOptional<TValue> IfNot(Func<CancellationToken, ValueTask> then) => origin.IfNot(then);

    public ValueTask<TValue> Value(CancellationToken cancellation = default) => origin.Value(cancellation);
}
