

using System;
using Tonga.Bytes;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
    /// </summary>
    public sealed class Base64 : TextEnvelope
    {
        /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Base64(String str) : this(AsText._(str))
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Base64(IText text) : base(() =>
            AsText._(
                new Base64Bytes(
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
        public static Base64 From(String str) => new Base64(AsText._(str));

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public static Base64 From(IText text) => new Base64(text);
    }
}
