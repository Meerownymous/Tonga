namespace Tonga.Pipe;

/// <summary>
/// Ternary pipe yielding based on a condition.
/// </summary>
public sealed class Conditional<In, Out>(
    System.Func<In, bool> condition,
    System.Func<In, Out> ifYes,
    System.Func<In, Out> ifNo
) : PipeEnvelope<In, Out>(
    new AsPipe<In, Out>(input => condition(input) ? ifYes(input) : ifNo(input))
)
{
    /// <summary>
    /// Ternary pipe yielding based on a condition.
    /// </summary>
    public Conditional(
        IFact condition,
        System.Func<In, Out> ifYes,
        System.Func<In, Out> ifNo
    ) : this(
        _ => condition.IsTrue(),
        ifYes,
        ifNo
    )
    { }

    /// <summary>
    /// Ternary pipe yielding based on a condition.
    /// </summary>
    public Conditional(
        IFact condition,
        IPipe<In, Out> ifYes,
        IPipe<In, Out> ifNo
    ) : this(
        _ => condition.IsTrue(),
        ifYes.Yield,
        ifNo.Yield
    )
    { }

    /// <summary>
    /// Ternary pipe yielding based on a condition.
    /// </summary>
    public Conditional(
        System.Func<In, bool> condition,
        IPipe<In, Out> ifYes,
        IPipe<In, Out> ifNo
    ) : this(
        condition,
        ifYes.Yield,
        ifNo.Yield
    )
    { }
}
