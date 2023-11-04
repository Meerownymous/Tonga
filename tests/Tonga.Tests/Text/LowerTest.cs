

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
                    AsText._("HelLo!")
                ).AsString() == "hello!"
            );
        }
    }
}
