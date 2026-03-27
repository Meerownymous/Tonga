using Tonga.Enumerable;
using Xunit;

namespace Tonga.Tests.Enumerable
{
    public sealed class EmptyTests
    {
        [Fact]
        public void StringIsEmpty()
        {
            Assert.False(
                new Empty<string>().GetEnumerator().MoveNext()
            );
        }

        [Fact]
        public void GenericIsEmpty()
        {
            Assert.False(
                new Empty<int>().GetEnumerator().MoveNext()
            );
        }
    }
}
