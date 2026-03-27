

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class ReversedTest
    {
        [Fact]
        public void ReverseText()
        {
            AssertText.Equal(
                "!olleH",
                new Reversed("Hello!")
            );
        }

        [Fact]
        public void ReversedEmptyTextIsEmptyText()
        {
            AssertText.Equal(
                "",
                new Reversed("")
            );
        }
    }
}
