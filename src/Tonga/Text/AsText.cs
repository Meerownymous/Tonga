

using System;
using System.Globalization;
using System.IO;
using System.Text;
using Tonga.Bytes;
using Tonga.IO;

#pragma warning disable MaxClassLength // Class length max
namespace Tonga.Text;

/// <summary>
/// A <see cref="IText"/> out of other objects.
/// </summary>
public sealed class AsText(Func<string> txt) : IText
{

    /// <summary>
    /// A <see cref="IText"/> out of a int.
    /// </summary>
    /// <param name="input">number</param>
    public AsText(int input) : this(() => input + "")
    { }

    /// <summary>
    /// A <see cref="IText"/> out of a long.
    /// </summary>
    /// <param name="input">number</param>
    public AsText(long input) : this(() => input + "")
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="double"/></param>
    public AsText(double input) : this(
        () => input.ToString(CultureInfo.InvariantCulture)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="double"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public AsText(double input, CultureInfo cultureInfo) : this(
        () => input.ToString(cultureInfo)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a float
    /// </summary>
    /// <param name="input">a <see cref="float"/></param>
    public AsText(float input) : this(
        () => input.ToString(CultureInfo.InvariantCulture)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="float"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public AsText(float input, CultureInfo cultureInfo) : this(
        () => input.ToString(cultureInfo)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a bool
    /// </summary>
    /// <param name="input">a <see cref="bool"/></param>
    public AsText(bool input) : this(
        () => input.ToString(CultureInfo.InvariantCulture)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a bool
    /// </summary>
    /// <param name="input">a <see cref="bool"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public AsText(bool input, CultureInfo cultureInfo) : this(
        () => input.ToString(cultureInfo)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">a file <see cref="Uri"/></param>
    public AsText(Uri uri) : this(new AsConduit(uri))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">a file <see cref="Uri"/></param>
    /// <param name="encoding">encoding of the data at the uri</param>
    public AsText(Uri uri, Encoding encoding) : this(new AsConduit(uri), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
    /// </summary>
    /// <param name="file"></param>
    public AsText(FileInfo file) : this(new AsConduit(file))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="encoding"></param>
    public AsText(FileInfo file, Encoding encoding) : this(new AsConduit(file), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="stream">a <see cref="Stream"/></param>
    public AsText(Stream stream) : this(new AsConduit(stream))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a <see cref="IConduit"/></param>
    public AsText(IConduit origin) : this(new AsBytes(origin))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a input</param>
    /// <param name="max">maximum buffer size</param>
    public AsText(IConduit origin, int max) : this(origin, max, Encoding.GetEncoding(0))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a input</param>
    /// <param name="encoding"><see cref="Encoding"/> of the input</param>
    public AsText(IConduit origin, Encoding encoding) : this(new AsBytes(origin), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a <see cref="IConduit"/></param>
    /// <param name="encoding">encoding of the <see cref="IConduit"/></param>
    /// <param name="max">maximum buffer size</param>
    public AsText(IConduit origin, int max, Encoding encoding) : this(new AsBytes(origin, max), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    public AsText(StringReader rdr) : this(new AsBytes(new AsConduit(rdr)))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public AsText(StringReader rdr, Encoding enc) : this(new AsBytes(rdr), enc)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    public AsText(StreamReader rdr) : this(new AsBytes(rdr))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public AsText(StreamReader rdr, Encoding cset) : this(new AsBytes(rdr, cset))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    /// <param name="max">maximum buffer size</param>
    public AsText(StreamReader rdr, Encoding cset, int max) : this(new AsBytes(rdr, cset, max))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">a <see cref="StringBuilder"/></param>
    public AsText(StringBuilder builder) : this(new AsBytes(builder))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">a <see cref="StringBuilder"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public AsText(StringBuilder builder, Encoding enc) : this(new AsBytes(builder, enc))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">a char array</param>
    public AsText(params char[] chars) : this(new AsBytes(chars))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">a char array</param>
    /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
    public AsText(char[] chars, Encoding encoding) : this(new AsBytes(chars, encoding))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Exception"/>.
    /// </summary>
    /// <param name="error"><see cref="Exception"/> to serialize</param>
    public AsText(Exception error) : this(new AsBytes(error))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="byte"/> array.
    /// </summary>
    /// <param name="bytes">a byte array</param>
    public AsText(params byte[] bytes) : this(new AsBytes(bytes))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    public AsText(IBytes bytes) : this(bytes, Encoding.GetEncoding(0))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
    public AsText(IBytes bytes, Encoding encoding) : this(
        () =>
        {
            var memoryStream = new MemoryStream(bytes.Bytes());
            return new StreamReader(memoryStream, encoding).ReadToEnd(); // removes the BOM from the Byte-Array
        })
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="string"/>.
    /// </summary>
    /// <param name="input">a string</param>
    public AsText(String input) : this(input, Encoding.GetEncoding(0))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="string"/>.
    /// </summary>
    /// <param name="input">a string</param>
    /// <param name="encoding"><see cref="Encoding"/> of the string</param>
    public AsText(String input, Encoding encoding) : this(
        () => encoding.GetString(encoding.GetBytes(input))
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
    /// </summary>
    /// <param name="fnc">func returning a string</param>
    public AsText(IFunc<string> fnc) : this(fnc.Invoke)
    {
    }

    public string Str() => txt();
}

public static partial class TextSmarts
{
    /// <summary>
    /// A <see cref="IText"/> out of a int.
    /// </summary>
    /// <param name="input">number</param>
    public static AsText AsText(this int input) => new(input);

    /// <summary>
    /// A <see cref="IText"/> out of a long.
    /// </summary>
    /// <param name="input">number</param>
    public static AsText AsText(this long input) => new(input);

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="double"/></param>
    public static AsText AsText(this double input) => new(input);

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="double"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public static AsText AsText(this double input, CultureInfo cultureInfo) => new(input, cultureInfo);

    /// <summary>
    /// A <see cref="IText"/> out of a float
    /// </summary>
    /// <param name="input">a <see cref="float"/></param>
    public static AsText AsText(this float input) => new(input);

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="float"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public static AsText AsText(this float input, CultureInfo cultureInfo) => new(input, cultureInfo);

    /// <summary>
    /// A <see cref="IText"/> out of a bool
    /// </summary>
    /// <param name="input">a <see cref="bool"/></param>
    public static AsText AsText(this bool input) => new(input);

    /// <summary>
    /// A <see cref="IText"/> out of a bool
    /// </summary>
    /// <param name="input">a <see cref="bool"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public static AsText AsText(this bool input, CultureInfo cultureInfo) => new(input, cultureInfo);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">a file <see cref="Uri"/></param>
    public static AsText AsText(this Uri uri) => new(uri);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">a file <see cref="Uri"/></param>
    /// <param name="encoding">encoding of the data at the uri</param>
    public static AsText AsText(this Uri uri, Encoding encoding) => new(uri, encoding);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
    /// </summary>
    public static AsText AsText(this FileInfo file) => new(file);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
    /// </summary>
    public static AsText AsText(this FileInfo file, Encoding encoding) => new(file, encoding);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="stream">a <see cref="Stream"/></param>
    public static AsText AsText(this Stream stream) => new(stream);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="conduit">a <see cref="IConduit"/></param>
    public static AsText AsText(this IConduit conduit) => new(conduit);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="conduit">a input</param>
    /// <param name="max">maximum buffer size</param>
    public static AsText AsText(this IConduit conduit, int max) => new(conduit, max);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="conduit">a input</param>
    /// <param name="encoding"><see cref="Encoding"/> of the input</param>
    public static AsText AsText(this IConduit conduit, Encoding encoding) => new(conduit, encoding);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="conduit">a <see cref="IConduit"/></param>
    /// <param name="encoding">encoding of the <see cref="IConduit"/></param>
    /// <param name="max">maximum buffer size</param>
    public static AsText AsText(this IConduit conduit, int max, Encoding encoding) => new(conduit, max, encoding);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    public static AsText AsText(this StringReader rdr) => new(rdr);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public static AsText AsText(this StringReader rdr, Encoding enc) => new(rdr, enc);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    public static AsText AsText(this StreamReader rdr) => new(rdr);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public static AsText AsText(this StreamReader rdr, Encoding cset) => new(rdr, cset);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    /// <param name="max">maximum buffer size</param>
    public static AsText AsText(this StreamReader rdr, Encoding cset, int max) => new(rdr, cset, max);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">a <see cref="StringBuilder"/></param>
    public static AsText AsText(this StringBuilder builder) => new(builder);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">a <see cref="StringBuilder"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public static AsText AsText(this StringBuilder builder, Encoding enc) => new(builder, enc);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">a char array</param>
    /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
    public static AsText AsText(this char[] chars, Encoding encoding) => new(chars, encoding);

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Exception"/>.
    /// </summary>
    /// <param name="error"><see cref="Exception"/> to serialize</param>
    public static AsText AsText(this Exception error) => new(error);

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    public static AsText AsText(this IBytes bytes) => new(bytes);

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
    public static AsText AsText(this IBytes bytes, Encoding encoding) => new(bytes, encoding);

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="string"/>.
    /// </summary>
    /// <param name="input">a string</param>
    public static AsText AsText(this String input) => new(input);

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="string"/>.
    /// </summary>
    /// <param name="input">a string</param>
    /// <param name="encoding"><see cref="Encoding"/> of the string</param>
    public static AsText AsText(this String input, Encoding encoding) => new(input, encoding);

    /// <summary>
    /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
    /// </summary>
    /// <param name="fnc">func returning a string</param>
    public static AsText AsText(this IFunc<string> fnc) => new(fnc);

    /// <summary>
    /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
    /// </summary>
    /// <param name="txt">scalar of a string</param>
    public static AsText AsText(this Func<string> txt) => new(txt);
}
