namespace Tonga;

/// <summary>
/// Swaps input to output.
/// </summary>
public interface IPipe<in TInput, out TOutput>
{
    /// <summary>
    /// Gem after traveling through pipe.
    /// </summary>
    TOutput Yield(TInput input);
}
