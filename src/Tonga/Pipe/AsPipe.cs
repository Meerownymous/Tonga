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

public static partial class PipeSmarts
{
    /// <summary>
    /// Swap an input to one output.
    /// </summary>
    public static IPipe<In, Out> AsPipe<In, Out>(this System.Func<In, Out> yield) =>
        new AsPipe<In, Out>(yield);

    /// <summary>
    /// Swap an input to one output.
    /// </summary>
    public static IPipe<In, In> AsPipe<In>(this System.Func<In, In> yield) =>
        new AsPipe<In, In>(yield);
}
