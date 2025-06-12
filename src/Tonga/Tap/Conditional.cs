namespace Tonga.Tap;

/// <summary>
/// Ternary pipe yielding based on a condition.
/// </summary>
public sealed class Conditional(
    System.Func<bool> condition,
    System.Action ifYes,
    System.Action ifNo
) : TapEnvelope(() =>
{
    if (condition())
        ifYes();
    else
        ifNo();
})
{
    /// <summary>
    /// Ternary pipe yielding based on a condition.
    /// </summary>
    public Conditional(
        IFact condition,
        System.Action ifYes,
        System.Action ifNo
    ) : this(
        condition.IsTrue,
        ifYes,
        ifNo
    )
    { }

    /// <summary>
    /// Ternary pipe yielding based on a condition.
    /// </summary>
    public Conditional(
        IFact condition,
        ITap ifYes,
        ITap ifNo
    ) : this(
        condition.IsTrue,
        ifYes.Trigger,
        ifNo.Trigger
    )
    { }

    /// <summary>
    /// Ternary pipe yielding based on a condition.
    /// </summary>
    public Conditional(
        System.Func<bool> condition,
        ITap ifYes,
        ITap ifNo
    ) : this(
        condition,
        ifYes.Trigger,
        ifNo.Trigger
    )
    { }
}
