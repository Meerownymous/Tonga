using System;

namespace Tonga.Fact;

/// <summary>
/// Envelope for facts.
/// </summary>
public abstract class FactEnvelope(IFact origin) : IFact
{
    public bool IsTrue() => origin.IsTrue();
    public bool IsFalse() => origin.IsFalse();

    public IFact IfTrue(Action then) => origin.IfTrue(then);

    public IFact IfFalse(Action then) => origin.IfFalse(then);

    public void Check() => origin.Check();
}
