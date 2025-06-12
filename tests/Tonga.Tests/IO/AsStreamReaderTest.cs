using System;
using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class ReaderOfTest
    {
        [Fact]
        public void ReadsSimpleFileContent()
        {
            var dir = "artifacts/ReaderOfTest";
            var file = "txt-1";
            var path = Path.GetFullPath(Path.Combine(dir, file));
            var content = "Hello, товарищ!";
            Directory.CreateDirectory(dir);
            File.Delete(path);

            //Create file through reading source
            new FullRead(
                new TeeOnRead(
                    new AsConduit(content),
                    new AsConduit(new Uri(path))
                )
            ).Trigger();

            Assert.Equal(
                content,
                new Uri(path)
                    .AsStreamReader()
                    .AsConduit()
                    .AsText()
                    .Str()
            );
        }

    }
}
