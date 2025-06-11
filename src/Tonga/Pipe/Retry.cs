

using System;

namespace Tonga.Pipe;

/// <summary>
/// Pipe that will retry if it fails.
/// </summary>
/// <typeparam name="In">type of input</typeparam>
/// <typeparam name="Out">type of output</typeparam>
public sealed class Retry<In, Out>(Func<In, Out> yield, Func<Int32, Boolean> exit) : IPipe<In, Out>
{
    /// <summary>
    /// Function that will retry if it fails.
    /// </summary>
    /// <param name="yield">func to retry</param>
    public Retry(Func<In, Out> yield) : this(yield, 3)
    { }

    /// <summary>
    /// Function that will retry if it fails.
    /// </summary>
    /// <param name="yield">func to retry</param>
    /// <param name="attempts">how often to retry</param>
    public Retry(Func<In, Out> yield, int attempts = 3) : this(
        yield, attempt => attempt >= attempts
    )
    { }

    /// <summary>
    /// Invoke the function and retrieve the output.
    /// </summary>
    /// <param name="input">the input argument</param>
    /// <returns>the output</returns>
    public Out Yield(In input)
    {
        int attempt = 0;
        Exception error = new ArgumentException(
            "An immediate exit, didn't have a chance to try at least once");

        while (!exit(attempt))
        {
            try
            {
                return yield(input);
            }
            catch (Exception ex)
            {
                error = ex;
            }
            ++attempt;
        }
        throw error;
    }
}

public static partial class PipeSmarts
{
    public static IPipe<In,Out> RetryOnError<In, Out>(Func<In, Out> yield, Func<Int32, Boolean> exit) =>
        new Retry<In, Out>(yield, exit);

    /// <summary>
    /// Function that will retry if it fails.
    /// </summary>
    /// <param name="yield">func to retry</param>
    public static IPipe<In, Out> RetryOnError<In, Out>(Func<In, Out> yield) =>
        new Retry<In, Out>(yield, 3);

    /// <summary>
    /// Function that will retry if it fails.
    /// </summary>
    /// <param name="yield">func to retry</param>
    /// <param name="attempts">how often to retry</param>
    public static IPipe<In, Out> RetryOnError<In, Out>(Func<In, Out> yield, int attempts = 3) =>
        new Retry<In, Out>(yield, attempts);
}
