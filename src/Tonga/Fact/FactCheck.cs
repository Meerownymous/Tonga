using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Fact;

/// <summary>
/// True if all facts are true.
/// </summary>
public sealed class FactCheck(IEnumerable<IFact> facts) : FactEnvelope(
    new And(facts)
)
{
    /// <summary>
    /// True if all facts are true.
    /// </summary>
    public FactCheck(params IFact[] facts) : this(new AsEnumerable<IFact>(facts))
    { }
}

public static class FactCheckSmarts
{
    public static IFact FactCheck(this FactCheck factCheck, params IFact[] facts) =>
        new FactCheck(facts);
}
