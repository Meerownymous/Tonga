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
            var conduit =
                new TeeOnRead(txt,
                    new Appending(new Uri(file))
                );

                new FullRead(conduit, flush: true, close: false).Trigger();
                new FullRead(conduit).Trigger();

                AssertText.Equal(
                    txt + txt,
                    new ConduitAsBytes(new Uri(file))
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
                    new Appending(new Uri(file.Value()))
                );

            new FullRead(tee, close: false).Trigger();
            new FullRead(tee, close: false).Trigger();
            tee.Stream().Close();

            AssertText.Equal(
                txt + txt,
                new ConduitAsBytes(
                    new FileInfo(file.Value())
                )
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
