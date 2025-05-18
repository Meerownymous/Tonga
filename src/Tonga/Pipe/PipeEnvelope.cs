namespace Tonga.Pipe;

/// <summary>
/// Envelope for pipes.
/// </summary>
public abstract class PipeEnvelope<In, Out>(System.Func<In, Out> pipe) : IPipe<In, Out>
{
    public PipeEnvelope(IPipe<In, Out> pipe) : this(pipe.Yield){ }

    public Out Yield(In input) => pipe(input);
}
