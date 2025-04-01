

using System;
using System.IO;
using Xunit;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.Text;
using Tonga.Scalar;
using Microsoft.VisualStudio.TestPlatform.PlatformAbstractions.Interfaces;
using Tonga.Tests.IO;

namespace Tonga.IO.Tests
{
    public sealed class StickyTest
    {
        [Fact]
        public void ReadsFileContent()
        {
            using (var file = new TempFile())
            {
                var str = "Hello World"; var lmt = "\r\n"; var times = 1000;

                ReadAll._(
                    new AsInput(
                        new TeeInputStream(
                            new MemoryStream(
                                new AsBytes(
                                    new Text.Joined(lmt,
                                        Enumerable.Head._(
                                            Endless._(str),
                                            times
                                        )
                                    )
                                ).Bytes()
                            ),
                            new OutputTo(
                                new Uri(file.Value())
                            ).Stream()
                        )
                    )
                ).Invoke();

                
                var ipt =
                    new Sticky(
                        new AsInput(
                            new Uri(file.Value())
                        )
                    );
                Assert.Equal(
                    new AsBytes(ipt).Bytes().Length,
                    new AsBytes(ipt).Bytes().Length
                );
            }
        }

        [Fact]
        public void ReadsRealUrl()
        {
            Assert.Contains(
                "<html",
                AsText._(
                    new Sticky(
                        new AsInput(
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
                    new Sticky(
                        new SlowInput(size)
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
                    new Sticky(
                        new SlowInput(size)
                    )
                ).Bytes().Length
            );
        }
    }
}
