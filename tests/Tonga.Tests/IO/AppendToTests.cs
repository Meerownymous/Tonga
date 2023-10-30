using System;
using System.IO;
using Xunit;
using Tonga.Bytes;
using Tonga.Text;
using Tonga.Scalar;
using Tonga.Func;

namespace Tonga.IO.Tests
{
    public sealed class AppendToTests
    {
        [Fact]
        public void WritesSimplePathContent()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file)) File.Delete(file);

            var txt = "Hello, товарищ!";

            var pipe =
                new TeeInput(txt,
                    new AppendTo(
                        new OutputTo(file)
                    )
                );

            ReadAll._(pipe).Invoke();
            ReadAll._(pipe).Invoke();

            Assert.True(
                AsText._(
                    new InputAsBytes(
                        new AsInput(new Uri(file))))
                .AsString() == (txt + txt),
                "Can't append path content");
        }

        [Fact]
        public void WritesSimpleFileContent()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest");
            var file = new Uri(Path.GetFullPath(Path.Combine(temp.FullName, "file.txt")));
            if (File.Exists(file.AbsolutePath))
            {
                File.Delete(file.AbsolutePath);
            }

            var txt = "Hello, друг!";
            var pipe =
                new TeeInput(txt,
                    new AppendTo(
                        new OutputTo(file)
                    )
                );

            ReadAll._(pipe).Invoke();
            ReadAll._(pipe).Invoke();

            Assert.Equal(
                txt + txt,
                AsText._(
                    new InputAsBytes(
                        new AsInput(file)
                    )
                ).AsString()
            );
        }

        [Fact]
        public void DisposesStream()
        {
            using (var temp = new TempFile())
            {

                var appendTo = new AppendTo(temp.Value());
                var stream = appendTo.Stream();
                Assert.True(stream.CanWrite);
                appendTo.Dispose();
                Assert.False(stream.CanWrite);
            }
        }
    }
}
