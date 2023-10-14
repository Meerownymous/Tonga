

using System;
using Tonga.Bytes;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 encoded <see cref="IText"/>
    /// </summary>
    public sealed class TextBase64 : TextEnvelope
    {
        /// <summary>
        /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to encode</param>
        public TextBase64(String str) : this(new LiveText(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to encode</param>
        public TextBase64(IText text) : base(
            new LiveText(
                new BytesBase64(
                    new BytesOf(text)
                )
            ),
            false
        )
        { }
    }
}
