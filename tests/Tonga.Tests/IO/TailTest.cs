using System;
using System.Linq;
using Tonga.Bytes;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TailTest
    {
        [Fact]
        public void TailsOnLongStream()
        {
            var size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var b =
                new BytesMorph(
                    new ConduitMorph(bytes)
                        .AsTail(size - 1)
                ).Raw();

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
                new BytesMorph(
                    new ConduitMorph(
                        bytes
                    ).AsTail(size)
                ).Raw();

            AssertBytes.Equal(
                b,
                bytes
            );
        }

        [Fact]
        public void TailsOnExactStreamAndBuffer()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            AssertBytes.Equal(
                new Tail(
                    new Tonga.IO.ConduitMorph(new BytesMorph(bytes)),
                    size,
                    size
                ),
                bytes
            );
        }

        [Fact]
        public void TailsOnShorterStream()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            AssertBytes.Equal(
                new Tail(
                    new ConduitMorph(bytes),
                    size + 1
                ),
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
            AssertBytes.Equal(
                res,
                new Tail(
                    bytes,
                    size - 1,
                    size - 1
                )
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
                    new Tail(bytes, size, size - 1).Stream();
                }
            );
        }
    }
}
