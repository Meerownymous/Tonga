

using System;
using Tonga.Bytes;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
    /// </summary>
    public sealed class Base64Text : TextEnvelope
    {
        /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Base64Text(String str, bool live = false) : this(new LiveText(str), live)
        { }

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        /// <param name="live">should the object build its value live, every time it is used?</param>
        public Base64Text(IText text, bool live = false) : base(() =>
            new LiveText(
                new Base64Bytes(
                    new BytesOf(text)
                )
            ).AsString(),
            live
        )
        { }
    }
}
