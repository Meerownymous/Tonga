

using System;
using System.IO;
using System.IO.Compression;
using Xunit;
using Tonga.Bytes;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class GzipOutputTest
    {

        [Fact]
        public void WritesToGzipOutput()
        {
            MemoryStream zipped = new MemoryStream();
            new LengthOf(
                new TeeInput(
                    "Hello!",
                    new GZipOutput(new OutputTo(zipped))
                )
            ).Value();

            Assert.Equal(
                "Hello!",
                new TextOf(
                    new InputOf(
                        new GZipStream(
                            new MemoryStream(zipped.ToArray()),
                            CompressionMode.Decompress
                        )
                    )
                ).AsString()
            );
        }

        [Fact]
        public void RejectsWhenClosed()
        {
            
            var path = Path.GetFullPath("assets/GZipOutput.txt");

            Assert.Throws<ArgumentException>(() =>
                {
                    var stream = new MemoryStream(new byte[256], true);
                    stream.Close();

                    new LengthOf(
                        new TeeInput(
                            "Hello!",
                            new GZipOutput(
                                new OutputTo(stream)
                            )
                        )
                    ).Value();
                }
            );
        }
    }

}
