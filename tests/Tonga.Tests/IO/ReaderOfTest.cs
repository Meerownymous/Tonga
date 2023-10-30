

using System;
using System.IO;
using Xunit;
using Tonga.Text;
using Tonga.Scalar;
using Tonga.Func;

namespace Tonga.IO.Tests
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
                new TeeInput(
                    new AsInput(content),
                    new OutputTo(path)
                )
            ).Invoke();

            Assert.Equal(
                content,
                AsText._(
                    new AsInput(
                        new AsReader(
                            new Uri(path)))
                ).AsString()
            );
        }

    }
}
