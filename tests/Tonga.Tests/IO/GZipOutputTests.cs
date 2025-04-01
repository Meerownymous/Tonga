using System;
using System.IO;
using System.IO.Compression;
using Tonga.Func;
using Tonga.IO;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class GzipOutputTest
    {

        [Fact]
        public void WritesToGzipOutput()
        {
            MemoryStream zipped = new MemoryStream();
            ReadAll._(
                new TeeInput(
                    "Hello!",
                    new GZipOutput(new OutputTo(zipped))
                )
            ).Invoke();

            Assert.Equal(
                "Hello!",
                AsText._(
                    new AsInput(
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

                    Length._(
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
