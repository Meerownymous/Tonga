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
            var reader = "hello, друг!".AsStreamReader();
            var bytes = new ReaderAsBytes(reader).Raw();

            var x =
                new ReaderAsBytes(
                    "hello, друг!".AsStreamReader()
                ).AsStream();//.FullRead().Yield();

            Assert.Equal(
                "hello, друг!",
                    new ReaderAsBytes(
                        "hello, друг!".AsStreamReader()
                    )
                    .AsText()
                    .Str()
            );
        }
    }
}
