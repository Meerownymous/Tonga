using Tonga.Fact;
using Xunit;

namespace Tonga.Tests.Fact
{
    public sealed class NotTest
    {
        [Fact]
        public void TrueToFalse()
        {
            Assert.True(
                new Not(new True()).IsTrue() == new False().IsTrue()
            );
        }

        [Fact]
        public void FalseToTrue()
        {
            Assert.True(
                new Not(new False()).IsTrue() == new True().IsTrue());
        }
    }
}
