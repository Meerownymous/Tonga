

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class RepeatedTest
    {
        [Fact]
        public void RepeatsWordsText()
        {
            Assert.Equal(
                "hellohello",
                new Repeated("hello", 2).Str()
            );
        }

        [Fact]
        public void RepeatsCharText()
        {
            Assert.Equal(
                "AAAAA",
                new Repeated("A", 5).Str()
            );
        }
    }
}
