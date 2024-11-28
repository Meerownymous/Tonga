using System;

namespace Tonga.Optional;

public sealed class OptEnvelope<TValue>(IOptional<TValue> origin) : IOptional<TValue>
{
    public bool Has() => origin.Has();
    public IOptional<TValue> IfHas(Action<TValue> then) => origin.IfHas(then);
    public IOptional<TValue> IfNot(Action then) => origin.IfNot(then);
    public TValue Value() => origin.Value();
}
