namespace Tonga.Fact;

/// <summary>
/// Logical negative.
/// </summary>
public sealed class Not(IFact fact) : FactEnvelope(
    new AsFact(fact.IsFalse)
);
