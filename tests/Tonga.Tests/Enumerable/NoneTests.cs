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
                new None<string>().GetEnumerator().MoveNext()
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
