

using System;
using System.Globalization;
using System.IO;
using System.Text;
using Tonga.Bytes;
using Tonga.IO;

#pragma warning disable MaxClassLength // Class length max
namespace Tonga.Text
{
    /// <summary>
    /// A <see cref="IText"/> out of other objects.
    /// </summary>
    public sealed class TextOf : IText
    {
        private readonly Func<string> origin;

        /// <summary>
        /// A <see cref="IText"/> out of a int.
        /// </summary>
        /// <param name="input">number</param>
        public TextOf(int input) : this(() => input + "")
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a long.
        /// </summary>
        /// <param name="input">number</param>
        public TextOf(long input) : this(() => input + "")
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        public TextOf(double input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public TextOf(double input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a float
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        public TextOf(float input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public TextOf(float input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        public TextOf(bool input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public TextOf(bool input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        public TextOf(Uri uri) : this(new InputOf(uri))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        /// <param name="encoding">encoding of the data at the uri</param>
        public TextOf(Uri uri, Encoding encoding) : this(new InputOf(uri), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        public TextOf(FileInfo file) : this(new InputOf(file))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        public TextOf(FileInfo file, Encoding encoding) : this(new InputOf(file), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="stream">a <see cref="Stream"/></param>
        public TextOf(Stream stream) : this(new InputOf(stream))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        public TextOf(IInput input) : this(new BytesOf(input))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="max">maximum buffer size</param>
        public TextOf(IInput input, int max) : this(input, max, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="encoding"><see cref="Encoding"/> of the input</param>
        public TextOf(IInput input, Encoding encoding) : this(new BytesOf(input), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        /// <param name="encoding">encoding of the <see cref="IInput"/></param>
        /// <param name="max">maximum buffer size</param>
        public TextOf(IInput input, int max, Encoding encoding) : this(new BytesOf(input, max), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public TextOf(StringReader rdr) : this(new BytesOf(new InputOf(rdr)))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public TextOf(StringReader rdr, Encoding enc) : this(new BytesOf(rdr), enc)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public TextOf(StreamReader rdr) : this(new BytesOf(rdr))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public TextOf(StreamReader rdr, Encoding cset) : this(new BytesOf(rdr, cset))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        /// <param name="max">maximum buffer size</param>
        public TextOf(StreamReader rdr, Encoding cset, int max) : this(new BytesOf(rdr, cset, max))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        public TextOf(StringBuilder builder) : this(new BytesOf(builder))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public TextOf(StringBuilder builder, Encoding enc) : this(new BytesOf(builder, enc))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        public TextOf(params char[] chars) : this(new BytesOf(chars))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
        public TextOf(char[] chars, Encoding encoding) : this(new BytesOf(chars, encoding))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="error"><see cref="Exception"/> to serialize</param>
        public TextOf(Exception error) : this(new BytesOf(error))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">a byte array</param>
        public TextOf(params byte[] bytes) : this(new BytesOf(bytes))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        public TextOf(IBytes bytes) : this(bytes, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
        public TextOf(IBytes bytes, Encoding encoding) : this(
            () =>
            {
                var memoryStream = new MemoryStream(bytes.AsBytes());
                return new StreamReader(memoryStream, encoding).ReadToEnd(); // removes the BOM from the Byte-Array
            })
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        public TextOf(String input) : this(input, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        /// <param name="encoding"><see cref="Encoding"/> of the string</param>
        public TextOf(String input, Encoding encoding) : this(
            () => encoding.GetString(encoding.GetBytes(input)))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
        /// </summary>
        /// <param name="fnc">func returning a string</param>
        public TextOf(IFunc<string> fnc) : this(() => fnc.Invoke())
        { }

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="scalar">scalar of a string</param>
        public TextOf(IScalar<String> scalar) : this(() => scalar.Value())
        { }

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="txt">scalar of a string</param>
        public TextOf(Func<String> txt)
        {
            this.origin = txt;
        }

        public string AsString()
        {
            return this.origin();
        }

        /// <summary>
        /// A <see cref="IText"/> out of a int.
        /// </summary>
        /// <param name="input">number</param>
        public static TextOf Pipe(int input) => new TextOf(input);

        /// <summary>
        /// A <see cref="IText"/> out of a long.
        /// </summary>
        /// <param name="input">number</param>
        public static TextOf Pipe(long input) => new TextOf(input);

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        public static TextOf Pipe(double input) => new TextOf(input);

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static TextOf Pipe(double input, CultureInfo cultureInfo) => new TextOf(input, cultureInfo);

        /// <summary>
        /// A <see cref="IText"/> out of a float
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        public static TextOf Pipe(float input) => new TextOf(input);

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static TextOf Pipe(float input, CultureInfo cultureInfo) => new TextOf(input, cultureInfo);

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        public static TextOf Pipe(bool input) => new TextOf(input);

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static TextOf Pipe(bool input, CultureInfo cultureInfo) => new TextOf(input, cultureInfo);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        public static TextOf Pipe(Uri uri) => new TextOf(uri);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        /// <param name="encoding">encoding of the data at the uri</param>
        public static TextOf Pipe(Uri uri, Encoding encoding) => new TextOf(uri, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        public static TextOf Pipe(FileInfo file) => new TextOf(file);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        public static TextOf Pipe(FileInfo file, Encoding encoding) => new TextOf(file, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="stream">a <see cref="Stream"/></param>
        public static TextOf Pipe(Stream stream) => new TextOf(stream);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        public static TextOf Pipe(IInput input) => new TextOf(input);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="max">maximum buffer size</param>
        public static TextOf Pipe(IInput input, int max) => new TextOf(input, max);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="encoding"><see cref="Encoding"/> of the input</param>
        public static TextOf Pipe(IInput input, Encoding encoding) => new TextOf(input, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        /// <param name="encoding">encoding of the <see cref="IInput"/></param>
        /// <param name="max">maximum buffer size</param>
        public static TextOf Pipe(IInput input, int max, Encoding encoding) => new TextOf(input, max, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public static TextOf Pipe(StringReader rdr) => new TextOf(rdr);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static TextOf Pipe(StringReader rdr, Encoding enc) => new TextOf(rdr, enc);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public static TextOf Pipe(StreamReader rdr) => new TextOf(rdr);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static TextOf Pipe(StreamReader rdr, Encoding cset) => new TextOf(rdr, cset);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        /// <param name="max">maximum buffer size</param>
        public static TextOf Pipe(StreamReader rdr, Encoding cset, int max) => new TextOf(rdr, cset, max);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        public static TextOf Pipe(StringBuilder builder) => new TextOf(builder);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static TextOf Pipe(StringBuilder builder, Encoding enc) => new TextOf(builder, enc);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        public static TextOf Pipe(params char[] chars) => new TextOf(chars);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
        public static TextOf Pipe(char[] chars, Encoding encoding) => new TextOf(chars, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="error"><see cref="Exception"/> to serialize</param>
        public static TextOf Pipe(Exception error) => new TextOf(error);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">a byte array</param>
        public static TextOf Pipe(params byte[] bytes) => new TextOf(bytes);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        public static TextOf Pipe(IBytes bytes) => new TextOf(bytes);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
        public static TextOf Pipe(IBytes bytes, Encoding encoding) => new TextOf(bytes, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        public static TextOf Pipe(String input) => new TextOf(input);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        /// <param name="encoding"><see cref="Encoding"/> of the string</param>
        public static TextOf Pipe(String input, Encoding encoding) => new TextOf(input, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
        /// </summary>
        /// <param name="fnc">func returning a string</param>
        public static TextOf Pipe(IFunc<string> fnc) => new TextOf(fnc);

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="txt">scalar of a string</param>
        public static TextOf Pipe(Func<String> txt) => new TextOf(txt);

        /// <summary>
        /// A <see cref="IText"/> out of a int.
        /// </summary>
        /// <param name="input">number</param>
        public static IText Sticky(int input) =>
            Text.Sticky.New(new TextOf(input));

        /// <summary>
        /// A <see cref="IText"/> out of a long.
        /// </summary>
        /// <param name="input">number</param>
        public static IText Sticky(long input) =>
            Text.Sticky.New(new TextOf(input));

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        public static IText Sticky(double input) =>
            Text.Sticky.New(new TextOf(input));

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static IText Sticky(double input, CultureInfo cultureInfo) =>
            Text.Sticky.New(new TextOf(input, cultureInfo));

        /// <summary>
        /// A <see cref="IText"/> out of a float
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        public static IText Sticky(float input) =>
            Text.Sticky.New(new TextOf(input));

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static IText Sticky(float input, CultureInfo cultureInfo) =>
            Text.Sticky.New(new TextOf(input, cultureInfo));

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        public static IText Sticky(bool input) =>
            Text.Sticky.New(new TextOf(input));

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static IText Sticky(bool input, CultureInfo cultureInfo) =>
            Text.Sticky.New(new TextOf(input, cultureInfo));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        public static IText Sticky(Uri uri) =>
            Text.Sticky.New(new TextOf(uri));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        /// <param name="encoding">encoding of the data at the uri</param>
        public static IText Sticky(Uri uri, Encoding encoding) =>
            Text.Sticky.New(new TextOf(uri, encoding));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        public static IText Sticky(FileInfo file) =>
            Text.Sticky.New(new TextOf(file));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        public static IText Sticky(FileInfo file, Encoding encoding) =>
            Text.Sticky.New(new TextOf(file, encoding));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="stream">a <see cref="Stream"/></param>
        public static IText Sticky(Stream stream) =>
            Text.Sticky.New(new TextOf(stream));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        public static IText Sticky(IInput input) =>
            Text.Sticky.New(new TextOf(input));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="max">maximum buffer size</param>
        public static IText Sticky(IInput input, int max) =>
            Text.Sticky.New(new TextOf(input, max));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="encoding"><see cref="Encoding"/> of the input</param>
        public static IText Sticky(IInput input, Encoding encoding) =>
            Text.Sticky.New(new TextOf(input, encoding));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        /// <param name="encoding">encoding of the <see cref="IInput"/></param>
        /// <param name="max">maximum buffer size</param>
        public static IText Sticky(IInput input, int max, Encoding encoding) =>
            Text.Sticky.New(new TextOf(input, max, encoding));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public static IText Sticky(StringReader rdr) =>
            Text.Sticky.New(new TextOf(rdr));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static IText Sticky(StringReader rdr, Encoding enc) =>
            Text.Sticky.New(new TextOf(rdr, enc));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public static IText Sticky(StreamReader rdr) =>
            Text.Sticky.New(new TextOf(rdr));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static IText Sticky(StreamReader rdr, Encoding cset) =>
            Text.Sticky.New(new TextOf(rdr, cset));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        /// <param name="max">maximum buffer size</param>
        public static IText Sticky(StreamReader rdr, Encoding cset, int max) =>
            Text.Sticky.New(new TextOf(rdr, cset, max));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        public static IText Sticky(StringBuilder builder) =>
            Text.Sticky.New(new TextOf(builder));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static IText Sticky(StringBuilder builder, Encoding enc) =>
            Text.Sticky.New(new TextOf(builder, enc));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        public static IText Sticky(params char[] chars) =>
            Text.Sticky.New(new TextOf(chars));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
        public static IText Sticky(char[] chars, Encoding encoding) =>
            Text.Sticky.New(new TextOf(chars, encoding));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="error"><see cref="Exception"/> to serialize</param>
        public static IText Sticky(Exception error) => Text.Sticky.New(new TextOf(error));

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">a byte array</param>
        public static IText Sticky(params byte[] bytes) => Text.Sticky.New(new TextOf(bytes));

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        public static IText Sticky(IBytes bytes) => Text.Sticky.New(new TextOf(bytes));

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
        public static IText Sticky(IBytes bytes, Encoding encoding) =>
            Text.Sticky.New(new TextOf(bytes, encoding));

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        public static IText Sticky(String input) => Text.Sticky.New(new TextOf(input));

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        /// <param name="encoding"><see cref="Encoding"/> of the string</param>
        public static IText Sticky(String input, Encoding encoding) => Text.Sticky.New(new TextOf(input, encoding));

        /// <summary>
        /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
        /// </summary>
        /// <param name="fnc">func returning a string</param>
        public static IText Sticky(IFunc<string> fnc) => Text.Sticky.New(new TextOf(fnc));

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="txt">scalar of a string</param>
        public static IText Sticky(Func<String> txt) => Text.Sticky.New(new TextOf(txt));
    }
}
