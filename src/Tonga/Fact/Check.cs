using System.Collections.Generic;
using Tonga.Enumerable;

namespace Tonga.Fact;

/// <summary>
/// True if all facts are true.
/// </summary>
public sealed class Check(IEnumerable<IFact> facts) : FactEnvelope(
    new And(facts)
)
{
    /// <summary>
    /// True if all facts are true.
    /// </summary>
    public Check(params IFact[] facts) : this(new AsEnumerable<IFact>(facts))
    { }
}

public static class FactCheckSmarts
{
    public static IFact Check(this Check check, params IFact[] facts) =>
        new Check(facts);
}
