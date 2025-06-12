

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class ReversedTest
    {
        [Fact]
        public void ReverseText()
        {
            Assert.Equal(
                "!olleH",
                "Hello!".AsText().AsReversed().Str()
            );
        }

        [Fact]
        public void ReversedEmptyTextIsEmptyText()
        {
            Assert.Equal(
                "",
                "".AsText().AsReversed().Str()
            );
        }
    }
}
