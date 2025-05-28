using System;
using System.IO;
using Tonga.Func;
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
            var dir = "artifacts/ReaderOfTest"; var file = "txt-1"; var path = Path.GetFullPath(Path.Combine(dir, file));
            var content = "Hello, товарищ!";
            Directory.CreateDirectory(dir);
            File.Delete(path);

            //Create file through reading source
            ReadAll._(
                new TeeOnRead(
                    new AsConduit(content),
                    new AsConduit(new Uri(path))
                )
            ).Invoke();

            Assert.Equal(
                content,
                AsText._(
                    new AsConduit(
                        new AsStreamReader(
                            new Uri(path)))
                ).AsString()
            );
        }

    }
}
