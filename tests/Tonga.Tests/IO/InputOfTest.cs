

using System;
using System.Collections;
using System.IO;
using System.Text;
using Xunit;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Text;
using Tonga.Tests;
using System.Linq;
using System.Diagnostics;
using Tonga.Scalar;
using Tonga.Func;

#pragma warning disable MaxPublicMethodCount

namespace Tonga.IO.Tests
{
    public sealed class InputOfTest
    {
        [Fact]
        public void OpenCloseIsSlowerThanReusing()
        {
            var content = new RandomBytes(1024).ToArray();
            var times = 1000;

            Debug.WriteLine(
                new ElapsedTime(() =>
                {
                    for(var i=0;i<times;i++)
                    {
                        using (var stream = new MemoryStream(content))
                        {
                            byte[] buf = new byte[16 << 10];

                            int bytesRead;
                            while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
                            {
                                _ = (long)bytesRead;
                            }
                            stream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }).AsTimeSpan().TotalMilliseconds
                + "vs " +
                new ElapsedTime(() =>
                {
                    using (var stream = new MemoryStream(content))
                    {
                        for (var i = 0; i < times; i++)
                        {
                            byte[] buf = new byte[16 << 10];

                            int bytesRead;
                            while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
                            {
                                _ = (long)bytesRead;
                            }
                            stream.Seek(0, SeekOrigin.Begin);
                        }
                    }
                }).AsTimeSpan().TotalMilliseconds
            );
        }

        [Fact]
        public void ReadsAlternativeInputForFileCase()
        {
            Assert.True(
                AsText._(
                    new InputWithFallback(
                        new InputOf(
                            new Uri(Path.GetFullPath("/this-file-does-not-exist.txt"))
                        ),
                        new InputOf(AsText._("Alternative text!"))
                    )
                ).AsString().EndsWith("text!"),
                "Can't read alternative source from file not found");
        }

        [Fact]
        public void ReadsSimpleFileContent()
        {
            var dir = @"artifacts/InputOfTest"; var file = "simple-filecontent.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            String content = "Hello, товарищ!";

            ReadAll._(
                new InputOf(
                    new TeeInputStream(
                        new MemoryStream(
                            new AsBytes(
                                new Text.Joined("\r\n",
                                new Head<string>(
                                    new Endless<string>(content),
                                    10)
                                )
                            ).Bytes()
                        ),
                        new OutputTo(
                            new Uri(path)
                        ).Stream()
                    )
                )
            ).Invoke();

            Assert.True(
                    AsText._(
                        new InputOf(
                            new Uri(path))
                    ).AsString().EndsWith(content),
                    "Can't read file content");
        }

        [Fact]
        public void ClosesInputStream()
        {
            Stream input;
            using (input = new MemoryStream(Encoding.UTF8.GetBytes("how are you?")))
            {
                AsText._(
                    new InputOf(
                        input)).AsString();
            }

            Assert.False(input.CanRead,
                "cannot close input stream");
        }

        [Fact]
        public void ReadsFileContent()
        {
            var dir = "artifacts/InputOfTest"; var file = "small-text.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            ReadAll._(
                new InputOf(
                    new TeeInputStream(
                        new MemoryStream(
                            new AsBytes(
                                new Text.Joined("\r\n",
                                    new Head<string>(
                                        new Endless<string>("Hello World"),
                                        10
                                    )
                                )
                            ).Bytes()
                        ),
                        new OutputTo(
                            new Uri(path)
                        ).Stream()
                    )
                )
            ).Invoke();

            Assert.StartsWith(
                "Hello World",
                AsText._(
                    new AsBytes(
                        new InputOf(
                            new Uri(Path.GetFullPath(path))
                        )
                    ).Bytes()
                ).AsString()
            );
        }

        [Fact]
        public void ReadsRealUrl()
        {
            Assert.True(
                AsText._(
                    new InputOf(
                        new Url("http://www.google.de"))
                ).AsString().Contains("<html"),
                "Can't fetch bytes from the URL"
            );
        }

        [Fact]
        public void ReadsRealUrlFromUri()
        {
            Assert.True(
                    AsText._(
                        new InputOf(
                            new Uri("http://www.google.de"))
                    ).AsString().Contains("<html"),
                    "Can't fetch bytes from the URL"
            );
        }

        [Fact]
        public void ReadsFile()
        {
            var dir = "artifacts/InputOfTest"; var file = "large-text.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            ReadAll._(
                new InputOf(
                    new TeeInputStream(
                        new MemoryStream(
                            new AsBytes(
                                new Text.Joined("\r\n",
                                Head._(
                                    Endless._("Hello World"),
                                    1000
                                )
                            )
                        ).Bytes()
                    ),
                    new OutputTo(
                        new Uri(path)).Stream()
                    )
                )
            ).Invoke();

            Assert.Equal(
                1000,
                Length._(
                    new Split(
                        AsText._(
                            new AsBytes(
                                new InputOf(
                                    new Uri(path)
                                )
                            )
                        ), "\r\n")
                ).Value()
            );
        }

