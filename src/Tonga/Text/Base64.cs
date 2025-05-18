

using System;
using Tonga.Bytes;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
    /// </summary>
    public sealed class Base64Decoded : TextEnvelope
    {
        /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        public Base64Decoded(String str) : this(AsText._(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        public Base64Decoded(IText text) : base(
            AsText._(
                new Bytes.Base64Decoded(
                    new AsBytes(text)
                )
            )
        )
        { }

        /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        public static Base64Decoded _(String str) => new(AsText._(str));

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        public static Base64Decoded _(IText text) => new(text);
    }
}
