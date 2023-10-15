

using Xunit;
using Tonga.Enumerable;

namespace Tonga.Enumerator.Test
{
    public sealed class LengthOfTest
    {
        [Fact]
        public void Counts()
        {
            Assert.True(
                new LengthOf(
                    EnumerableOf.Pipe(1, 2, 3, 4, 5).GetEnumerator()
                ).Value() == 5,
                "cannot count items"
            );
        }
    }
}
