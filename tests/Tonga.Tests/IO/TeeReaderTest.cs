using System;
using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TeeReaderTest
    {
        [Fact]
        public void TestTeeReader()
        {
            var baos = new MemoryStream();
            var content = "Hello, товарищ!";

            var reader = new TeeReader(
                new AsReader(content),
                new WriterTo(
                    new OutputTo(baos))
            );
            int done = 0;
            while (done >= 0)
            {
                done = reader.Read();
            }
            reader.Dispose();
            Assert.True(
                String.Compare(
                    AsText._(
                        new AsInput(
                            new AsReader(baos.ToArray()))
                    ).AsString(),
                    content,
                    StringComparison.Ordinal
                ) == 0,
                "Can't read content"
            );
        }

    }
}
