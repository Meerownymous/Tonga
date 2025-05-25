

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class LowerTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.True(
                new Lower(
                    AsText._("HelLo!")
                ).Str() == "hello!"
            );
        }
    }
}
