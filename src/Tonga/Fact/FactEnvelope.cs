using System;

namespace Tonga.Fact;

/// <summary>
/// Envelope for facts.
/// </summary>
public abstract class FactEnvelope(Func<bool> origin) : IFact
{
    public FactEnvelope(IFact origin) : this(origin.IsTrue)
    { }

    public bool IsTrue() => origin();
    public bool IsFalse() => !origin();

    public IFact IfTrue(Action then) => new AsFact(origin, ifTrue: then, () => { });

    public IFact IfFalse(Action then) => new AsFact(origin, ifTrue: () => { }, ifFalse: then);

    public void Check() { }
}
