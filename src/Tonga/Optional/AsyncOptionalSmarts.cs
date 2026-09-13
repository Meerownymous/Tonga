using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Optional;

/// <summary>
/// Shorter ways to act on an <see cref="IAsyncOptional{TValue}"/>.
/// </summary>
public static class AsyncOptionalSmarts
{
    /// <summary>
    /// Act on the value when it is there.
    /// </summary>
    public static IAsyncOptional<TValue> IfHas<TValue>(
        this IAsyncOptional<TValue> origin, Func<TValue, ValueTask> then
    ) =>
        origin.IfHas((item, _) => then(item));

    /// <summary>
    /// Act on the value when it is there.
    /// </summary>
    public static IAsyncOptional<TValue> IfHas<TValue>(
        this IAsyncOptional<TValue> origin, Action<TValue> then
    ) =>
        origin.IfHas((item, _) =>
        {
            then(item);
            return default;
        });

    /// <summary>
    /// Act when no value is there.
    /// </summary>
    public static IAsyncOptional<TValue> IfNot<TValue>(
        this IAsyncOptional<TValue> origin, Func<ValueTask> then
    ) =>
        origin.IfNot(_ => then());

    /// <summary>
    /// Act when no value is there.
    /// </summary>
    public static IAsyncOptional<TValue> IfNot<TValue>(
        this IAsyncOptional<TValue> origin, Action then
    ) =>
        origin.IfNot(_ =>
        {
            then();
            return default;
        });

    /// <summary>
    /// A sync <see cref="IOptional{TValue}"/> as an async one.
    /// The sync optional is consulted when the result is awaited, not when it is built.
    /// </summary>
    public static IAsyncOptional<TValue> AsAsyncOptional<TValue>(this IOptional<TValue> origin) =>
        new AsyncOptSync<TValue>(origin);
}
