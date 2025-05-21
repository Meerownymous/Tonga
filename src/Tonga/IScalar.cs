

namespace Tonga;

/// <summary>
/// A capsule for anything.
/// </summary>
public interface IScalar<out Val>
{
    /// <summary>
    /// Access the value.
    /// </summary>
    Val Value();
}
