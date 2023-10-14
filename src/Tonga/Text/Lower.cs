

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as lowercase.
    /// </summary>
    public sealed class Lower : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/>  as lowercase.
        /// </summary>
        /// <param name="text">text to lower</param>
        public Lower(IText text) : base(() => text.AsString().ToLower(), false)
        { }
    }
}
