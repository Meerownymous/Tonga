using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Bytes;

    public sealed class BytesOfTest
    {
        [Fact]
        public void ReadsIntIntoBytes()
        {
            int value = 123;
            Assert.True(
                BitConverter.ToInt32(
                    new AsBytes(
                        value
                    ).Raw(),
                    0
                ) == value,
                "Can't read int into bytes"
            );
        }

        [Fact]
        public void ReadsLongIntoBytes()
        {
            long value = 123456789123456789;
            Assert.True(
                BitConverter.ToInt64(
                    new AsBytes(
                        value
                    ).Raw(),
                    0
                ) == value,
                "Can't read long into bytes"
            );
        }

        [Fact]
        public void ReadsDoubleIntoBytes()
        {
            double value = 1.23;
            Assert.True(
                BitConverter.ToDouble(
                    new AsBytes(
                        value
                    ).Raw(),
                    0
                ) == value,
                "Can't read double into bytes"
            );
        }

        [Fact]
        public void ReadsFloatIntoBytes()
        {
            float value = 1.23f;
            Assert.True(
                BitConverter.ToSingle(
                    new AsBytes(
                        value
                    ).Raw(),
                    0
                ) == value,
                "Can't read float into bytes"
            );
        }

        [Fact]
        public void ReadsLargeInMemoryContent()
        {
            int multiplier = 5_000;
            String body = "1234567890";
            Assert.Equal(
                body.Length * multiplier,
                new AsBytes(
                    new AsConduit(
                        body.AsEndless()
                            .AsHead(multiplier)
                            .AsJoined("")
                    )
                ).Raw().Length
            );
        }

        [Fact]
        public void ReadsInputIntoBytes()
        {
            Assert.Equal(
                "Hello, друг!",
                Encoding.UTF8.GetString(
                    new AsBytes(
                        new AsConduit("Hello, друг!")
                    ).Raw()
                )
            );
        }

        [Fact]
        public void ReadsFromReader()
        {
            String source = "hello, друг!";
            Assert.Equal(
                source,
                    new StreamReader(
                        new MemoryStream(
                            Encoding.UTF8.GetBytes(source)
                        )
                    )
                    .AsBytes(Encoding.UTF8)
                    .AsText()
                    .Str()

            );
        }

        [Fact]
        public void ReadsInputIntoBytesWithSmallBuffer()
        {
            String source = "hello, товарищ!";
            Assert.Equal(
                source,
                Encoding.UTF8.GetString(
                    source
                        .AsText()
                        .AsConduit()
                        .AsBytes(2)
                        .Raw()
                )
            );
        }

        [Fact]
        public void ClosesInputStream()
        {
            IText t;
            using (var stream = new MemoryStream("how are you?"u8.ToArray()))
            {
                t = new AsConduit(stream).AsText(Encoding.UTF8);
            }
            Assert.Throws<ObjectDisposedException>(() => t.Str());
        }


        [Fact]
        public void AsBytes()
        {
            IText text = "Hello!".AsText();
            Assert.True(
                StructuralComparisons.StructuralEqualityComparer.Equals(
                    text.AsConduit().AsBytes().Raw(),
                    text.Str().AsBytes().Raw()
                )
            );
        }

        [Fact]
        public void PrintsStackTrace()
        {
            string stackTrace;
            try { throw new IOException("It doesn't work at all"); }
            catch (IOException ex)
            {
                stackTrace = new AsBytes(ex).AsText().Str();
            }

            Assert.True(
                stackTrace.Contains("IOException") &&
                stackTrace.Contains("doesn't work at all"),
                "Can't print exception stacktrace"
            );
        }

        [Fact]
        public void ReadsCharsIntoBytesWithCorrectEncoding()
        {
            var text = "Hello!";
            Assert.Equal(
                text,
                new AsBytes(
                    new Tonga.List.AsList<char>(text),
                    Encoding.Unicode
                ).AsText(Encoding.Unicode)
                .Str()
            );
        }
    }

