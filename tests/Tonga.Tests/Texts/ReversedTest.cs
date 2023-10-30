

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class ReversedTest
    {
        [Fact]
        public void ReverseText()
        {
            Assert.True(
                new Reversed(
                    AsText._("Hello!")
                ).AsString() == "!olleH",
                "Can't reverse a text");
        }

        [Fact]
        public void ReversedEmptyTextIsEmptyText()
        {
            Assert.True(
                new Reversed(
                    AsText._("")
                ).AsString() == "",
                "Can't reverse empty text");
        }
    }
}
