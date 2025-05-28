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

/// <summary>
/// Prepared pipe that yields from pre-fed input.
/// </summary>
public interface IPipe<out TOutput>
{
    /// <summary>
    /// Gem after traveling through pipe.
    /// </summary>
    TOutput Yield();
}
