

using System;
using System.IO;
using System.Text;
using Xunit;
using Tonga.Bytes;
using Tonga.Text;

namespace Tonga.IO.Tests
{
    public sealed class InputStreamOfTest
    {
        [Fact]
        public void ReadsSimpleFileContent()
        {
            var dir = "artifacts/InputStreamOfTest"; var file = "txt-1"; var path = Path.GetFullPath(Path.Combine(dir, file));
            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            String content = "Hello, товарищ!";
            File.WriteAllBytes(path, new BytesOf(new LiveText(content, Encoding.UTF8)).AsBytes());

            Assert.True(
                new LiveText(
                    new InputAsBytes(
                        new InputOf(
                            new InputStreamOf(
                                new Uri(path))))
                ).AsString() == content,
                "Can't read file content");

        }

        [Fact]
        public void ReadsFromReader()
        {
            String content = "Hello, дорогой товарищ!";
            Assert.True(
                new LiveText(
                    new InputOf(
                        new InputStreamOf(
                            new StreamReader(
                                new InputOf(content).Stream())))
                ).AsString() == content);
        }

        [Fact]
        public void ReadsFromReaderThroughSmallBuffer()
        {
            String content = "Hello, صديق!";
            Assert.True(
                new LiveText(
                    new InputOf(
                        new InputStreamOf(
                            new StreamReader(
                                new InputOf(content).Stream()),
                            1
                        )
                    )
                ).AsString() == content,
                "Can't read from reader through small buffer"
            );
        }

        [Fact]
        public void MakesDataAvailable()
        {
            String content = "Hello,חבר!";
            Assert.True(
                new InputStreamOf(content).Length > 0,
                "Can't show that data is available"
            );
        }

        [Fact]
        public void ReadsSimpleFileContentWithWhitespacesInUri()
        {
            var dir = "artifacts/Input StreamOf Test";
            var file = "txt-1";
            var path = Path.GetFullPath(Path.Combine(dir, file));

            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);

            String content = "Hello, товарищ!";
            File.WriteAllBytes(path, new BytesOf(new LiveText(content, Encoding.UTF8)).AsBytes());

            Assert.True(
                new LiveText(
                    new InputAsBytes(
                        new InputOf(
                            new InputStreamOf(
                                new Uri(path))))
                ).AsString() == content,
                "Can't read file content"
            );
        }
    }
}
