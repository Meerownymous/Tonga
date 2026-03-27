using System;
using System.IO;
using System.IO.Compression;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO;

public sealed class GZipCompressing
{

    [Fact]
    public void WritesToGzipOutput()
    {
        MemoryStream zipped = new MemoryStream();
        new FullRead(
            new TeeOnRead(
                "Hello!",
                new Tonga.IO.GZipCompressing(zipped)
            )
        ).Trigger();

        AssertText.Equal(
            "Hello!",
            new GZipStream(
                new MemoryStream(zipped.ToArray()),
                CompressionMode.Decompress
            )

        );
    }

    [Fact]
    public void RejectsClosedStream()
    {
        Path.GetFullPath("assets/GZipOutput.txt");
        Assert.Throws<ArgumentException>(() =>
            {
                var stream = new MemoryStream(new byte[256], true);
                stream.Close();
                new FullRead(
                    new TeeOnRead(
                        "Hello!",
                        new Tonga.IO.GZipCompressing(stream)
                    )
                ).Trigger();
            }
        );
    }
}
