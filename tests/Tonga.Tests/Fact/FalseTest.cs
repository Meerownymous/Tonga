using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact
{
    public sealed class FalseTest
    {
        [Fact]
        public void AsValue()
        {
            Assert.False(
                new False().IsTrue()
            );
        }
    }
}
