


using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class EmptyMapTests
    {
        [Fact]
        public void NonGenericIsEmpty()
        {
            Assert.Empty(new EmptyMap());
        }

        [Fact]
        public void SemiGenericIsEmpty()
        {
            Assert.Empty(new EmptyMap<double>());
        }

        [Fact]
        public void GenericIsEmpty()
        {
            Assert.Empty(new EmptyMap<double, string>());
        }
    }
}
