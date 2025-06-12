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
            using var tempDir = new TempDirectory();
            var file = Path.GetFullPath(Path.Combine(tempDir.Value().FullName, "file.txt"));
            var txt = "Hello, товарищ!";
            var conduit =
                new TeeOnRead(txt,
                    new Appending(
                        new AsConduit(new Uri(file))
                    )
                );

                new FullRead(conduit, close: false).Trigger();
                new FullRead(conduit).Trigger();

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

            new FullRead(tee, close: false).Trigger();
            new FullRead(tee, close: false).Trigger();
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
            using var temp = new TempFile();
            var appendTo = new Appending(new Uri(temp.Value()));
            var stream = appendTo.Stream();
            Assert.True(stream.CanWrite);
            appendTo.Dispose();
            Assert.False(stream.CanWrite);
        }
    }
}
