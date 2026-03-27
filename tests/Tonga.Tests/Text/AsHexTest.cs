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
            AssertText.Equal(
                string.Empty,
                new AsHex(string.Empty.ToCharArray())
            );
        }

        [Fact]
        public void Sentence()
        {
            AssertText.Equal(
                "5768617427732075702c20d0b4d180d183d0b33f",
                new AsHex("What's up, друг?")
            );
        }

        [Fact]
        public void SentenceImplicit()
        {
            AssertText.Equal(
                "5768617427732075702c20d0b4d180d183d0b33f",
                new AsHex("What's up, друг?")
            );
        }
    }
}
