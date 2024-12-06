using System;

namespace Tonga.Fact;

/// <summary>
/// Condition as fact.
/// </summary>
public sealed class AsFact(Func<bool> condition, Action ifTrue, Action ifFalse) : IFact
{
    private readonly Lazy<bool> result = new(condition.Invoke);

    public AsFact(bool condition) : this(() => condition, () =>{}, () =>{})
    { }

    public AsFact(Func<bool> condition) : this(condition, () =>{}, () =>{})
    { }

    public bool IsTrue() => result.Value;
    public bool IsFalse() => !result.Value;
    public IFact IfTrue(Action then) => new AsFact(condition, then, ifFalse);
    public IFact IfFalse(Action then) => new AsFact(condition, ifTrue, then);

    public void Check()
    {
        if (result.Value)
            ifTrue();
        else
            ifFalse();
    }
}

public static class AsFactSmarts
{
    public static IFact AsFact(this Func<bool> condition) => new AsFact(condition);
}
