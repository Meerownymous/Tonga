

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
    public sealed class AsText : IText
    {
        private readonly System.Func<string> origin;

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
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        public AsText(double input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public AsText(double input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a float
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        public AsText(float input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public AsText(float input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        public AsText(bool input) : this(
            () => input.ToString(CultureInfo.InvariantCulture)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public AsText(bool input, CultureInfo cultureInfo) : this(
            () => input.ToString(cultureInfo)
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        public AsText(Uri uri) : this(new InputOf(uri))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        /// <param name="encoding">encoding of the data at the uri</param>
        public AsText(Uri uri, Encoding encoding) : this(new InputOf(uri), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        public AsText(FileInfo file) : this(new InputOf(file))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        public AsText(FileInfo file, Encoding encoding) : this(new InputOf(file), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="stream">a <see cref="Stream"/></param>
        public AsText(Stream stream) : this(new InputOf(stream))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        public AsText(IInput input) : this(new AsBytes(input))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="max">maximum buffer size</param>
        public AsText(IInput input, int max) : this(input, max, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="encoding"><see cref="Encoding"/> of the input</param>
        public AsText(IInput input, Encoding encoding) : this(new AsBytes(input), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        /// <param name="encoding">encoding of the <see cref="IInput"/></param>
        /// <param name="max">maximum buffer size</param>
        public AsText(IInput input, int max, Encoding encoding) : this(new AsBytes(input, max), encoding)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public AsText(StringReader rdr) : this(new AsBytes(new InputOf(rdr)))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public AsText(StringReader rdr, Encoding enc) : this(new AsBytes(rdr), enc)
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public AsText(StreamReader rdr) : this(new AsBytes(rdr))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public AsText(StreamReader rdr, Encoding cset) : this(new AsBytes(rdr, cset))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        /// <param name="max">maximum buffer size</param>
        public AsText(StreamReader rdr, Encoding cset, int max) : this(new AsBytes(rdr, cset, max))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        public AsText(StringBuilder builder) : this(new AsBytes(builder))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public AsText(StringBuilder builder, Encoding enc) : this(new AsBytes(builder, enc))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        public AsText(params char[] chars) : this(new AsBytes(chars))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
        public AsText(char[] chars, Encoding encoding) : this(new AsBytes(chars, encoding))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="error"><see cref="Exception"/> to serialize</param>
        public AsText(Exception error) : this(new AsBytes(error))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">a byte array</param>
        public AsText(params byte[] bytes) : this(new AsBytes(bytes))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        public AsText(IBytes bytes) : this(bytes, Encoding.GetEncoding(0))
        { }

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
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        public AsText(String input) : this(input, Encoding.GetEncoding(0))
        { }

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        /// <param name="encoding"><see cref="Encoding"/> of the string</param>
        public AsText(String input, Encoding encoding) : this(
            () => encoding.GetString(encoding.GetBytes(input))
        )
        { }

        /// <summary>
        /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
        /// </summary>
        /// <param name="fnc">func returning a string</param>
        public AsText(IFunc<string> fnc) : this(() => fnc.Invoke())
        { }

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="txt">scalar of a string</param>
        public AsText(Func<string> txt)
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
        public static AsText _(int input) => new AsText(input);

        /// <summary>
        /// A <see cref="IText"/> out of a long.
        /// </summary>
        /// <param name="input">number</param>
        public static AsText _(long input) => new AsText(input);

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        public static AsText _(double input) => new AsText(input);

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="double"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static AsText _(double input, CultureInfo cultureInfo) => new AsText(input, cultureInfo);

        /// <summary>
        /// A <see cref="IText"/> out of a float
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        public static AsText _(float input) => new AsText(input);

        /// <summary>
        /// A <see cref="IText"/> out of a double
        /// </summary>
        /// <param name="input">a <see cref="float"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static AsText _(float input, CultureInfo cultureInfo) => new AsText(input, cultureInfo);

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        public static AsText _(bool input) => new AsText(input);

        /// <summary>
        /// A <see cref="IText"/> out of a bool
        /// </summary>
        /// <param name="input">a <see cref="bool"/></param>
        /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
        public static AsText _(bool input, CultureInfo cultureInfo) => new AsText(input, cultureInfo);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        public static AsText _(Uri uri) => new AsText(uri);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Uri"/>.
        /// </summary>
        /// <param name="uri">a file <see cref="Uri"/></param>
        /// <param name="encoding">encoding of the data at the uri</param>
        public static AsText _(Uri uri, Encoding encoding) => new AsText(uri, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        public static AsText _(FileInfo file) => new AsText(file);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
        /// </summary>
        /// <param name="file"></param>
        /// <param name="encoding"></param>
        public static AsText _(FileInfo file, Encoding encoding) => new AsText(file, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="stream">a <see cref="Stream"/></param>
        public static AsText _(Stream stream) => new AsText(stream);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        public static AsText _(IInput input) => new AsText(input);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="max">maximum buffer size</param>
        public static AsText _(IInput input, int max) => new AsText(input, max);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a input</param>
        /// <param name="encoding"><see cref="Encoding"/> of the input</param>
        public static AsText _(IInput input, Encoding encoding) => new AsText(input, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="IInput"/>.
        /// </summary>
        /// <param name="input">a <see cref="IInput"/></param>
        /// <param name="encoding">encoding of the <see cref="IInput"/></param>
        /// <param name="max">maximum buffer size</param>
        public static AsText _(IInput input, int max, Encoding encoding) => new AsText(input, max, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public static AsText _(StringReader rdr) => new AsText(rdr);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static AsText _(StringReader rdr, Encoding enc) => new AsText(rdr, enc);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        public static AsText _(StreamReader rdr) => new AsText(rdr);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static AsText _(StreamReader rdr, Encoding cset) => new AsText(rdr, cset);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
        /// </summary>
        /// <param name="rdr">a <see cref="StreamReader"/></param>
        /// <param name="cset"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        /// <param name="max">maximum buffer size</param>
        public static AsText _(StreamReader rdr, Encoding cset, int max) => new AsText(rdr, cset, max);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        public static AsText _(StringBuilder builder) => new AsText(builder);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="builder">a <see cref="StringBuilder"/></param>
        /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
        public static AsText _(StringBuilder builder, Encoding enc) => new AsText(builder, enc);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        public static AsText _(params char[] chars) => new AsText(chars);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="char"/> array.
        /// </summary>
        /// <param name="chars">a char array</param>
        /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
        public static AsText _(char[] chars, Encoding encoding) => new AsText(chars, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="Exception"/>.
        /// </summary>
        /// <param name="error"><see cref="Exception"/> to serialize</param>
        public static AsText _(Exception error) => new AsText(error);

        /// <summary>
        /// A <see cref="IText"/> out of a <see cref="byte"/> array.
        /// </summary>
        /// <param name="bytes">a byte array</param>
        public static AsText _(params byte[] bytes) => new AsText(bytes);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        public static AsText _(IBytes bytes) => new AsText(bytes);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
        /// </summary>
        /// <param name="bytes">A <see cref="IBytes"/> object</param>
        /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
        public static AsText _(IBytes bytes, Encoding encoding) => new AsText(bytes, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        public static AsText _(String input) => new AsText(input);

        /// <summary>
        /// A <see cref="IText"/> out of <see cref="string"/>.
        /// </summary>
        /// <param name="input">a string</param>
        /// <param name="encoding"><see cref="Encoding"/> of the string</param>
        public static AsText _(String input, Encoding encoding) => new AsText(input, encoding);

        /// <summary>
        /// A <see cref="IText"/> out of the return value of a <see cref="IFunc{T}"/>.
        /// </summary>
        /// <param name="fnc">func returning a string</param>
        public static AsText _(IFunc<string> fnc) => new AsText(fnc);

        /// <summary>
        /// A <see cref="IText"/> out of encapsulating <see cref="IScalar{T}"/>.
        /// </summary>
        /// <param name="txt">scalar of a string</param>
        public static AsText _(System.Func<string> txt) => new AsText(txt);
    }
}
