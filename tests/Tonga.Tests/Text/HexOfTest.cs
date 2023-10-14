

using Xunit;
using Tonga.Bytes;

namespace Tonga.Text.Test
{
    public sealed class HexOfTest
    {
        [Fact]
        public void EmptyString()
        {
            Assert.Equal(
                string.Empty,
                new HexOf(new BytesOf(string.Empty.ToCharArray())).AsString()
            );
        }

        [Fact]
        public void Sentence()
        {
            Assert.Equal(
                "5768617427732075702c20d0b4d180d183d0b33f",
                new HexOf(
                    new BytesOf("What's up, друг?")
                ).AsString()
            );
        }
    }
}
