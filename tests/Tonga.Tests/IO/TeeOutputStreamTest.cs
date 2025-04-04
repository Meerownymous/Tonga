using System;
using System.IO;
using System.Text;
using Tonga.IO;
using Xunit;

#pragma warning disable NoStatics // No Statics
namespace Tonga.Tests.IO;

public sealed class TeeOutputStreamTest
{
    [Fact]
    public void CopiesContentByteByByte()
    {
        var baos = new MemoryStream();
        var copy = new MemoryStream();
        String content = "Hello, товарищ!";
        Assert.True(
            TeeOutputStreamTest.AsString(
                new TeeInputStream(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes(content)
                    ),
                    new TeeOutputStream(baos, copy)
                )
            ) ==
            Encoding.UTF8.GetString(baos.ToArray()) &&
            Encoding.UTF8.GetString(baos.ToArray()) ==
            Encoding.UTF8.GetString(copy.ToArray()),
            "Can't copy OutputStream to OutputStream byte by byte");
    }

    private static String AsString(Stream input)
    {
        var baos = new MemoryStream();

        for (var i = 0; i < input.Length; i++)
        {
            baos.Write(new byte[1] { (Byte)input.ReadByte() }, 0, 1);
        }
        input.Dispose();

        return Encoding.UTF8.GetString(baos.ToArray());
    }

}
#pragma warning restore NoStatics // No Statics
