

using System;
using Tonga.Bytes;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 encoded <see cref="IText"/>
    /// </summary>
    public sealed class TextAsBase64 : TextEnvelope
    {
        /// <summary>
        /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to encode</param>
        public TextAsBase64(String str) : this(AsText._(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64-Encoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to encode</param>
        public TextAsBase64(IText text) : base(
            AsText._(
                new Base64Encoded(
                    new AsBytes(text)
                )
            )
        )
        { }
    }
}
