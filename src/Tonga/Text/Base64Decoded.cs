

using System;
using Tonga.Bytes;

namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
    /// </summary>
    public sealed class Base64Decoded(TextMorph text) : TextEnvelope(() =>
        new TextMorph(
            new Bytes.Base64Decoded(
                new AsBytes(text)
            ).Raw()
        ).Str()
    )
    {
        public Base64Decoded(IText txt) : this(new TextMorph(txt))
        { }
    }

    public static partial class TextSmarts
    {

    /// <summary>
        /// A <see cref="string"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="str">string to decode</param>
        public static Base64Decoded AsBase64Decoded(this String str) => new(str);

        /// <summary>
        /// A <see cref="IText"/> as Base64 decoded <see cref="IText"/>
        /// </summary>
        /// <param name="text">text to decode</param>
        public static Base64Decoded AsBase64Decoded(this IText text) => new(text);
    }
}
