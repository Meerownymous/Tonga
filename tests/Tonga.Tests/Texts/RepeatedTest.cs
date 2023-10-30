

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class RepeatedTest
    {
        [Fact]
        public void RepeatsWordsText()
        {
            Assert.True(
                new Repeated("hello", 2).AsString() == "hellohello",
                "Can't repeat a text");
        }

        [Fact]
        public void RepeatsCharText()
        {
            Assert.True(
                new Repeated("A", 5).AsString() == "AAAAA",
                "Can't repeat a char");
        }
    }
}
