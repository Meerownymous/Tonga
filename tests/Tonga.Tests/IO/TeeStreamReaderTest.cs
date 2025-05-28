using System;
using System.IO;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.IO
{
    public sealed class TeeStreamReaderTest
    {
        [Fact]
        public void TestTeeReader()
        {
            var baos = new MemoryStream();
            var content = "Hello, товарищ!";

            var reader = new TeeStreamReader(
                new AsStreamReader(content),
                new AsStreamWriter(
                    new AsConduit(baos))
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
                        new Tonga.IO.AsConduit(
                            new AsStreamReader(baos.ToArray()))
                    ).AsString(),
                    content,
                    StringComparison.Ordinal
                ) == 0,
                "Can't read content"
            );
        }

    }
}
