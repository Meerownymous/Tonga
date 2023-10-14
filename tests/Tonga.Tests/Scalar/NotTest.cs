

using Xunit;

namespace Tonga.Scalar.Tests
{
    public sealed class NotTest
    {
        [Fact]
        public void TrueToFalse()
        {
            Assert.True(
            new Not(new True()).Value() == new False().Value());
        }

        [Fact]
        public void FalseToTrue()
        {
            Assert.True(
                new Not(new False()).Value() == new True().Value());
        }
    }
}