        [Fact]
        public void ReadsStringIntoBytes()
        {
            var content = "Hello, друг!";

            Assert.True(
                    Encoding.UTF8.GetString(
                        new AsBytes(
                            new InputOf(content)
                        ).Bytes()) == content,
                    "Can't read bytes from Input");
        }

        [Fact]
        public void ReadsStringBuilder()
        {
            String starts = "Name it, ";
            String ends = "then it exists!";
            Assert.True(
                    AsText._(
                        new AsBytes(
                            new InputOf(
                                new StringBuilder(starts).Append(ends)
                            )
                        ).Bytes()).AsString() == starts + ends,
                    "can't read from stringbuilder"
                );
        }

        [Fact]
        public void ReadsArrayOfChars()
        {
            Assert.True(
                    AsText._(
                        new AsBytes(
                            new InputOf(
                                'H', 'o', 'l', 'd', ' ',
                                'i', 'n', 'f', 'i', 'n', 'i', 't', 'y'
                            )
                        ).Bytes()).AsString() == "Hold infinity",
                    "Can't read array of chars.");
        }

        [Fact]
        public void ReadsEncodedArrayOfChars()
        {
            Assert.True(
                AsText._(
                    new AsBytes(
                        new InputOf(
                            new char[]{
                        'O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                        ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a',
                            }
                        )
                    ).Bytes()
                ).AsString() == "O que sera que sera",
                "Can't read array of encoded chars."
            );
        }

        [Fact]
        public void ReadsStringFromReader()
        {
            String source = "hello, source!";
            Assert.True(
                AsText._(
                    new InputOf(
                        new StreamReader(
                            new InputOf(source).Stream())
                    )
                ).AsString() == source,
                "Can't read string through a reader"
            );
        }

        [Fact]
        public void ReadsEncodedStringFromReader()
        {
            String source = "hello, друг!";
            Assert.True(
                AsText._(
                    new InputOf(
                            new StreamReader(
                                new InputOf(source).Stream()),
                            Encoding.UTF8)
                ).AsString() == source,
                "Can't read encoded string through a reader"
            );
        }

        [Fact]
        public void ReadsAnArrayOfBytes()
        {

            byte[] bytes = new byte[] { (byte)0xCA, (byte)0xFE };
            Assert.True(
                StructuralComparisons.StructuralEqualityComparer.Equals(
                new InputAsBytes(
                    new InputOf(bytes)
                ).Bytes(), bytes),
                "Can't read array of bytes");
        }

        [Fact]
        public void MakesDataAvailable()
        {
            String content = "Hello,חבר!";
            Assert.True(
                new InputOf(content).Stream().Length > 0,
                "Can't show that data is available"
            );
        }

    }
}
