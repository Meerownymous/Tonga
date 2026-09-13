using System.Threading;
using System.Threading.Tasks;

namespace Tonga;

/// <summary>
/// A fact which is checked asynchronously.
/// </summary>
public interface IAsyncFact
{
    /// <summary>
    /// Is fact true?
    /// </summary>
    ValueTask<bool> IsTrue(CancellationToken cancellation = default);

    /// <summary>
    /// Is fact false?
    /// </summary>
    ValueTask<bool> IsFalse(CancellationToken cancellation = default);
}
