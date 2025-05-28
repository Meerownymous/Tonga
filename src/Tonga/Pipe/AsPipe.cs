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

/// <summary>
/// input to output
/// </summary>
public sealed class AsPipe<Out>(System.Func<Out> yield) : IPipe<Out>
{
    public AsPipe(Out fix) : this(() => fix)
    { }

    /// <summary>
    /// Retrieve piped Gem
    /// </summary>
    public Out Yield() => yield();
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
    public static IPipe<Out> AsPipe<Out>(this System.Func<Out> yield) =>
        new AsPipe<Out>(yield);
}
