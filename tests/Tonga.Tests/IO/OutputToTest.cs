using System;
using System.IO;
using Tonga.Bytes;
using Tonga.Func;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class OutputToTest
    {
        [Fact]
        public void WritesSimplePathContent()
        {
            var temp = Directory.CreateDirectory("artifacts/OutputToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file)) File.Delete(file);

            String content = "Hello, товарищ!";
            ReadAll._(
                new TeeInput(
                    content,
                    new OutputTo(file)
                )
            ).Invoke();

            Assert.True(
                AsText._(
                    new InputAsBytes(
                        new AsInput(new Uri(file))))
                .AsString() == content,
                "Can't write path content");
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
                new TeeInput(
                    txt,
                    new OutputTo(file))
            ).Invoke();

            Assert.Equal(
                txt,
                AsText._(
                    new InputAsBytes(
                        new AsInput(file)
                    )
                )
                .AsString()
            );
        }
    }
}
