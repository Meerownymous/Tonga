

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
                    new AsBytes(
                        value
                    ).Bytes(),
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
                    ).Bytes(),
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
                    ).Bytes(),
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
                    ).Bytes(),
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
                new AsBytes(
                    new InputOf(
                        new Text.Joined(
                            "",
                            Head._(
                                Endless._(body),
                                multiplier
                            )
                        )
                    )
                ).Bytes().Length == body.Length * multiplier,
            "Can't read large content from in-memory Input");
        }

        [Fact]
        public void ReadsInputIntoBytes()
        {
            Assert.True(
                Encoding.UTF8.GetString(
                    new AsBytes(
                        new InputOf("Hello, друг!")
                    ).Bytes()) == "Hello, друг!");
        }

        [Fact]
        public void ReadsFromReader()
        {
            String source = "hello, друг!";
            Assert.True(
                AsText._(
                    new AsBytes(
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
                    new AsBytes(
                        new InputOf(
                            AsText._(source)
                        ),
                        2
                    ).Bytes()) == source
                );
        }

        [Fact]
        public void ClosesInputStream()
        {
            IText t;
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes("how are you?")))
            {
                t =
                    AsText._(
                        new InputOf(stream),
                        Encoding.UTF8
                    );
            }

            Assert.Throws<ObjectDisposedException>(() => t.AsString());
        }


        [Fact]
        public void AsBytes()
        {
            IText text = AsText._("Hello!");
            Assert.True(
                StructuralComparisons.StructuralEqualityComparer.Equals(
                    new AsBytes(
                        new InputOf(text)
                        ).Bytes(),
                    new AsBytes(text.AsString()).Bytes()
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
                    AsText._(
                        new AsBytes(
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
                AsText._(
                    new AsBytes(
                        new List.AsList<char>(text),
                        Encoding.Unicode
                    ),
                    Encoding.Unicode
                ).AsString()
            );
        }
    }
}
#pragma warning restore MaxPublicMethodCount // a public methods count maximum
