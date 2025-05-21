

using System;
using System.IO;
using Tonga.Func;

namespace Tonga.IO
{
    /// <summary>
    /// Conduit which returns an alternate value if it fails.
    /// </summary>
    public sealed class ConduitWithFallback(IConduit origin, IFunc<Exception, IConduit> fbk) : IConduit, IDisposable
    {
        /// <summary>
        /// Conduit which returns an alternate value if it fails.
        /// </summary>
        public ConduitWithFallback(IConduit origin) : this(origin, new DeadConduit())
        { }

        /// <summary>
        /// Conduit which returns an alternate value if it fails.
        /// </summary>
        public ConduitWithFallback(IConduit origin, IConduit alt) : this(origin, _ => alt)
        { }

        /// <summary>
        /// Conduit which returns an alternate value from the given <see cref="Func{IOException, IInput}"/>if it fails.
        /// </summary>
        public ConduitWithFallback(IConduit origin, Func<Exception, IConduit> alt) : this(
            origin,
            new AsFunc<Exception, IConduit>(alt)
        )
        { }

        /// <summary>
        /// Get the stream.
        /// </summary>
        /// <returns>the stream</returns>
        public Stream Stream()
        {
            Stream stream;
            try
            {
                stream = origin.Stream();
            }
            catch (Exception ex)
            {
                stream = fbk.Invoke(ex).Stream();
            }
            return stream;
        }

        /// <summary>
        /// Clean up.
        /// </summary>
        public void Dispose() => (origin as IDisposable)?.Dispose();
    }
}
