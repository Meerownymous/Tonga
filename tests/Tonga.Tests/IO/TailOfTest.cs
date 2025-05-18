using System;
using System.Linq;
using Tonga.Bytes;
using Tonga.IO;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TailOfTest
    {
        [Fact]
        public void TailsOnLongStream()
        {
            var size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            var b =
                new AsBytes(
                    new Tail(
                        new Tonga.IO.AsConduit(
                            new AsBytes(bytes)
                        ),
                        size - 1
                    )
                ).Bytes();

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
                new AsBytes(
                    new Tail(
                        new Tonga.IO.AsConduit(new AsBytes(bytes)),
                        size
                    )
                ).Bytes();

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
                new AsBytes(
                    new Tail(
                        new Tonga.IO.AsConduit(new AsBytes(bytes)),
                        size,
                        size
                    )
                ).Bytes(),
                bytes
            );
        }

        [Fact]
        public void TailsOnShorterStream()
        {
            int size = 4;
            byte[] bytes = new RandomBytes(size).ToArray();

            Assert.Equal(
                new AsBytes(
                    new Tail(
                        new Tonga.IO.AsConduit(new AsBytes(bytes)),
                        size + 1
                    )
                ).Bytes(),
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
                new AsBytes(
                    new Tail(
                        new Tonga.IO.AsConduit(new AsBytes(bytes)),
                        size - 1,
                        size - 1
                    )
                ).Bytes(),
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
                    new AsBytes(
                        new Tail(
                            new Tonga.IO.AsConduit(
                                new AsBytes(bytes)
                            ),
                            size,
                            size - 1
                        )
                    ).Bytes();
                });
        }
    }
}
