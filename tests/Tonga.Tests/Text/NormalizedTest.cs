

using Tonga.Text;
using Xunit;

namespace Tonga.Tests.Text
{
    public sealed class NormalizedTest
    {
        [Fact]
        public void NormalizesText()
        {
            Assert.True(
            new Normalized(" \t hello  \t\tworld   \t").Str() == "hello world",
            "Can't normalize a text");
        }
    }
}
