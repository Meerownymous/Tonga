namespace Tonga;

/// <summary>
/// Pipe that lets input travel through and returns output.
/// </summary>
public interface IPipe<in TIn, out TOut>
{
    /// <summary>
    /// Yield output from input.
    /// </summary>
    TOut Yield(TIn input);
}
