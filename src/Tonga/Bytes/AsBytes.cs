

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Tonga.Bytes;

/// <summary>
/// Bytes out of other objects.
/// </summary>
public sealed class AsBytes(Func<Byte[]> bytes) : IBytes
{
    /// <summary>
    /// Bytes out of a input.
    /// </summary>
    /// <param name="source">the input</param>
    public AsBytes(IConduit source) : this(new ConduitAsBytes(source))
    {
    }

    /// <summary>
    /// Bytes out of a input.
    /// </summary>
    /// <param name="source">the input</param>
    /// <param name="max">max buffer size</param>
    public AsBytes(IConduit source, int max) : this(new ConduitAsBytes(source, max))
    {
    }

    /// <summary>
    /// Bytes out of a StringBuilder.
    /// </summary>
    /// <param name="builder">stringbuilder</param>
    public AsBytes(StringBuilder builder) : this(builder, Encoding.UTF8)
    {
    }

    /// <summary>
    /// Bytes out of a StringBuilder.
    /// </summary>
    /// <param name="builder">stringbuilder</param>
    /// <param name="enc">encoding of stringbuilder</param>
    public AsBytes(StringBuilder builder, Encoding enc) : this(
        () => enc.GetBytes(builder.ToString()))
    {
    }

    /// <summary>
    /// Bytes out of a StringReader.
    /// </summary>
    /// <param name="rdr">stringreader</param>
    public AsBytes(StringReader rdr) : this(new ReaderAsBytes(rdr))
    {
    }

    /// <summary>
    /// Bytes out of a StreamReader.
    /// </summary>
    /// <param name="rdr">streamreader</param>
    public AsBytes(StreamReader rdr) : this(new ReaderAsBytes(rdr))
    {
    }

