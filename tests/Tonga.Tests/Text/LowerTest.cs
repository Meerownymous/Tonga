

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class LowerTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.Equal(
                "hello!",
                new Lower(
                    "HelLo!".AsText()
                ).Str()
            );
        }
    }
}
