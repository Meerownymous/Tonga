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
}
