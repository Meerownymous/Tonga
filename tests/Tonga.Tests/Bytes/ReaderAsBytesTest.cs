using System.IO;
using Tonga.Bytes;
using Tonga.IO;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Bytes
{
    public sealed class ReaderAsBytesTest
    {
        [Fact]
        public void ReadsString()
        {
            Assert.Equal(
                "hello, друг!",
                AsText._(
                    new ReaderAsBytes(
                        new StreamReader(
                            new AsConduit("hello, друг!").Stream())
                    )
                ).AsString()
            );
        }
    }
}
