


using Xunit;

namespace Tonga.Map.Tests
{
    public sealed class EmptyMapTests
    {
        [Fact]
        public void GenericIsEmpty()
        {
            Assert.Empty(Empty._<double, string>().Pairs());
        }
    }
}
