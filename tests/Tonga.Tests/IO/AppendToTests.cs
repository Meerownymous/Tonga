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
                new TeeOnReadConduit(txt,
                    new AppendTo(
                        new AsConduit(new Uri(file))
                    )
                );

            ReadAll._(pipe).Invoke();
            ReadAll._(pipe).Invoke();

            Assert.True(
                AsText._(
                    new ConduitAsBytes(
                        new AsConduit(new Uri(file))))
                .AsString() == (txt + txt),
                "Can't append path content");
        }

        [Fact]
        public void WritesSimpleFileContent()
        {
            using var file = new TempFile();

            var path = file.Value();
            var txt = "Hello, Objects!";
            var tee =
                new TeeOnReadConduit(txt,
                    new AppendTo(
                        new AsConduit(new Uri(file.Value()))
                    )
                );

            ReadAll._(tee).Invoke();
            ReadAll._(tee).Invoke();
            tee.Stream().Close();

            Assert.Equal(
                txt + txt,
                AsText._(
                    new ConduitAsBytes(
                        new AsConduit(
                            new FileInfo(file.Value())
                        )
                    )
                ).AsString()
            );
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
