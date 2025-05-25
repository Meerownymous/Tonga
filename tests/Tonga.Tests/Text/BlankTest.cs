using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class BlankTest
    {
        [Fact]
        public void IsBlank()
        {
            Assert.Equal(
                " ",
                new Blank().Str()
            );
        }
    }
}