    /// <summary>
    /// Bytes out of a StreamReader.
    /// </summary>
    /// <param name="rdr">the reader</param>
    /// <param name="enc">encoding of the reader</param>
    /// <param name="max">max buffer size of the reader</param>
    public AsBytes(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(
        new ReaderAsBytes(rdr, enc, max)
    )
    {
    }

    /// <summary>
    /// Bytes out of a list of chars.
    /// </summary>
    /// <param name="chars">enumerable of bytes</param>
    public AsBytes(IEnumerable<char> chars) : this(chars, Encoding.UTF8)
    {
    }

    /// <summary>
    /// Bytes out of a list of chars.
    /// </summary>
    /// <param name="chars">enumerable of chars</param>
    /// <param name="enc">encoding of chars</param>
    public AsBytes(IEnumerable<char> chars, Encoding enc) : this(
        chars.ToArray(),
        enc
    )
    {
    }

    /// <summary>
    /// Bytes out of a char array.
    /// </summary>
    /// <param name="chars">array of chars</param>
    public AsBytes(params char[] chars) : this(chars, Encoding.UTF8)
    {
    }

    /// <summary>
    /// Bytes out of a char array.
    /// </summary>
    /// <param name="chars">an array of chars</param>
    /// <param name="encoding">encoding of chars</param>
    public AsBytes(char[] chars, Encoding encoding) : this(new String(chars), encoding)
    {
    }

    /// <summary>
    /// Bytes out of a string.
    /// </summary>
    /// <param name="source">a string</param>
    public AsBytes(String source) : this(source, Encoding.UTF8)
    {
    }

    /// <summary>
    /// Bytes out of a string.
    /// </summary>
    /// <param name="source">a string</param>
    public AsBytes(Func<String> source) : this(source, Encoding.UTF8)
    {
    }

    /// <summary>
    /// Bytes out of a string.
    /// </summary>
    /// <param name="source">a string</param>
    /// <param name="encoding">encoding of the string</param>
    public AsBytes(String source, Encoding encoding) : this(
        () => encoding.GetBytes(source))
    {
    }

    /// <summary>
    /// Bytes out of a string.
    /// </summary>
    /// <param name="source">a string</param>
    /// <param name="encoding">encoding of the string</param>
    public AsBytes(Func<String> source, Encoding encoding) : this(
        () => encoding.GetBytes(source()))
    {
    }

    /// <summary>
    /// Bytes out of Text.
    /// </summary>
    /// <param name="text">a text</param>
    public AsBytes(IText text) : this(text, Encoding.UTF8)
    {
    }

    /// <summary>
    /// Bytes out of Text.
    /// </summary>
    /// <param name="text">a text</param>
    /// <param name="encoding">encoding of the string</param>
    public AsBytes(IText text, Encoding encoding) : this(
        () => encoding.GetBytes(text.Str()))
    {
    }

    /// <summary>
    /// Bytes out of Exception object.
    /// </summary>
    /// <param name="error">exception to serialize</param>
    public AsBytes(Exception error) : this(
        () => Encoding.UTF8.GetBytes(error.ToString())
    )
    {
    }

    /// <summary>
    /// Bytes out of IBytes object.
    /// </summary>
    /// <param name="bytes">bytes</param>
    public AsBytes(IBytes bytes) : this(bytes.Bytes)
    {
    }

    /// <summary>
    /// Bytes out of byte array.
    /// </summary>
    /// <param name="bytes">byte aray</param>
    public AsBytes(params Byte[] bytes) : this(() => bytes)
    {
    }

    /// <summary>
    /// Bytes out of an int.
    /// </summary>
    /// <param name="number">an int</param>
    public AsBytes(int number) : this(() =>
        BitConverter.GetBytes(number)
    )
    {
    }

    /// <summary>
    /// Bytes out of a long.
    /// </summary>
    /// <param name="number">a long</param>
    public AsBytes(long number) : this(() =>
        BitConverter.GetBytes(number)
    )
    {
    }

    /// <summary>
    /// Bytes out of a float.
    /// </summary>
    /// <param name="number">a float</param>
    public AsBytes(float number) : this(() =>
        BitConverter.GetBytes(number)
    )
    {
    }

    /// <summary>
    /// Bytes out of a double.
    /// </summary>
    /// <param name="number">a double</param>
    public AsBytes(double number) : this(() =>
        BitConverter.GetBytes(number)
    )
    {
    }

    /// <summary>
    /// Bytes out of other objects.
    /// </summary>
    /// <param name="bytes">scalar of bytes</param>
    public AsBytes(IScalar<Byte[]> bytes) : this(bytes.Value)
    {
    }

    /// <summary>
    /// Get the content as byte array.
    /// </summary>
    /// <returns>content as byte array</returns>
    public byte[] Bytes() => bytes();

}


public static partial class BytesSmarts
{
    public static IBytes AsBytes(this IConduit origin) => new AsBytes(origin);
    public static IBytes AsBytes(this IConduit origin, int max) => new AsBytes(origin, max);
    public static IBytes AsBytes(this StringBuilder builder) => new AsBytes(builder);
    public static IBytes AsBytes(this StringBuilder builder, Encoding enc) =>
        new AsBytes(builder, enc);
    public static IBytes AsBytes(this StringReader rdr) => new AsBytes(rdr);
    public static IBytes AsBytes(this StreamReader rdr) => new AsBytes(rdr);
    public static IBytes AsBytes(this StreamReader rdr, Encoding enc, int max = 16 << 10) =>
        new AsBytes(rdr, enc, max);
    public static IBytes AsBytes(this IEnumerable<char> chars, Encoding enc) =>
        new AsBytes(chars, enc);

    public static IBytes AsBytes(this char[] chars) => new AsBytes(chars);
    public static IBytes AsBytes(this char[] chars, Encoding enc) => new AsBytes(chars, enc);
    public static IBytes AsBytes(this string source) => new AsBytes(source);
    public static IBytes AsBytes(this string source, Encoding enc) => new AsBytes(source, enc);
    public static IBytes AsBytes(this IText txt) => new AsBytes(txt);
    public static IBytes AsBytes(this IText txt, Encoding enc) => new AsBytes(txt, enc);
    public static IBytes AsBytes(this Exception ex) => new AsBytes(ex);
    public static IBytes AsBytes(this Byte[] bytes) => new AsBytes(bytes);
    public static IBytes AsBytes(this int number) => new AsBytes(number);
    public static IBytes AsBytes(this long number) => new AsBytes(number);
    public static IBytes AsBytes(this float number) => new AsBytes(number);
    public static IBytes AsBytes(this double number) => new AsBytes(number);
    public static IBytes AsBytes(this IScalar<byte[]> bytes) => new AsBytes(bytes);
    public static IBytes AsBytes(this Func<Byte[]> bytes) => new AsBytes(bytes);
}
