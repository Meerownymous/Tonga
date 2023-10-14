

using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class TrueTest
    {
        [Fact]
        public void AsValue()
        {
            Assert.True(
            new True().Value() == true);

        }
    }
}
