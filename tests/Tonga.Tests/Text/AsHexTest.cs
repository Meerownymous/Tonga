using Tonga.Bytes;
using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class AsHexTest
    {
        [Fact]
        public void EmptyString()
        {
            Assert.Equal(
                string.Empty,
                new AsHex(new AsBytes(string.Empty.ToCharArray())).Str()
            );
        }

        [Fact]
        public void Sentence()
        {
            Assert.Equal(
                "5768617427732075702c20d0b4d180d183d0b33f",
                new AsHex(
                    new AsBytes("What's up, друг?")
                ).Str()
            );
        }
    }
}
