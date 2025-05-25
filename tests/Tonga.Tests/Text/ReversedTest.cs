

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class ReversedTest
    {
        [Fact]
        public void ReverseText()
        {
            Assert.True(
                new Reversed(
                    AsText._("Hello!")
                ).Str() == "!olleH",
                "Can't reverse a text");
        }

        [Fact]
        public void ReversedEmptyTextIsEmptyText()
        {
            Assert.True(
                new Reversed(
                    AsText._("")
                ).Str() == "",
                "Can't reverse empty text");
        }
    }
}
