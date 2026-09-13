using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Optional;

/// <summary>
/// An async optional which is filled with a value.
/// The value is materialised when it is awaited, not when the optional is built.
/// </summary>
public sealed class AsyncOptFull<TValue> : IAsyncOptional<TValue>
{
    private readonly Func<CancellationToken, ValueTask<TValue>> value;
    private readonly Func<TValue, CancellationToken, ValueTask> then;
    private readonly bool acts;

    /// <summary>
    /// An async optional which is filled with a value.
    /// </summary>
    public AsyncOptFull(Func<CancellationToken, ValueTask<TValue>> value) : this(value, (_, _) => default, false)
    { }

    /// <summary>
    /// An async optional which is filled with a value.
    /// </summary>
    public AsyncOptFull(Func<ValueTask<TValue>> value) : this(_ => value())
    { }

    /// <summary>
    /// An async optional which is filled with a ready value.
    /// </summary>
    public AsyncOptFull(TValue value) : this(_ => new ValueTask<TValue>(value))
    { }

    private AsyncOptFull(
        Func<CancellationToken, ValueTask<TValue>> value,
        Func<TValue, CancellationToken, ValueTask> then,
        bool acts
    )
    {
        this.value = value;
        this.then = then;
        this.acts = acts;
    }

    public async ValueTask<bool> Has(CancellationToken cancellation = default)
    {
        if (this.acts)
            await this.then(await this.value(cancellation), cancellation);
        return true;
    }

    public IAsyncOptional<TValue> IfHas(Func<TValue, CancellationToken, ValueTask> then) =>
        new AsyncOptFull<TValue>(
            this.value,
            async (item, cancellation) =>
            {
                await this.then(item, cancellation);
                await then(item, cancellation);
            },
            true
        );

    public IAsyncOptional<TValue> IfNot(Func<CancellationToken, ValueTask> then) => this;

    public async ValueTask<TValue> Value(CancellationToken cancellation = default)
    {
        var value = await this.value(cancellation);
        if (this.acts)
            await this.then(value, cancellation);
        return value;
    }
}
