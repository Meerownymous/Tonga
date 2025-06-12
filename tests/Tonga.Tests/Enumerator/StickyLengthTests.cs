using Tonga.Enumerable;
using Tonga.Enumerator;
using Xunit;

namespace Tonga.Tests.Enumerator
{
    public sealed class StickyLengthTests
    {
        [Fact]
        public void Counts()
        {
            Assert.Equal(
                5,
                new StickyLength(
                    (1, 2, 3, 4, 5).AsEnumerable().GetEnumerator()
                ).Value()
            );
        }
    }
}
