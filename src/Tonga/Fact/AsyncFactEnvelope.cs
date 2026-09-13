using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Fact;

/// <summary>
/// Envelope for async facts.
/// </summary>
public abstract class AsyncFactEnvelope(Func<CancellationToken, ValueTask<bool>> origin) : IAsyncFact
{
    /// <summary>
    /// Envelope for async facts.
    /// </summary>
    public AsyncFactEnvelope(Func<ValueTask<bool>> origin) : this(_ => origin())
    { }

    /// <summary>
    /// Envelope for async facts.
    /// </summary>
    public AsyncFactEnvelope(IAsyncFact origin) : this(origin.IsTrue)
    { }

    public ValueTask<bool> IsTrue(CancellationToken cancellation = default) => origin(cancellation);

    public async ValueTask<bool> IsFalse(CancellationToken cancellation = default) =>
        !await origin(cancellation);
}
