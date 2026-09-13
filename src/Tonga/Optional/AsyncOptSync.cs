using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Optional;

/// <summary>
/// A sync <see cref="IOptional{TValue}"/> as an <see cref="IAsyncOptional{TValue}"/>.
/// The sync optional is consulted when this one is awaited, not when it is built.
/// </summary>
public sealed class AsyncOptSync<TValue> : IAsyncOptional<TValue>
{
    private readonly Func<IOptional<TValue>> origin;
    private readonly Func<TValue, CancellationToken, ValueTask> ifHas;
    private readonly Func<CancellationToken, ValueTask> ifNot;
    private readonly bool acts;

    /// <summary>
    /// A sync <see cref="IOptional{TValue}"/> as an <see cref="IAsyncOptional{TValue}"/>.
    /// </summary>
    public AsyncOptSync(IOptional<TValue> origin) : this(() => origin)
    { }

    /// <summary>
    /// A sync <see cref="IOptional{TValue}"/> as an <see cref="IAsyncOptional{TValue}"/>.
    /// </summary>
    public AsyncOptSync(Func<IOptional<TValue>> origin) : this(origin, (_, _) => default, _ => default, false)
    { }

    private AsyncOptSync(
        Func<IOptional<TValue>> origin,
        Func<TValue, CancellationToken, ValueTask> ifHas,
        Func<CancellationToken, ValueTask> ifNot,
        bool acts
    )
    {
        this.origin = origin;
        this.ifHas = ifHas;
        this.ifNot = ifNot;
        this.acts = acts;
    }

    public async ValueTask<bool> Has(CancellationToken cancellation = default)
    {
        var optional = this.origin();
        var has = optional.Has();

        if (has)
        {
            // asking for the value is what materialises it, so only ask when something wants it
            if (this.acts)
                await this.ifHas(optional.Value(), cancellation);
        }
        else
        {
            await this.ifNot(cancellation);
        }

        return has;
    }

    public IAsyncOptional<TValue> IfHas(Func<TValue, CancellationToken, ValueTask> then) =>
        new AsyncOptSync<TValue>(
            this.origin,
            async (item, cancellation) =>
            {
                await this.ifHas(item, cancellation);
                await then(item, cancellation);
            },
            this.ifNot,
            true
        );

    public IAsyncOptional<TValue> IfNot(Func<CancellationToken, ValueTask> then) =>
        new AsyncOptSync<TValue>(
            this.origin,
            this.ifHas,
            async cancellation =>
            {
                await this.ifNot(cancellation);
                await then(cancellation);
            },
            this.acts
        );

    public async ValueTask<TValue> Value(CancellationToken cancellation = default)
    {
        var optional = this.origin();
        if (!optional.Has())
        {
            await this.ifNot(cancellation);
            return optional.Value(); // throws, which is what an empty optional owes the caller
        }

        var value = optional.Value();
        await this.ifHas(value, cancellation);
        return value;
    }
}
