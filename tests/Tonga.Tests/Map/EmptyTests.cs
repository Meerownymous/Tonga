


using Tonga.Map;
using Xunit;

namespace Tonga.Tests.Map
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
