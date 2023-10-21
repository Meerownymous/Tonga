

using System;

namespace Tonga.Text
{
    /// <summary>
    /// An <see cref="IText"/> which is thread safe.
    /// </summary>
    public sealed class Synced : TextEnvelope
    {
        /// <summary>
        /// An <see cref="IText"/> which is thread safe.
        /// </summary>
        /// <param name="text">Text to be accessed thread safe</param>
        public Synced(IText text) : this(text, text)
        { }

        /// <summary>
        /// An <see cref="IText"/> which is thread safe.
        /// </summary>
        /// <param name="text">Text to be accessed thread safe</param>
        /// <param name="lck">Object to be locked to ensure thread safety</param>
        /// <returns></returns>
        public Synced(IText text, Object lck) : base(() =>
            {
                lock (lck)
                {
                    return text.AsString();
                }
            }
        )
        { }
    }
}
