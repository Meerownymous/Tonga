using System;

namespace Tonga.Optional;

/// <summary>
///     An optional that is filled with a vslue.
/// </summary>
public sealed class OptEmpty<TValue> : IOptional<TValue>
{
    public bool Has() => false;

    public IOptional<TValue> IfHas(Action<TValue> action) => this;
    public IOptional<TValue> IfNot(Action action)
    {
        action();
        return this;
    }

    public TValue Value() => throw new InvalidOperationException("The Optional is empty.");
}
