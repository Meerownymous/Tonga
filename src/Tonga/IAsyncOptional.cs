using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga;

/// <summary>
/// A value which may or may not be there, materialised asynchronously.
/// Unlike <see cref="IOptional{TValue}"/>, nothing blocks: both the presence check
/// and the value are awaited.
/// <para>
/// <see cref="IfHas"/> and <see cref="IfNot"/> only build the chain. The actions they
/// take run when the optional is materialised, which is every time <see cref="Has"/>
/// or <see cref="Value"/> is awaited.
/// </para>
/// </summary>
public interface IAsyncOptional<TValue>
{
    /// <summary>
    /// Is a value there?
    /// </summary>
    ValueTask<bool> Has(CancellationToken cancellation = default);

    /// <summary>
    /// Act on the value when it is there.
    /// </summary>
    IAsyncOptional<TValue> IfHas(Func<TValue, CancellationToken, ValueTask> then);

    /// <summary>
    /// Act when no value is there.
    /// </summary>
    IAsyncOptional<TValue> IfNot(Func<CancellationToken, ValueTask> then);

    /// <summary>
    /// Access the value. Throws when there is none.
    /// </summary>
    ValueTask<TValue> Value(CancellationToken cancellation = default);
}
