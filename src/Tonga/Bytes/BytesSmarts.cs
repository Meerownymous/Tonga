using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tonga.Bytes;

public static class BytesSmarts
{
    public static IBytes AsBytes(this IInput input) => new AsBytes(input);
    public static IBytes AsBytes(this IInput input, int max) => new AsBytes(input, max);
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
