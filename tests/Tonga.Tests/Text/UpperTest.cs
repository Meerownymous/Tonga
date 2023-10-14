

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class UpperTest
    {
        [Fact]
        public void ConvertsText()
        {
            Assert.Equal(
                "HELLO!",
                new Upper(new LiveText("Hello!")).AsString()
            );
        }

    }
}
