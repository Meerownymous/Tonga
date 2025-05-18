using System;
using System.IO;
using System.Text;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class AsInputStreamTest
    {
        [Fact]
        public void ReadsSimpleFileContent()
        {
            using (var file = new TempFile())
            {
                String content = "Hello, товарищ!";
                File.WriteAllBytes(
                    file.Value(),
                    new AsBytes(
                        AsText._(content, Encoding.UTF8)
                    ).Bytes()
                );

                Assert.Equal(
                    content,
                    AsText._(
                        new InputAsBytes(
                            new Tonga.IO.AsConduit(
                                new AsInputStream(
                                    new Uri(file.Value())
                                )
                            )
                        )
                    ).AsString()
                );

            }
        }

        [Fact]
        public void ReadsFromReader()
        {
            String content = "Hello, дорогой товарищ!";
            Assert.True(
                AsText._(
                    new Tonga.IO.AsConduit(
                        new AsInputStream(
                            new StreamReader(
                                new Tonga.IO.AsConduit(content).Stream())))
                ).AsString() == content);
        }

        [Fact]
        public void ReadsFromReaderThroughSmallBuffer()
        {
            String content = "Hello, صديق!";
            Assert.Equal(
                content,
                AsText._(
                    new Tonga.IO.AsConduit(
                        new AsInputStream(
                            new StreamReader(
                                new Tonga.IO.AsConduit(content).Stream()),
                            1
                        )
                    )
                ).AsString()
            );
        }

        [Fact]
        public void MakesDataAvailable()
        {
            String content = "Hello,חבר!";
            Assert.True(
                new AsInputStream(content).Length > 0,
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
            File.WriteAllBytes(path, new AsBytes(AsText._(content, Encoding.UTF8)).Bytes());

            Assert.True(
                AsText._(
                    new InputAsBytes(
                        new Tonga.IO.AsConduit(
                            new AsInputStream(
                                new Uri(path))))
                ).AsString() == content,
                "Can't read file content"
            );
        }
    }
}
