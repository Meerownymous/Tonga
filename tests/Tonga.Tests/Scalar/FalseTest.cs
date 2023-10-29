

using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class FalseTest
    {
        [Fact]
        public void AsValue()
        {
            Assert.False(
                False._().Value());
        }
    }
}
