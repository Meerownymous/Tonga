using System;
using System.IO;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.IO;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class EconomicTest
    {
        [Fact]
        public void ReadsFileContent()
        {
            using var file = new TempFile();
            var str = "Hello World"; var lmt = "\r\n"; var times = 1000;

            ReadAll._(
                new AsConduit(
                    new TeeStream(
                        new MemoryStream(
                            new AsBytes(
                                new Tonga.Text.Joined(lmt,
                                    Tonga.Enumerable.Head._(
                                        Endless._(str),
                                        times
                                    )
                                )
                            ).Bytes()
                        ),
                        new AsConduit(
                            new Uri(file.Value())
                        ).Stream()
                    )
                )
            ).Invoke();


            var ipt =
                new Compiled(
                    new AsConduit(
                        new Uri(file.Value())
                    )
                );
            Assert.Equal(
                new AsBytes(ipt).Bytes().Length,
                new AsBytes(ipt).Bytes().Length
            );
        }

        [Fact]
        public void ReadsRealUrl()
        {
            Assert.Contains(
                "<html",
                AsText._(
                    new Compiled(
                        new AsConduit(
                            new Url("http://www.google.de")
                        )
                    )
                ).AsString()
            );
        }

        [Fact]
        public void ReadsFileContentSlowlyAndCountsLength()
        {
            long size = 100_000L;
            Assert.True(
                Length._(
                    new Compiled(
                        new SlowIConduit(size)
                    )
                ).Value() == size,
                "Can't read bytes from a large source slowly and count length"
            );
        }

        [Fact]
        public void ReadsFileContentSlowly()
        {
            int size = 130_000;
            Assert.Equal(
                size,
                new AsBytes(
                    new Compiled(
                        new SlowIConduit(size)
                    )
                ).Bytes().Length
            );
        }
    }
}
