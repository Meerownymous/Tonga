using System;

namespace Tonga.Tap;

/// <summary>
/// Tap that does something if condition is false.
/// </summary>
public sealed class IfNo(Func<bool> condition, Action act) : TapEnvelope(
    new Conditional(condition, () => { }, act)
)
{
    public IfNo(IFact condition, Action act) : this(condition.IsFalse, act)
    { }
}

public static partial class TapSmarts
{
    public static ITap IfNo(this IFact condition, Action act) =>
        new IfNo(condition, act);
}
