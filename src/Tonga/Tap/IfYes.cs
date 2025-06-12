using System;
using Tonga.Fact;

namespace Tonga.Tap;

/// <summary>
/// Tap that does something if condition is true.
/// </summary>
public sealed class IfYes(Func<bool> condition, Action act) : TapEnvelope(
    new Conditional(condition, act, () => { })
)
{
    public IfYes(IFact condition, Action act) : this(condition.IsTrue, act)
    { }
}

public static partial class TapSmarts
{
    public static ITap IfYes(this Func<bool> condition, Action act) =>
        new IfYes(new AsFact(condition), act);
}
