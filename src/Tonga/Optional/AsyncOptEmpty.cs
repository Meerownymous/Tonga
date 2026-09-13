using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Optional;

/// <summary>
/// An async optional which holds no value.
/// </summary>
public sealed class AsyncOptEmpty<TValue> : IAsyncOptional<TValue>
{
    private readonly Func<CancellationToken, ValueTask> then;
    private readonly bool acts;

    /// <summary>
    /// An async optional which holds no value.
    /// </summary>
    public AsyncOptEmpty() : this(_ => default, false)
    { }

    private AsyncOptEmpty(Func<CancellationToken, ValueTask> then, bool acts)
    {
        this.then = then;
        this.acts = acts;
    }

    public async ValueTask<bool> Has(CancellationToken cancellation = default)
    {
        if (this.acts)
            await this.then(cancellation);
        return false;
    }

    public IAsyncOptional<TValue> IfHas(Func<TValue, CancellationToken, ValueTask> then) => this;

    public IAsyncOptional<TValue> IfNot(Func<CancellationToken, ValueTask> then) =>
        new AsyncOptEmpty<TValue>(
            async cancellation =>
            {
                await this.then(cancellation);
                await then(cancellation);
            },
            true
        );

    public ValueTask<TValue> Value(CancellationToken cancellation = default) =>
        throw new InvalidOperationException("The Optional is empty.");
}
