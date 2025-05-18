using System;
using System.Text;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.IO.Tests;
using Tonga.Tests.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class InputAsBytesTest
    {

        [Fact]
        public void ReadsLargeInMemoryContent()
        {
            int multiplier = 5_000;
            String body = "1234567890";
            Assert.True(
                new InputAsBytes(
                        new AsConduit(
                        String.Join(
                            "",
                                new Head<string>(
                                new Endless<string>(body),
                                multiplier
                            )
                        )
                    )
                ).Bytes().Length == body.Length * multiplier,
                "Can't read large content from in-memory Input");
        }

        [Fact]
        public void ReadsLargeContent()
        {
            int size = 100_000;
            using (var slow = new SlowInputStream(size))
            {
                Assert.True(
                    new InputAsBytes(
                        new AsConduit(slow)
                    ).Bytes().Length == size,
                    "Can't read large content from Input");
            }
        }

        [Fact]
        public void ReadsInputIntoBytes()
        {
            var content = "Hello, друг!";

            Assert.True(
                    Encoding.UTF8.GetString(
                        new InputAsBytes(
                            new AsConduit(
                                new AsBytes(
                                    AsText._(content)
                                )
                            )
                        ).Bytes()) == content,
                    "cannot read bytes into input");
        }

        [Fact]
        public void ReadsInputIntoBytesWithSmallBuffer()
        {
            var content = "Hello, товарищ!";

            Assert.True(
                    Encoding.UTF8.GetString(
                        new InputAsBytes(
                            new AsConduit(
                                new AsBytes(
                                    AsText._(content)
                                )
                            ),
                            2
                        ).Bytes()) == content,
                    "cannot read bytes with small buffer");
        }

    }
}
