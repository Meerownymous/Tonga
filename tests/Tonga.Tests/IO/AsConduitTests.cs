using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.IO;
using Tonga.Text;
using Xunit;
using Length = Tonga.Enumerator.Length;

#pragma warning disable MaxPublicMethodCount

namespace Tonga.Tests.IO
{
    public sealed class AsConduitTests
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
                        using var stream = new MemoryStream(content);
                        byte[] buf = new byte[16 << 10];

                        int bytesRead;
                        while ((bytesRead = stream.Read(buf, 0, buf.Length)) > 0)
                        {
                            _ = (long)bytesRead;
                        }
                        stream.Seek(0, SeekOrigin.Begin);
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
            Assert.EndsWith(
                "text!",
                new BackFalling(
                    new AsConduit(
                        new Func<FileInfo>(() =>
                            throw new Exception()
                        )
                    ),
                    new AsConduit(
                        "Alternative text!".AsText()
                    )
                ).AsText()
                .Str()
);
        }

        [Fact]
        public void ReadsSimpleFileContent()
        {
            using var tempDir = new TempDirectory();
            var file = "simple-filecontent.txt";
            var path = Path.GetFullPath(Path.Combine(tempDir.Value().FullName, file));
            String content = "Hello, товарищ!";

            new FullRead(
                new TeeOnReadStream(
                    new MemoryStream(
                        $"{content}\r\n"
                            .AsRepeated(10)
                            .AsTrimmedRight("\r\n")
                            .AsBytes()
                            .Raw()
                    ),
                    new Uri(path)
                        .AsConduit()
                        .Stream()
                )
            ).Yield();

            Assert.EndsWith(
                content,
                new AsConduit(
                        new Uri(path)
                    )
                    .AsText()
                    .Str()
            );
        }

        [Fact]
        public void CanCloseInput()
        {
            Stream input;
            using (input = new MemoryStream("how are you?"u8.ToArray()))
            {
                new AsConduit(input).AsText().Str();
            }
            Assert.False(input.CanRead);
        }

        [Fact]
        public void ReadsFileContent()
        {
            using var tempDir = new TempDirectory();
            var file = "small-text.txt";
            var path = Path.GetFullPath(Path.Combine(tempDir.Value().FullName, file));

            new FullRead(
                new TeeOnReadStream(
                    new MemoryStream(
                        new Joined("\r\n",
                            "Hello World".AsRepeated(10)
                        ).AsBytes()
                        .Raw()
                    ),
                    new AsConduit(
                        new Uri(path)
                    ).Stream()
                ).AsConduit()
            ).Yield();

            Assert.StartsWith(
                "Hello World",
                new AsBytes(
                    new AsConduit(
                        new Uri(Path.GetFullPath(path))
                    )
                )
                .AsText()
                .Str()
            );
        }

        [Fact]
        public void ReadsRealUrl()
        {
            Assert.Contains(
                "<html",
                new AsConduit(
                    new Url("http://www.google.de")
                ).AsText().Str()
            );
        }

        [Fact]
        public void ReadsFile()
        {
            using var file = new TempFile();
            new FullRead(
                new AsConduit(
                    new TeeOnReadStream(
                            "Hello World\r\n"
                                .AsRepeated(1000)
                                .AsStream(),
                        new Uri(file.Value()).AsStream()
                    )
                )
            ).Yield();

            Assert.Equal(
                1000,
                new Uri(file.Value())
                    .AsConduit()
                    .AsBytes()
                    .AsText()
                    .AsSplit("\r\n")
                    .Length()
                    .Value()
            );
        }

        [Fact]
        public void ReadsStringIntoBytes()
        {
            var content = "Hello, друг!";

            Assert.Equal(
                content,
                Encoding.UTF8.GetString(
                    new AsConduit(content)
                        .AsBytes()
                        .Raw()
                )
            );
        }

        [Fact]
        public void ReadsStringBuilder()
        {
            String starts = "Name it, ";
            String ends = "then it exists!";
            Assert.Equal(
                starts + ends,
                new AsConduit(
                    new StringBuilder(starts).Append(ends)
                ).AsText()
                .Str()
            );
        }

        [Fact]
        public void ReadsArrayOfChars()
        {
            Assert.Equal(
                "Hold infinity",
                new AsConduit(
                    'H', 'o', 'l', 'd', ' ',
                    'i', 'n', 'f', 'i', 'n', 'i', 't', 'y'
                ).AsText().Str()
            );
        }

        [Fact]
        public void ReadsEncodedArrayOfChars()
        {
            Assert.Equal(
                "O que sera que sera",
                new AsConduit('O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a')
                    .AsText()
                    .Str()
                );
        }

        [Fact]
        public void ReadsStringFromReader()
        {
            String source = "hello, source!";
            Assert.Equal(
                source,
                new AsConduit(
                    new StreamReader(
                        new AsConduit(source).Stream()
                    )
                ).AsText()
                .Str()
            );
        }

        [Fact]
        public void ReadsEncodedStringFromReader()
        {
            String source = "hello, друг!";
            Assert.Equal(
                source,
                new AsConduit(
                    new StreamReader(
                        new AsConduit(source).Stream()
                    ),
                    Encoding.UTF8
                ).AsText()
                .Str()
            );
        }

        [Fact]
        public void ReadsAnArrayOfBytes()
        {

            byte[] bytes = [0xCA, 0xFE];
            Assert.True(
                StructuralComparisons.StructuralEqualityComparer.Equals(
                new ConduitAsBytes(
                    new AsConduit(bytes)
                ).Raw(), bytes)
            );
        }

        [Fact]
        public void MakesDataAvailable()
        {
            Assert.True(
                new AsConduit("Hello,חבר!").Stream().Length > 0
            );
        }

        [Fact]
        public void WritesSimplePathContent()
        {
            var temp = Directory.CreateDirectory("artifacts/OutputToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file)) File.Delete(file);

            String content = "Hello, товарищ!";
            new FullRead(
                new TeeOnRead(
                    content,
                    new AsConduit(new Uri(file))
                )
            ).Yield();

            Assert.Equal(
                content,
                new ConduitAsBytes(
                    new AsConduit(new Uri(file))
                ).AsText().Str()
            );
        }

        [Fact]
        public void WritesSimpleFileContent()
        {
            var temp = Directory.CreateDirectory("artifacts/OutputToTest");
            var file = new Uri(Path.GetFullPath(Path.Combine(temp.FullName, "file.txt")));
            if (File.Exists(file.AbsolutePath))
            {
                File.Delete(file.AbsolutePath);
            }

            String txt = "Hello, друг!";
            new FullRead(
                new TeeOnRead(
                    txt,
                    new AsConduit(file))
            ).Yield();

            Assert.Equal(
                txt,
                new ConduitAsBytes(
                    new AsConduit(file)
                ).AsText()
                .Str()
            );
        }

    }
}
