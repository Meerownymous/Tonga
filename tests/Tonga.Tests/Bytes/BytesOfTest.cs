

using System;
using System.Collections;
using System.IO;
using System.Text;
using Xunit;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Text;

#pragma warning disable MaxPublicMethodCount // a public methods count maximum
namespace Tonga.IO.Tests
{
    public sealed class BytesOfTest
    {
        [Fact]
        public void ReadsIntIntoBytes()
        {
            int value = 123;
            Assert.True(
                BitConverter.ToInt32(
                    new BytesOf(
                        value
                    ).AsBytes(),
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
                    new BytesOf(
                        value
                    ).AsBytes(),
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
                    new BytesOf(
                        value
                    ).AsBytes(),
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
                    new BytesOf(
                        value
                    ).AsBytes(),
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
            Assert.True(
                new BytesOf(
                    new InputOf(
                        new Text.Joined(
                            "",
                            new Head<string>(
                                new Endless<string>(body),
                                multiplier
                            )
                        )
                    )
                ).AsBytes().Length == body.Length * multiplier,
            "Can't read large content from in-memory Input");
        }

        [Fact]
        public void ReadsInputIntoBytes()
        {
            Assert.True(
                Encoding.UTF8.GetString(
                    new BytesOf(
                        new InputOf("Hello, друг!")
                    ).AsBytes()) == "Hello, друг!");
        }

        [Fact]
        public void ReadsFromReader()
        {
            String source = "hello, друг!";
            Assert.True(
                new LiveText(
                    new BytesOf(
                        new StreamReader(
                            new MemoryStream(
                                Encoding.UTF8.GetBytes(source))),
                        Encoding.UTF8,
                        16 << 10
                    )
                ).AsString() == source
            );
        }

        [Fact]
        public void ReadsInputIntoBytesWithSmallBuffer()
        {
            String source = "hello, товарищ!";
            Assert.True(
                Encoding.UTF8.GetString(
                    new BytesOf(
                        new InputOf(
                            new LiveText(source)
                        ),
                        2
                    ).AsBytes()) == source
                );
        }

        [Fact]
        public void ClosesInputStream()
        {
            IText t;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("how are you?")))
            {
                t =
                    new LiveText(
                        new InputOf(stream),
                        Encoding.UTF8
                    );
            }

            Assert.Throws<ObjectDisposedException>(() => t.AsString());
        }


        [Fact]
        public void AsBytes()
        {
            IText text = new LiveText("Hello!");
            Assert.True(
                StructuralComparisons.StructuralEqualityComparer.Equals(
                    new BytesOf(
                        new InputOf(text)
                        ).AsBytes(),
                    new BytesOf(text.AsString()).AsBytes()
                )
            );
        }

        [Fact]
        public void PrintsStackTrace()
        {
            string stackTrace = "";

            try { throw new IOException("It doesn't work at all"); }
            catch (IOException ex)
            {
                stackTrace =
                    new LiveText(
                        new BytesOf(
                            ex
                        )
                    ).AsString();
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
                new TextOf(
                    new BytesOf(
                        new List.ListOf<char>(text),
                        Encoding.Unicode
                    ),
                    Encoding.Unicode
                ).AsString()
            );
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
