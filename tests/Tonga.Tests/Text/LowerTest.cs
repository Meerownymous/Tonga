

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class LowerTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.True(
                new Lower(
                    new LiveText("HelLo!")
                ).AsString() == "hello!"
            );
        }
    }
}
