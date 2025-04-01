using System;
using System.IO;
using Tonga.Bytes;
using Tonga.Func;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
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
            using (var file = new TempFile())
            {
                var txt = "Hello, Objects!";
                var pipe =
                    new TeeInput(txt,
                        new AppendTo(
                            new OutputTo(file.Value())
                        )
                    );

                ReadAll._(pipe).Invoke();
                ReadAll._(pipe).Invoke();
                pipe.Stream().Close();

                Assert.Equal(
                    txt + txt,
                    AsText._(
                        new InputAsBytes(
                            new AsInput(new FileInfo(file.Value()))
                        )
                    ).AsString()
                );
            }
        }

        [Fact]
        public void DisposesStream()
        {
            using (var temp = new TempFile())
            {

                var appendTo = new AppendTo(new Uri(temp.Value()));
                var stream = appendTo.Stream();
                Assert.True(stream.CanWrite);
                appendTo.Dispose();
                Assert.False(stream.CanWrite);
            }
        }
    }
}
