using Tonga.Enumerable;
using Tonga.Enumerator;
using Xunit;

namespace Tonga.Tests.Enumerator
{
    public sealed class LengthOfTest
    {
        [Fact]
        public void Counts()
        {
            Assert.True(
                new LengthOf(
                    AsEnumerable._(1, 2, 3, 4, 5).GetEnumerator()
                ).Value() == 5,
                "cannot count items"
            );
        }
    }
}
