
using System;

namespace Tonga.Optional;

/// <summary>
///     An optional that is filled with a vslue.
/// </summary>
public sealed class OptFull<TValue>(Func<TValue> value) : IOptional<TValue>
{
    public OptFull(TValue value) : this(() => value)
    { }

    private readonly Lazy<TValue> value = new(value);

    public bool Has() => true;

    public IOptional<TValue> IfHas(Action<TValue> action)
    {
        action(value.Value);
        return this;
    }

    public IOptional<TValue> IfNot(Action action) => this;

    public TValue Value() => value.Value;

}
