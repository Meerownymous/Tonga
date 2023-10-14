

using System.Text;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> whose characters have been rotated.
    /// </summary>
    public sealed class Rotated : TextEnvelope
    {
        /// <summary>
        /// A <see cref="IText"/> whose characters have been rotated.
        /// </summary>
        /// <param name="text">text to rotate</param>
        /// <param name="shift">direction and amount of chars to rotate (minus means rotate left, plus means rotate right)</param>
        public Rotated(IText text, int shift) : base(() =>
            {
                var str = text.AsString();
                int length = str.Length;
                if (length != 0 && shift != 0 && shift % length != 0)
                {
                    var builder = new StringBuilder(length);
                    int offset = -(shift % length);
                    if (offset < 0)
                    {
                        offset = str.Length + offset;
                    }
                    str = builder.Append(
                        str.Substring(offset)
                    ).Append(
                        str.Substring(0, offset)
                    ).ToString();
                }
                return str;
            },
            false
        )
        { }
    }
}
