

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Tonga.Scalar;

#pragma warning disable MaxClassLength // Class length max

namespace Tonga.Bytes
{
    /// <summary>
    /// Bytes out of other objects.
    /// </summary>
    public sealed class AsBytes : IBytes
    {
        /// <summary>
        /// original
        /// </summary>
        private readonly Func<byte[]> origin;

        /// <summary>
        /// Bytes out of a input.
        /// </summary>
        /// <param name="input">the input</param>
        public AsBytes(IInput input) : this(new InputAsBytes(input))
        { }

        /// <summary>
        /// Bytes out of a input.
        /// </summary>
        /// <param name="input">the input</param>
        /// <param name="max">max buffer size</param>
        public AsBytes(IInput input, int max) : this(new InputAsBytes(input, max))
        { }

        /// <summary>
        /// Bytes out of a StringBuilder.
        /// </summary>
        /// <param name="builder">stringbuilder</param>
        public AsBytes(StringBuilder builder) : this(builder, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a StringBuilder.
        /// </summary>
        /// <param name="builder">stringbuilder</param>
        /// <param name="enc">encoding of stringbuilder</param>
        public AsBytes(StringBuilder builder, Encoding enc) : this(
            () => enc.GetBytes(builder.ToString()))
        { }

        /// <summary>
        /// Bytes out of a StringReader.
        /// </summary>
        /// <param name="rdr">stringreader</param>
        public AsBytes(StringReader rdr) : this(new ReaderAsBytes(rdr))
        { }

        /// <summary>
        /// Bytes out of a StreamReader.
        /// </summary>
        /// <param name="rdr">streamreader</param>
        public AsBytes(StreamReader rdr) : this(new ReaderAsBytes(rdr))
        { }

        /// <summary>
        /// Bytes out of a StreamReader.
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">max buffer size of the reader</param>
        public AsBytes(StreamReader rdr, Encoding enc, int max = 16 << 10) : this(
            new ReaderAsBytes(rdr, enc, max)
        )
        { }

        /// <summary>
        /// Bytes out of a list of chars.
        /// </summary>
        /// <param name="chars">enumerable of bytes</param>
        public AsBytes(IEnumerable<char> chars) : this(chars, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a list of chars.
        /// </summary>
        /// <param name="chars">enumerable of chars</param>
        /// <param name="enc">encoding of chars</param>
        public AsBytes(IEnumerable<char> chars, Encoding enc) : this(
            chars.ToArray(),
            enc
        )
        { }

        /// <summary>
        /// Bytes out of a char array.
        /// </summary>
        /// <param name="chars">array of chars</param>
        public AsBytes(params char[] chars) : this(chars, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a char array.
        /// </summary>
        /// <param name="chars">an array of chars</param>
        /// <param name="encoding">encoding of chars</param>
        public AsBytes(char[] chars, Encoding encoding) : this(new String(chars), encoding)
        { }

        /// <summary>
        /// Bytes out of a string.
        /// </summary>
        /// <param name="source">a string</param>
        public AsBytes(String source) : this(source, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of a string.
        /// </summary>
        /// <param name="source">a string</param>
        /// <param name="encoding">encoding of the string</param>
        public AsBytes(String source, Encoding encoding) : this(
            () => encoding.GetBytes(source))
        { }

        /// <summary>
        /// Bytes out of Text.
        /// </summary>
        /// <param name="text">a text</param>
        public AsBytes(IText text) : this(text, Encoding.UTF8)
        { }

        /// <summary>
        /// Bytes out of Text.
        /// </summary>
        /// <param name="text">a text</param>
        /// <param name="encoding">encoding of the string</param>
        public AsBytes(IText text, Encoding encoding) : this(
            () => encoding.GetBytes(text.AsString()))
        { }

        /// <summary>
        /// Bytes out of Exception object.
        /// </summary>
        /// <param name="error">exception to serialize</param>
        public AsBytes(Exception error) : this(
            () => Encoding.UTF8.GetBytes(error.ToString())
        )
        { }

        /// <summary>
        /// Bytes out of IBytes object.
        /// </summary>
        /// <param name="bytes">bytes</param>
        public AsBytes(IBytes bytes) : this(bytes.Bytes)
        { }

        /// <summary>
        /// Bytes out of byte array.
        /// </summary>
        /// <param name="bytes">byte aray</param>
        public AsBytes(params Byte[] bytes) : this(() => bytes)
        { }

        /// <summary>
        /// Bytes out of an int.
        /// </summary>
        /// <param name="number">an int</param>
        public AsBytes(int number) : this(() =>
            BitConverter.GetBytes(number)
        )
        { }

        /// <summary>
        /// Bytes out of a long.
        /// </summary>
        /// <param name="number">a long</param>
        public AsBytes(long number) : this(() =>
            BitConverter.GetBytes(number)
        )
        { }

        /// <summary>
        /// Bytes out of a float.
        /// </summary>
        /// <param name="number">a float</param>
        public AsBytes(float number) : this(() =>
            BitConverter.GetBytes(number)
        )
        { }

        /// <summary>
        /// Bytes out of a double.
        /// </summary>
        /// <param name="number">a double</param>
        public AsBytes(double number) : this(() =>
            BitConverter.GetBytes(number)
        )
        { }

        /// <summary>
        /// Bytes out of other objects.
        /// </summary>
        /// <param name="bytes">scalar of bytes</param>
        public AsBytes(IScalar<Byte[]> bytes) : this(() =>
            bytes.Value()
        )
        { }

        /// <summary>
        /// Bytes out of other objects.
        /// </summary>
        /// <param name="bytes">scalar of bytes</param>
        private AsBytes(Func<Byte[]> bytes)
        {
            this.origin = bytes;
        }

        /// <summary>
        /// Get the content as byte array.
        /// </summary>
        /// <returns>content as byte array</returns>
        public byte[] Bytes()
        {
            return this.origin();
        }

        /// <summary>
        /// Bytes out of a input.
        /// </summary>
        public static AsBytes _(IInput input) => new AsBytes(input);

        /// <summary>
        /// Bytes out of a input.
        /// </summary>
        public static AsBytes _(IInput input, int max) => new AsBytes(input, max);

        /// <summary>
        /// Bytes out of a StringBuilder.
        /// </summary>
        public static AsBytes _(StringBuilder builder) => new AsBytes(builder);

        /// <summary>
        /// Bytes out of a StringBuilder.
        /// </summary>
        public static AsBytes _(StringBuilder builder, Encoding enc) => new AsBytes(builder, enc);

        /// <summary>
        /// Bytes out of a StringReader.
        /// </summary>
        public static AsBytes _(StringReader rdr) => new AsBytes(rdr);

        /// <summary>
        /// Bytes out of a StreamReader.
        /// </summary>
        /// <param name="rdr">streamreader</param>
        public static AsBytes _(StreamReader rdr) => new AsBytes(rdr);

        /// <summary>
        /// Bytes out of a StreamReader.
        /// </summary>
        /// <param name="rdr">the reader</param>
        /// <param name="enc">encoding of the reader</param>
        /// <param name="max">max buffer size of the reader</param>
        public static AsBytes _(StreamReader rdr, Encoding enc, int max = 16 << 10) =>
            new AsBytes(rdr, enc, max);

        /// <summary>
        /// Bytes out of a list of chars.
        /// </summary>
        /// <param name="chars">enumerable of bytes</param>
        public static AsBytes _(IEnumerable<char> chars) => new AsBytes(chars);

        /// <summary>
        /// Bytes out of a list of chars.
        /// </summary>
        /// <param name="chars">enumerable of chars</param>
        /// <param name="enc">encoding of chars</param>
        public static AsBytes _(IEnumerable<char> chars, Encoding enc) => new AsBytes(chars, enc);

        /// <summary>
        /// Bytes out of a char array.
        /// </summary>
        /// <param name="chars">array of chars</param>
        public static AsBytes _(params char[] chars) => new AsBytes(chars);

        /// <summary>
        /// Bytes out of a char array.
        /// </summary>
        /// <param name="chars">an array of chars</param>
        /// <param name="encoding">encoding of chars</param>
        public static AsBytes _(char[] chars, Encoding encoding) => new AsBytes(chars, encoding);

        /// <summary>
        /// Bytes out of a string.
        /// </summary>
        /// <param name="source">a string</param>
        public static AsBytes _(String source) => new AsBytes(source);

        /// <summary>
        /// Bytes out of a string.
        /// </summary>
        public static AsBytes _(String source, Encoding encoding) => new AsBytes(source, encoding);

        /// <summary>
        /// Bytes out of Text.
        /// </summary>
        /// <param name="text">a text</param>
        public static AsBytes _(IText text) => new AsBytes(text);

        /// <summary>
        /// Bytes out of Text.
        /// </summary>
        /// <param name="text">a text</param>
        /// <param name="encoding">encoding of the string</param>
        public static AsBytes _(IText text, Encoding encoding) =>
            new AsBytes(text, encoding);

        /// <summary>
        /// Bytes out of Exception object.
        /// </summary>
        /// <param name="error">exception to serialize</param>
        public static AsBytes _(Exception error) =>
            new AsBytes(error);

        /// <summary>
        /// Bytes out of byte array.
        /// </summary>
        /// <param name="bytes">byte aray</param>
        public static AsBytes _(params Byte[] bytes) => new AsBytes(bytes);

        /// <summary>
        /// Bytes out of an int.
        /// </summary>
        /// <param name="number">an int</param>
        public static AsBytes _(int number) => new AsBytes(number);

        /// <summary>
        /// Bytes out of a long.
        /// </summary>
        /// <param name="number">a long</param>
        public static AsBytes _(long number) => new AsBytes(number);

        /// <summary>
        /// Bytes out of a float.
        /// </summary>
        /// <param name="number">a float</param>
        public static AsBytes _(float number) => new AsBytes(number);

        /// <summary>
        /// Bytes out of a double.
        /// </summary>
        /// <param name="number">a double</param>
        public AsBytes _(double number) => new AsBytes(number);

        /// <summary>
        /// Bytes out of other objects.
        /// </summary>
        /// <param name="bytes">scalar of bytes</param>
        public AsBytes _(IScalar<Byte[]> bytes) =>
            new AsBytes(bytes.Value);
    }
}
