using System;
using System.Threading;
using System.Threading.Tasks;

namespace Tonga.Fact;

/// <summary>
/// An awaited condition as a fact.
/// </summary>
public sealed class AsAsyncFact(Func<CancellationToken, ValueTask<bool>> condition) : IAsyncFact
{
    /// <summary>
    /// An awaited condition as a fact.
    /// </summary>
    public AsAsyncFact(Func<ValueTask<bool>> condition) : this(_ => condition())
    { }

    /// <summary>
    /// A ready condition as a fact.
    /// </summary>
    public AsAsyncFact(bool condition) : this(_ => new ValueTask<bool>(condition))
    { }

    /// <summary>
    /// A sync <see cref="IFact"/> as an async fact.
    /// </summary>
    public AsAsyncFact(IFact origin) : this(_ => new ValueTask<bool>(origin.IsTrue()))
    { }

    public ValueTask<bool> IsTrue(CancellationToken cancellation = default) => condition(cancellation);

    public async ValueTask<bool> IsFalse(CancellationToken cancellation = default) =>
        !await condition(cancellation);
}

public static partial class AsFactSmarts
{
    /// <summary>
    /// An awaited condition as a fact.
    /// </summary>
    public static IAsyncFact AsAsyncFact(this Func<ValueTask<bool>> condition) => new AsAsyncFact(condition);

    /// <summary>
    /// A sync <see cref="IFact"/> as an async fact.
    /// </summary>
    public static IAsyncFact AsAsyncFact(this IFact origin) => new AsAsyncFact(origin);
}
