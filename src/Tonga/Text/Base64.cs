

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
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Base64Decoded(String str) : this(AsText._(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Base64Decoded(IText text) : base(() =>
            AsText._(
                new Bytes.Base64Decoded(
                    new AsBytes(text)
                )
            ).AsString()
        )
        { }

        /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Base64Decoded _(String str) => new Base64Decoded(AsText._(str));

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Base64Decoded _(IText text) => new Base64Decoded(text);
    }
}
