using Tonga.Enumerable;
using Tonga.Enumerator;
using Xunit;

namespace Tonga.Tests.Enumerator
{
    public sealed class LiveLengthOfTests
    {
        [Fact]
        public void Counts()
        {
            Assert.True(
                new LiveLengthOf(
                    AsEnumerable._(1, 2, 3, 4, 5).GetEnumerator()).Value() == 5,
                "cannot count items"
            );
        }
    }
}
