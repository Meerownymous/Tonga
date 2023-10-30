

using System;
using System.IO;
using System.Text;
using Xunit;
using Tonga.Bytes;
using Tonga.Text;
using Tonga.Func;

namespace Tonga.IO.Tests
{
    public sealed class TeeInputTest
    {
        [Fact]
        public void CopiesFromUrlToFile()
        {
            using (var directory = new TempDirectory())
            {
                var directoryPath = directory.Value().FullName;
                ReadAll._(
                    new TeeInput(
                        new Uri("http://www.google.de"),
                        new Uri($@"file://{directoryPath}\output.txt")
                    )
                ).Invoke();

                Assert.True(
                    File.ReadAllText(
                        $@"{directoryPath}\output.txt"
                    ).Contains(
                        "<html"
                    ),
                    "Can't copy website to file"
                );
            }
        }

        [Fact]
        public void CopiesFromFileToFile()
        {
            using (var directory = new TempDirectory())
            {
                var directoryPath = directory.Value().FullName;
                File.WriteAllText(
                    $@"{directoryPath}\input.txt",
                    "this is a test"
                );

                ReadAll._(
                    new TeeInput(
                        new Uri($@"{directoryPath}\input.txt"),
                        new Uri($@"{directoryPath}\output.txt")
                    )
                ).Invoke();

                Assert.True(
                    File.ReadAllText(
                        $@"{directoryPath}\output.txt"
                    ).Contains(
                        "this is a test"
                    ),
                    "Can't copy file to another file"
                );
            }
        }

        [Fact]
        public void CopiesContent()
        {
            var baos = new MemoryStream();
            String content = "Hello, товарищ!";
            Assert.True(
                AsText._(
                    new TeeInput(
                        new AsInput(content),
                        new OutputTo(baos)
                    )
                ).AsString() == Encoding.UTF8.GetString(baos.ToArray())
            );
        }

        [Fact]
        public void CopiesToFile()
        {
            var dir = "artifacts/TeeInputTest";
            var file = "txt.txt";
            var path = Path.GetFullPath(Path.Combine(dir, file));

            Directory.CreateDirectory(dir);
            if (File.Exists(path)) File.Delete(path);


            var str =
                AsText._(
                    new AsBytes(
                        new TeeInput(
                            "Hello, друг!",
                            new OutputTo(new Uri(path))
                        )
                    )
                ).AsString();

            Assert.Equal(
                str,
                AsText._(new AsInput(new Uri(path))).AsString()
            );
        }
    }
}
