

using System.IO;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> that can't accept null.
    /// </summary>
    public sealed class NotNull : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/>  that can't accept null.
        /// </summary>
        /// <param name="text"></param>
        public NotNull(IText text) : base(() =>
            {
                if (text == null)
                {
                    throw new IOException("invalid text (null)");
                }
                return text.AsString();
            },
            false
        )
        { }
    }
}
