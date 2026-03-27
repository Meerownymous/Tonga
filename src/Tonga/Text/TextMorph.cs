

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
public class TextMorph(Func<string> txt) : IText
{
    public TextMorph(IText txt) : this(txt.Str)
    { }

    /// <summary>
    /// A <see cref="IText"/> out of a int.
    /// </summary>
    /// <param name="input">number</param>
    public TextMorph(int input) : this(() => input + "")
    { }

    /// <summary>
    /// A <see cref="IText"/> out of a long.
    /// </summary>
    /// <param name="input">number</param>
    public TextMorph(long input) : this(() => input + "")
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="double"/></param>
    public TextMorph(double input) : this(
        () => input.ToString(CultureInfo.InvariantCulture)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="double"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public TextMorph(double input, CultureInfo cultureInfo) : this(
        () => input.ToString(cultureInfo)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a float
    /// </summary>
    /// <param name="input">a <see cref="float"/></param>
    public TextMorph(float input) : this(
        () => input.ToString(CultureInfo.InvariantCulture)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a double
    /// </summary>
    /// <param name="input">a <see cref="float"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public TextMorph(float input, CultureInfo cultureInfo) : this(
        () => input.ToString(cultureInfo)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a bool
    /// </summary>
    /// <param name="input">a <see cref="bool"/></param>
    public TextMorph(bool input) : this(
        () => input.ToString(CultureInfo.InvariantCulture)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a bool
    /// </summary>
    /// <param name="input">a <see cref="bool"/></param>
    /// <param name="cultureInfo">info about which culture the text should be formatted for</param>
    public TextMorph(bool input, CultureInfo cultureInfo) : this(
        () => input.ToString(cultureInfo)
    )
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">a file <see cref="Uri"/></param>
    public TextMorph(Uri uri) : this(new ConduitMorph(uri))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Uri"/>.
    /// </summary>
    /// <param name="uri">a file <see cref="Uri"/></param>
    /// <param name="encoding">encoding of the data at the uri</param>
    public TextMorph(Uri uri, Encoding encoding) : this(new ConduitMorph(uri), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
    /// </summary>
    /// <param name="file"></param>
    public TextMorph(FileInfo file) : this(new ConduitMorph(file))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="FileInfo"/>
    /// </summary>
    /// <param name="file"></param>
    /// <param name="encoding"></param>
    public TextMorph(FileInfo file, Encoding encoding) : this(new ConduitMorph(file), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="stream">a <see cref="Stream"/></param>
    public TextMorph(Stream stream) : this(new ConduitMorph(stream))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a <see cref="IConduit"/></param>
    public TextMorph(IConduit origin) : this(new BytesMorph(origin))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a <see cref="IConduit"/></param>
    public TextMorph(ConduitEnvelope origin) : this(new BytesMorph(origin.Stream))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a input</param>
    /// <param name="max">maximum buffer size</param>
    public TextMorph(IConduit origin, int max) : this(origin, max, Encoding.GetEncoding(0))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a input</param>
    /// <param name="encoding"><see cref="Encoding"/> of the input</param>
    public TextMorph(IConduit origin, Encoding encoding) : this(new BytesMorph(origin), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a <see cref="IConduit"/></param>
    /// <param name="encoding">encoding of the <see cref="IConduit"/></param>
    /// <param name="max">maximum buffer size</param>
    public TextMorph(IConduit origin, int max, Encoding encoding) : this(new BytesMorph(origin, max), encoding)
    { }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a <see cref="IConduit"/></param>
    public TextMorph(ConduitMorph origin) : this(new BytesMorph(origin))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a input</param>
    /// <param name="max">maximum buffer size</param>
    public TextMorph(ConduitMorph origin, int max) : this(origin, max, Encoding.GetEncoding(0))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a input</param>
    /// <param name="encoding"><see cref="Encoding"/> of the input</param>
    public TextMorph(ConduitMorph origin, Encoding encoding) : this(new BytesMorph(origin), encoding)
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="IConduit"/>.
    /// </summary>
    /// <param name="origin">a <see cref="IConduit"/></param>
    /// <param name="encoding">encoding of the <see cref="IConduit"/></param>
    /// <param name="max">maximum buffer size</param>
    public TextMorph(ConduitMorph origin, int max, Encoding encoding) : this(new BytesMorph(origin, max), encoding)
    { }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    public TextMorph(StringReader rdr) : this(new ReaderAsBytes(rdr))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public TextMorph(StringReader rdr, Encoding enc) : this(new ReaderAsBytes(rdr, enc))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    public TextMorph(StreamReader rdr) : this(new BytesMorph(rdr))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public TextMorph(StreamReader rdr, Encoding enc) : this(new BytesMorph(rdr, enc))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StreamReader"/>.
    /// </summary>
    /// <param name="rdr">a <see cref="StreamReader"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    /// <param name="max">maximum buffer size</param>
    public TextMorph(StreamReader rdr, Encoding enc, int max) : this(new BytesMorph(rdr, enc, max))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">a <see cref="StringBuilder"/></param>
    public TextMorph(StringBuilder builder) : this(new BytesMorph(builder))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">a <see cref="StringBuilder"/></param>
    /// <param name="enc"><see cref="Encoding"/> of the <see cref="StreamReader"/></param>
    public TextMorph(StringBuilder builder, Encoding enc) : this(new BytesMorph(builder, enc))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">a char array</param>
    public TextMorph(params char[] chars) : this(new BytesMorph(chars))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="char"/> array.
    /// </summary>
    /// <param name="chars">a char array</param>
    /// <param name="encoding"><see cref="Encoding"/> of the chars</param>
    public TextMorph(char[] chars, Encoding encoding) : this(new BytesMorph(chars, encoding))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="Exception"/>.
    /// </summary>
    /// <param name="error"><see cref="Exception"/> to serialize</param>
    public TextMorph(Exception error) : this(new BytesMorph(error))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of a <see cref="byte"/> array.
    /// </summary>
    /// <param name="bytes">a byte array</param>
    public TextMorph(params byte[] bytes) : this(new BytesMorph(bytes))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    public TextMorph(IBytes bytes) : this(new BytesMorph(bytes))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    public TextMorph(BytesEnvelope bytes) : this(new BytesMorph(bytes))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    public TextMorph(BytesMorph bytes) : this(bytes, Encoding.GetEncoding(0))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
    public TextMorph(BytesMorph bytes, Encoding encoding) : this(
        () =>
        {
            var memoryStream = new MemoryStream(bytes.Raw());
            return new StreamReader(memoryStream, encoding).ReadToEnd(); // removes the BOM from the Byte-Array
        })
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="IBytes"/> object.
    /// </summary>
    /// <param name="bytes">A <see cref="IBytes"/> object</param>
    /// <param name="encoding"><see cref="Encoding"/> of the <see cref="IBytes"/> object</param>
    public TextMorph(IBytes bytes, Encoding encoding) : this(new BytesMorph(bytes), encoding)
    { }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="string"/>.
    /// </summary>
    /// <param name="input">a string</param>
    public TextMorph(String input) : this(input, Encoding.GetEncoding(0))
    {
    }

    /// <summary>
    /// A <see cref="IText"/> out of <see cref="string"/>.
    /// </summary>
    /// <param name="input">a string</param>
    /// <param name="encoding"><see cref="Encoding"/> of the string</param>
    public TextMorph(String input, Encoding encoding) : this(
        () => encoding.GetString(encoding.GetBytes(input))
    )
    { }

    public string Str() => txt();

    public static implicit operator TextMorph(TextEnvelope value) => new(value.Str);
    public static implicit operator TextMorph(BytesEnvelope value) => new(value);
    public static implicit operator TextMorph(ConduitEnvelope value) => new(value);
    public static implicit operator TextMorph(ConduitMorph value) => new(value);
    public static implicit operator TextMorph(string value) => new(value);
    public static implicit operator TextMorph(int value) => new(value);
    public static implicit operator TextMorph(long value) => new(value);
    public static implicit operator TextMorph(double value) => new(value);
    public static implicit operator TextMorph((double value, CultureInfo culture) prm) => new(prm.value, prm.culture);
    public static implicit operator TextMorph(float value) => new(value);
    public static implicit operator TextMorph((float value, CultureInfo culture) prm) => new(prm.value, prm.culture);
    public static implicit operator TextMorph(bool value) => new(value);
    public static implicit operator TextMorph((bool value, CultureInfo culture) prm) => new(prm.value, prm.culture);
    public static implicit operator TextMorph(Uri uri) => new(uri);
    public static implicit operator TextMorph(FileInfo file) => new(file);
    public static implicit operator TextMorph(Stream value) => new(value);
    public static implicit operator TextMorph(StringReader value) => new(value);
    public static implicit operator TextMorph(StreamReader value) => new(value);
    public static implicit operator TextMorph(StringBuilder value) => new(value);
    public static implicit operator TextMorph(char[] value) => new(value);
    public static implicit operator TextMorph(char value) => new(value);
    public static implicit operator TextMorph(Exception value) => new(value);
    public static implicit operator TextMorph(byte[] value) => new(value);
    public static implicit operator TextMorph(byte value) => new(value);
    public static implicit operator TextMorph(BytesMorph value) => new(value);
}

