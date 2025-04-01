using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class NoneTests
    {
        [Fact]
        public void StringIsEmpty()
        {
            Assert.False(
                None._<string>().GetEnumerator().MoveNext()
            );
        }

        [Fact]
        public void GenericIsEmpty()
        {
            Assert.False(
                None._<int>().GetEnumerator().MoveNext()
            );
        }
    }
}
