

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
                new None().GetEnumerator().MoveNext()
            );
        }

        [Fact]
        public void GenericIsEmpty()
        {
            Assert.False(
                new None<int>().GetEnumerator().MoveNext()
            );
        }
    }
}
