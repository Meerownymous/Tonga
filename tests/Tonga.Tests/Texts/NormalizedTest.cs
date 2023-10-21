

using Xunit;

namespace Tonga.Text.Test
{
    public sealed class NormalizedTest
    {
        [Fact]
        public void NormalizesText()
        {
            Assert.True(
            new Normalized(" \t hello  \t\tworld   \t").AsString() == "hello world",
            "Can't normalize a text");
        }
    }
}
