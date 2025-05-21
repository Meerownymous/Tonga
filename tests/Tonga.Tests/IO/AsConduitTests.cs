using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Tonga.Bytes;
using Tonga.Enumerable;
using Tonga.Func;
using Tonga.IO;
using Tonga.Scalar;
using Tonga.Text;
using Xunit;

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
            Assert.EndsWith(
                "text!",
                AsText._(
                    new ConduitWithFallback(
                        new AsConduit(() =>
                            throw new Exception()
                        ),
                        new AsConduit(AsText._("Alternative text!"))
                    )
                )
                .AsString()
);
        }

        [Fact]
        public void ReadsSimpleFileContent()
        {
            var dir = @"artifacts/InputOfTest"; var file = "simple-filecontent.txt"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            String content = "Hello, товарищ!";

            ReadAll._(
                new AsConduit(
                    new TeeStream(
                        new MemoryStream(
                            new AsBytes(
                                new global::Tonga.Text.Joined("\r\n",
                                new Head<string>(
                                    new Endless<string>(content),
                                    10)
                                )
                            ).Bytes()
                        ),
                        new AsConduit(
                            new Uri(path)
                        ).Stream()
                    )
                )
            ).Invoke();

            Assert.True(
                    AsText._(
                        new AsConduit(
                            new Uri(path))
                    ).AsString().EndsWith(content),
                    "Can't read file content");
        }

        [Fact]
        public void ClosesInputStream()
        {
            Stream input;
            using (input = new MemoryStream("how are you?"u8.ToArray()))
            {
                AsText._(
                    new AsConduit(
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
                new AsConduit(
                    new TeeStream(
                        new MemoryStream(
                            new AsBytes(
                                new global::Tonga.Text.Joined("\r\n",
                                    new Head<string>(
                                        new Endless<string>("Hello World"),
                                        10
                                    )
                                )
                            ).Bytes()
                        ),
                        new AsConduit(
                            new Uri(path)
                        ).Stream()
                    )
                )
            ).Invoke();

            Assert.StartsWith(
                "Hello World",
                AsText._(
                    new AsBytes(
                        new AsConduit(
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
                    new AsConduit(
                        new Url("http://www.google.de"))
                ).AsString().Contains("<html"),
                "Can't fetch bytes from the URL"
            );
        }

        [Fact]
        public void ReadsFile()
        {
            using var file = new TempFile();
            ReadAll._(
                new AsConduit(
                    new TeeStream(
                        new MemoryStream(
                            new AsBytes(
                                new global::Tonga.Text.Joined("\r\n",
                                    Tonga.Enumerable.Head._(
                                        Endless._("Hello World"),
                                        1000
                                    )
                                )
                            ).Bytes()
                        ),
                        new AsConduit(
                            new Uri(file.Value())
                        ).Stream()
                    )
                )
            ).Invoke();

            Assert.Equal(
                1000,
                Length._(
                    new Split(
                        AsText._(
                            new AsBytes(
                                new AsConduit(
                                    new Uri(file.Value())
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
                            new AsConduit(content)
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
                            new AsConduit(
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
                            new AsConduit(
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
                        new AsConduit('O', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a', ' ', 'q', 'u', 'e', ' ', 's', 'e', 'r', 'a')
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
                    new AsConduit(
                        new StreamReader(
                            new AsConduit(source).Stream())
                    )
                ).AsString() == source,
                "Can't read string through a reader"
            );
        }

        [Fact]
        public void ReadsEncodedStringFromReader()
        {
            String source = "hello, друг!";
            Assert.Equal(
                source,
                AsText._(
                    new AsConduit(
                            new StreamReader(
                                new AsConduit(source).Stream()),
                            Encoding.UTF8)
                ).AsString()
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
                ).Bytes(), bytes)
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
            ReadAll._(
                new TeeOnRead(
                    content,
                    new AsConduit(new Uri(file))
                )
            ).Invoke();

            Assert.Equal(
                content,
                AsText._(
                        new ConduitAsBytes(
                            new AsConduit(new Uri(file))))
                    .AsString()
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
            ReadAll._(
                new TeeOnRead(
                    txt,
                    new AsConduit(file))
            ).Invoke();

            Assert.Equal(
                txt,
                AsText._(
                        new ConduitAsBytes(
                            new AsConduit(file)
                        )
                    )
                    .AsString()
            );
        }

    }
}
