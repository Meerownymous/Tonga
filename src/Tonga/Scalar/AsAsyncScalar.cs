using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Scalar;

/// <summary>
/// Anything awaitable as a <see cref="IAsyncScalar{T}"/>.
/// Building it costs nothing, the work starts when the value is awaited.
/// </summary>
public sealed class AsAsyncScalar<T>(Func<CancellationToken, ValueTask<T>> origin) : IAsyncScalar<T>
{
    /// <summary>
    /// Anything awaitable as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public AsAsyncScalar(Func<ValueTask<T>> origin) : this(_ => origin())
    { }

    /// <summary>
    /// A ready value as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public AsAsyncScalar(T value) : this(_ => new ValueTask<T>(value))
    { }

    /// <summary>
    /// A sync <see cref="IScalar{T}"/> as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public AsAsyncScalar(IScalar<T> origin) : this(_ => new ValueTask<T>(origin.Value()))
    { }

    public ValueTask<T> Value(CancellationToken cancellation = default) => origin(cancellation);
}

public static class AsyncScalarSmarts
{
    /// <summary>
    /// Anything awaitable as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> AsAsyncScalar<T>(this Func<ValueTask<T>> origin) =>
        new AsAsyncScalar<T>(origin);

    /// <summary>
    /// Anything awaitable as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> AsAsyncScalar<T>(this Func<CancellationToken, ValueTask<T>> origin) =>
        new AsAsyncScalar<T>(origin);

    /// <summary>
    /// A sync <see cref="IScalar{T}"/> as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> AsAsyncScalar<T>(this IScalar<T> origin) =>
        new AsAsyncScalar<T>(origin);

    /// <summary>
    /// A <see cref="Task{T}"/> producing function as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> AsAsyncScalar<T>(this Func<Task<T>> origin) =>
        new AsAsyncScalar<T>(async () => await origin());

    /// <summary>
    /// A <see cref="Task{T}"/> producing function as a <see cref="IAsyncScalar{T}"/>.
    /// </summary>
    public static IAsyncScalar<T> AsAsyncScalar<T>(this Func<CancellationToken, Task<T>> origin) =>
        new AsAsyncScalar<T>(async cancellation => await origin(cancellation));
}
