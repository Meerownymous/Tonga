


namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as uppercase.
    /// </summary>
    public sealed class Upper : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> as uppercase.
        /// </summary>
        /// <param name="text">text to uppercase</param>
        public Upper(IText text) : base(() => text.AsString().ToUpper(), false)
        { }
    }
}
