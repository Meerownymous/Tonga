using System;

namespace Tonga;

public interface IOptional<TValue>
{
    bool Has();
    IOptional<TValue> IfHas(Action<TValue> then);
    IOptional<TValue> IfNot(Action then);
    TValue Value();
}
