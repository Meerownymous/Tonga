

using System;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> whose contents have been replaced by another text.
    /// </summary>
    public sealed class Replaced : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/>  whose contents have been replaced by another text.
        /// </summary>
        /// <param name="text">text to replace contents in</param>
        /// <param name="find">part to replace</param>
        /// <param name="replace">replacement to insert</param>
        public Replaced(IText text, String find, String replace) : base(() =>
            text.AsString().Replace(find, replace),
            false
        )
        { }
    }
}
