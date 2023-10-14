

using System;
using System.Linq;
using Xunit;
using Tonga.Bytes;
using Tonga.Tests;

namespace Tonga.IO.Tests
{
    public sealed class TailOfTest
    {
        [Fact]
        public void TailsOnLongStream()
        {
            var size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var b =
                new BytesOf(
                    new TailOf(
                        new InputOf(
                            new BytesOf(bytes)
                        ),
                        size - 1
                    )
                ).AsBytes();

            var dest = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, dest, 0, bytes.Length - 1);

            Assert.Equal(
                b,
                dest
            );
        }

        [Fact]
        public void TailsOnExactStream()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var b =
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size
                    )
                ).AsBytes();

            Assert.Equal(
                b,
                bytes
            );
        }

        [Fact]
        public void TailsOnExactStreamAndBuffer()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            Assert.Equal(
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size,
                        size
                    )
                ).AsBytes(),
                bytes
            );
        }

        [Fact]
        public void TailsOnShorterStream()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            Assert.Equal(
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size + 1
                    )
                ).AsBytes(),
                bytes
            );
        }

        [Fact]
        public void TailsOnStreamLongerThanBufferAndBytes()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var res = new byte[bytes.Length - 1];
            Array.Copy(bytes, 1, res, 0, bytes.Length - 1);
            Assert.Equal(
                new BytesOf(
                    new TailOf(
                        new InputOf(new BytesOf(bytes)),
                        size - 1,
                        size - 1
                    )
                ).AsBytes(),
                res
            );
        }

        [Fact]
        public void failsIfBufferSizeSmallerThanTailSize()
        {
            int size = 4;
            var bytes = new RandomBytes(size).ToArray();
            Assert.Throws<ArgumentException>(
                () =>
                {
                    new BytesOf(
                        new TailOf(
                            new InputOf(
                                new BytesOf(bytes)
                            ),
                            size,
                            size - 1
                        )
                    ).AsBytes();
                });
        }
    }
}
