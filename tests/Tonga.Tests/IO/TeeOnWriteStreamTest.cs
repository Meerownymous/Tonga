using System;
using System.IO;
using System.Text;
using Tonga.IO;
using Xunit;
namespace Tonga.Tests.IO;

public sealed class TeeOnWriteStreamTest
{
    [Fact]
    public void CopiesContentByteByByte()
    {
        var baos = new MemoryStream();
        var copy = new MemoryStream();
        String content = "Hello, товарищ!";
        Assert.True(
            AsString(
                new TeeOnReadStream(
                    new MemoryStream(
                        Encoding.UTF8.GetBytes(content)
                    ),
                    new TeeOnWriteStream(baos, copy)
                )
            ) ==
            Encoding.UTF8.GetString(baos.ToArray()) &&
            Encoding.UTF8.GetString(baos.ToArray()) ==
            Encoding.UTF8.GetString(copy.ToArray()),
            "Can't copy OutputStream to OutputStream byte by byte");

        static string AsString(Stream input)
        {
            var baos = new MemoryStream();
            for (var i = 0L; i < input.Length; i++)
                baos.WriteByte((byte)input.ReadByte());
            input.Dispose();
            return Encoding.UTF8.GetString(baos.ToArray());
        }
    }
}
