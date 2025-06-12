using System;
using System.IO;
using System.Text;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TeeOnReadTest
    {
        [Fact]
        public void CopiesFromUrlToFile()
        {
            using var directory = new TempDirectory();
            var directoryPath = directory.Value().FullName;
            new FullRead(
                new TeeOnRead(
                    new Uri("http://www.google.de"),
                    new Uri($@"file://{directoryPath}\output.txt")
                )
            ).Yield();

            Assert.Contains(
                "<html",
                File.ReadAllText($@"{directoryPath}\output.txt")
            );
        }

        [Fact]
        public void CopiesFromFileToFile()
        {
            using var directory = new TempDirectory();
            var directoryPath = directory.Value().FullName;
            File.WriteAllText(
                $@"{directoryPath}\input.txt",
                "this is a test"
            );

            new FullRead(
                new TeeOnRead(
                    new Uri($@"{directoryPath}\input.txt"),
                    new Uri($@"{directoryPath}\output.txt")
                )
            ).Yield();

            Assert.Contains(
                "this is a test",
                File.ReadAllText($@"{directoryPath}\output.txt")
            );
        }

        [Fact]
        public void CopiesContent()
        {
            var baos = new MemoryStream();
            String content = "Hello, товарищ!";
            Assert.Equal(
                new TeeOnRead(
                    new AsConduit(content),
                    new AsConduit(baos)
                ).AsText().Str(),
                Encoding.UTF8.GetString(baos.ToArray())
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
                new TeeOnRead(
                    "Hello, друг!",
                    new Uri(path).AsConduit()
                ).AsText().Str();

            Assert.Equal(str, new Uri(path).AsText().Str());
        }
    }
}
