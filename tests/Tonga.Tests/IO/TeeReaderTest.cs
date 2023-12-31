

using System.IO;
using Xunit;
using Tonga.Text;

namespace Tonga.IO.Tests
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
                AsText._(
                    new AsInput(
                        new AsReader(baos.ToArray()))
                ).AsString().CompareTo(content) == 0,
                "Can't read content");
        }

    }
}
