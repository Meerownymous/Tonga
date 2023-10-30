

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Enumerable.Test
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
