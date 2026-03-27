

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class LowerTest
    {
        [Fact]
        public void ConvertsText()
        {
            AssertText.Equal(
                "hello!",
                new Lower("HelLo!")
            );
        }
    }
}
