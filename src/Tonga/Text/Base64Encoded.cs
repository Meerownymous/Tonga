

using System;
using Tonga.Bytes;

namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> as Base64 encoded <see cref="IText"/>
/// </summary>
public sealed class Base64Encoded(Func<string> origin) : TextEnvelope(
    new Bytes.Base64Encoded(
        new AsBytes(origin)
    ).AsText()
)
{
    /// <summary>
    /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
    /// </summary>
    /// <param name="str">string to encode</param>
    public Base64Encoded(String str) : this(() => str)
    { }

    /// <summary>
    /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
    /// </summary>
    public Base64Encoded(IText txt) : this(txt.Str)
    { }
}

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> as Base64 encoded <see cref="IText"/>
    /// </summary>
    public static IText AsBase64Encoded(this Func<string> origin) => new Base64Encoded(origin);

    /// <summary>
    /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
    /// </summary>
    public static IText AsBase64Encoded(this string origin) => new Base64Encoded(origin);

    /// <summary>
    /// A <see cref="string"/> as Base64-Encoded <see cref="IText"/>
    /// </summary>
    public static IText AsBase64Encoded(this IText origin) => new Base64Encoded(origin);
}
