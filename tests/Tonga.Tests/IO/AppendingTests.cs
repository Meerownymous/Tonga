using System;
using System.IO;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class AppendingTests
    {
        [Fact]
        public void WritesSimplePathContent()
        {
            var temp = Directory.CreateDirectory("artifacts/AppendToTest/");
            var file = Path.GetFullPath(Path.Combine(temp.FullName, "file.txt"));
            if (File.Exists(file)) File.Delete(file);

            var txt = "Hello, товарищ!";

            var stream =
                new TeeOnRead(txt,
                    new Appending(
                        new AsConduit(new Uri(file))
                    )
                );

            new FullRead(stream).Yield();
            new FullRead(stream).Yield();

            Assert.Equal(
                txt + txt,
                new ConduitAsBytes(
                    new AsConduit(new Uri(file))).AsText().Str()
            );
        }

        [Fact]
        public void WritesSimpleFileContent()
        {
            using var file = new TempFile();

            _ = file.Value();
            var txt = "Hello, Objects!";
            var tee =
                new TeeOnRead(txt,
                    new Appending(
                        new AsConduit(new Uri(file.Value()))
                    )
                );

            new FullRead(tee).Yield();
            new FullRead(tee).Yield();
            tee.Stream().Close();

            Assert.Equal(
                txt + txt,
                new ConduitAsBytes(
                    new AsConduit(
                        new FileInfo(file.Value())
                    )
                ).AsText()
                .Str()
            );
        }

        [Fact]
        public void DisposesStream()
        {
            using (var temp = new TempFile())
            {

                var appendTo = new Appending(new Uri(temp.Value()));
                var stream = appendTo.Stream();
                Assert.True(stream.CanWrite);
                appendTo.Dispose();
                Assert.False(stream.CanWrite);
            }
        }
    }
}
