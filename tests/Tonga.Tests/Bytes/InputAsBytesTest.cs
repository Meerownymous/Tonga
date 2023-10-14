

using System;
using System.Text;
using Xunit;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.IO.Tests;
using Tonga.Text;

namespace Tonga.Bytes.Tests
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
                        new InputOf(
                        String.Join(
                            "",
                                new HeadOf<string>(
                                new Endless<string>(body),
                                multiplier
                            )
                        )
                    )
                ).AsBytes().Length == body.Length * multiplier,
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
                        new InputOf(slow)
                    ).AsBytes().Length == size,
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
                            new InputOf(
                                new BytesOf(
                                    new LiveText(content)
                                )
                            )
                        ).AsBytes()) == content,
                    "cannot read bytes into input");
        }

        [Fact]
        public void ReadsInputIntoBytesWithSmallBuffer()
        {
            var content = "Hello, товарищ!";

            Assert.True(
                    Encoding.UTF8.GetString(
                        new InputAsBytes(
                            new InputOf(
                                new BytesOf(
                                    new LiveText(content)
                                )
                            ),
                            2
                        ).AsBytes()) == content,
                    "cannot read bytes with small buffer");
        }

    }
}
