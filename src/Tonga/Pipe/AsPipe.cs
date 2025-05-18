namespace Tonga.Pipe;

/// <summary>
/// input to output
/// </summary>
public sealed class AsPipe<In, Out>(System.Func<In,Out> forge) : IPipe<In, Out>
{
    public AsPipe(Out fix) : this(_ => fix)
    { }

    /// <summary>
    /// Retrieve piped Gem
    /// </summary>
    public Out Yield(In input) => forge(input);
}

public static class AsPipe
{
    /// <summary>
    /// Swap an input to one output.
    /// </summary>
    public static AsPipe<In, Out> _<In, Out>(System.Func<In, Out> forge) =>
        new(forge);
}
