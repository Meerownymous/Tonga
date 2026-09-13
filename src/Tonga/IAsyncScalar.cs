using System.Threading;
using System.Threading.Tasks;

namespace Tonga;

/// <summary>
/// A capsule for anything which is produced asynchronously.
/// Building the capsule costs nothing, the work starts when the value is awaited.
/// </summary>
public interface IAsyncScalar<TValue>
{
    /// <summary>
    /// Access the value.
    /// </summary>
    ValueTask<TValue> Value(CancellationToken cancellation = default);
}
