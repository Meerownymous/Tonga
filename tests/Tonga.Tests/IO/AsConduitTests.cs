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
            AssertText.EndsWith(
                "text!",
                new BackFalling(
                    new LambdaConduit(() =>
                        throw new Exception()
                    ),
                    "Alternative text!"
                )
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
                        new BytesMorph(
                        $"{content}\r\n"
                            .AsRepeated(10)
                            .AsTrimmedRight("\r\n")
                        )
                            .Raw()
                    ),
                    new ConduitMorph(new Uri(path)).Stream()
                )
            ).Trigger();

            AssertText.EndsWith(
                content,
                new ConduitMorph(new Uri(path))
            );
        }

        [Fact]
        public void CanCloseInput()
        {
            Stream input;
            using (input = new MemoryStream("how are you?"u8.ToArray()))
            {
                new TextMorph(new ConduitMorph(input)).Str();
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
                        new BytesMorph(
                            new Joined("\r\n", "Hello World".AsRepeated(10))
                        )
                        .Raw()
                    ),
                    new ConduitMorph(new Uri(path)).Stream()
                )
            ).Trigger();

            AssertText.StartsWith(
                "Hello World",
                new BytesMorph(
                    new ConduitMorph(
                        new Uri(Path.GetFullPath(path))
                    )
                )
            );
        }

        [Fact]
        public void ReadsRealUrl()
        {
            AssertText.Contains(
                "<html",
                new ConduitMorph(
                    new Url("http://www.google.de")
                )
            );
        }

        [Fact]
        public void ReadsFile()
        {
            using var file = new TempFile();
            new FullRead(
                new TeeOnReadStream(
                    "Hello World\r\n"
                        .AsRepeated(1000)
                        .AsStream(),
                    new Uri(file.Value())
                        .AsStream()
                )
            ).Trigger();

            AssertText.Equal(
                1000,
                new TextMorph(
                    new BytesMorph(
                        new ConduitMorph(
                            new Uri(file.Value())
                        )
                    )
                )
                .SplitBy("\r\n")
                .Length()
                .Value()
            );
        }

        [Fact]
        public void ReadsStringIntoBytes()
        {
            var content = "Hello, друг!";

            AssertText.Equal(
                content,
                Encoding.UTF8.GetString(
                    new BytesMorph(
                        new ConduitMorph(content)
                    ).Raw()
                )
            );
        }

        [Fact]
        public void ReadsStringBuilder()
        {
            String starts = "Name it, ";
            String ends = "then it exists!";
            AssertText.Equal(
                starts + ends,
                new ConduitMorph(
                    new StringBuilder(starts).Append(ends)
                )
            );
        }

        [Fact]
        public void ReadsArrayOfChars()
        {
            Assert.Equal(
                "Hold infinity",
                new ConduitMorph(
                    'H', 'o', 'l', 'd', ' ',
                    'i', 'n', 'f', 'i', 'n', 'i', 't', 'y'
                )
            );
        }

        [Fact]
        public void ReadsEncodedArrayOfChars()
        {
            AssertText.Equal(
                "O que sera que sera",
                new ConduitMorph('O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a')
            );
        }

        [Fact]
        public void ReadsStringFromReader()
        {
            String source = "hello, source!";
            AssertText.Equal(
                source,
                new StreamReader(
                    new ConduitMorph(source).Stream()
                )
            );
        }

        [Fact]
        public void ReadsEncodedStringFromReader()
        {
            String source = "hello, друг!";
            AssertText.Equal(
                source,
                new ConduitMorph(
                    new StreamReader(
                        new ConduitMorph(source).Stream()
                    ),
                    Encoding.UTF8
                )
            );
        }

        [Fact]
        public void ReadsAnArrayOfBytes()
        {

            byte[] bytes = [0xCA, 0xFE];
            Assert.True(
                StructuralComparisons.StructuralEqualityComparer.Equals(
                new ConduitAsBytes(
                    new ConduitMorph(bytes)
                ).Raw(), bytes)
            );
        }

        [Fact]
        public void MakesDataAvailable()
        {
            Assert.True(
                new ConduitMorph("Hello,חבר!").Stream().Length > 0
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
                    new ConduitMorph(new Uri(file))
                )
            ).Trigger();

            AssertText.Equal(
                content,
                new ConduitAsBytes(
                    new Uri(file)
                )
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
                new TeeOnRead(txt, file)
            ).Trigger();

            AssertText.Equal(
                txt,
                new ConduitAsBytes(file)
            );
        }

    }
}
