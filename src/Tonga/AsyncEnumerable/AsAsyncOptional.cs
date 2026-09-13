using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.AsyncEnumerable;

/// <summary>
/// The first item of an <see cref="IAsyncEnumerable{T}"/> as an <see cref="IAsyncOptional{TValue}"/>.
/// Nothing is read until the optional is awaited, and then no more than one matching item.
/// </summary>
public sealed class AsAsyncOptional<T> : IAsyncOptional<T>
{
    private readonly IAsyncEnumerable<T> source;
    private readonly Func<T, bool> condition;
    private readonly Func<T, CancellationToken, ValueTask> ifHas;
    private readonly Func<CancellationToken, ValueTask> ifNot;

    /// <summary>
    /// The first item of an <see cref="IAsyncEnumerable{T}"/> as an <see cref="IAsyncOptional{TValue}"/>.
    /// </summary>
    public AsAsyncOptional(IAsyncEnumerable<T> source) : this(source, _ => true)
    { }

    /// <summary>
    /// The first matching item of an <see cref="IAsyncEnumerable{T}"/> as an <see cref="IAsyncOptional{TValue}"/>.
    /// </summary>
    public AsAsyncOptional(IAsyncEnumerable<T> source, Func<T, bool> condition) : this(
        source, condition, (_, _) => default, _ => default
    )
    { }

    private AsAsyncOptional(
        IAsyncEnumerable<T> source,
        Func<T, bool> condition,
        Func<T, CancellationToken, ValueTask> ifHas,
        Func<CancellationToken, ValueTask> ifNot
    )
    {
        this.source = source;
        this.condition = condition;
        this.ifHas = ifHas;
        this.ifNot = ifNot;
    }

    public async ValueTask<bool> Has(CancellationToken cancellation = default)
    {
        var (has, value) = await First(cancellation);
        if (has)
            await this.ifHas(value, cancellation);
        else
            await this.ifNot(cancellation);
        return has;
    }

    public IAsyncOptional<T> IfHas(Func<T, CancellationToken, ValueTask> then) =>
        new AsAsyncOptional<T>(
            this.source,
            this.condition,
            async (item, cancellation) =>
            {
                await this.ifHas(item, cancellation);
                await then(item, cancellation);
            },
            this.ifNot
        );

    public IAsyncOptional<T> IfNot(Func<CancellationToken, ValueTask> then) =>
        new AsAsyncOptional<T>(
            this.source,
            this.condition,
            this.ifHas,
            async cancellation =>
            {
                await this.ifNot(cancellation);
                await then(cancellation);
            }
        );

    public async ValueTask<T> Value(CancellationToken cancellation = default)
    {
        var (has, value) = await First(cancellation);
        if (!has)
        {
            await this.ifNot(cancellation);
            throw new InvalidOperationException("The Optional is empty.");
        }

        await this.ifHas(value, cancellation);
        return value;
    }

    private async ValueTask<(bool has, T value)> First(CancellationToken cancellation)
    {
        var enumerator = this.source.AsFiltered(this.condition).GetAsyncEnumerator(cancellation);
        try
        {
            return await enumerator.MoveNextAsync()
                ? (true, enumerator.Current)
                : (false, default);
        }
        finally
        {
            await enumerator.DisposeAsync();
        }
    }
}

public static partial class AsyncEnumerableSmarts
{
    /// <summary>
    /// The first item of an <see cref="IAsyncEnumerable{T}"/> as an <see cref="IAsyncOptional{TValue}"/>.
    /// </summary>
    public static IAsyncOptional<T> AsAsyncOptional<T>(this IAsyncEnumerable<T> source) =>
        new AsAsyncOptional<T>(source);

    /// <summary>
    /// The first matching item of an <see cref="IAsyncEnumerable{T}"/> as an <see cref="IAsyncOptional{TValue}"/>.
    /// </summary>
    public static IAsyncOptional<T> AsAsyncOptional<T>(this IAsyncEnumerable<T> source, Func<T, bool> condition) =>
        new AsAsyncOptional<T>(source, condition);
}
